using System;
using System.Collections.Generic;

namespace LicPlate
{
    public class LicenseCharacter
    {
        private string chr;
        private double err;
        private double conf;
        public int left; // the x coord of the character
        public vector_PatternMatchResult PatternResults;

        public string character
        {
            get
            {
                return chr;
            }
            set
            {
                chr = value;
            }
        }
        public double error
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }
        public double confidence
        {
            get
            {
                return conf;
            }
            set
            {
                conf = value;
            }
        }
        public new string ToString()
        {
            return chr.ToString() + " " + Math.Round(err, 2).ToString();
        }

        public LicenseCharacter(string character) : this(character, -1) { }
        public LicenseCharacter(string character, double error) : this(character, -1, -1) { }
        public LicenseCharacter(string character, double error, double confidence) : this(character, error, confidence, -1, null) { }
        public LicenseCharacter(string character, double error, double confidence, int leftPos, vector_PatternMatchResult results)
        {
            chr = character;
            err = error;
            conf = confidence;
            left = leftPos;
            PatternResults = results;
        }
    }

    public class LicensePlate
    {
        private double conf;
        private List<LicenseCharacter> chars;
        
        // De positie van het streepje (eerst volgede nieuwe positie)
        private int pos1;
        private int pos2;

        private void getPos()
        {
            pos1 = -1;
            pos2 = -1;

            int[] difference = new int[5];
            LicenseCharacter previous = chars[0];

            for(int i = 1; i < chars.Count; i++)//LicenseCharacter LicChar in chars)
            {
               
                difference[i - 1] = chars[i].left - previous.left;                
                previous = chars[i];
            }

            int valuePos1 = 0;
            int valuePos2 = 0;
            for(int a = 0; a < difference.Length; a++)// in difference)
            {
                if (difference[a] > valuePos1)
                {
                    valuePos2 = valuePos1;
                    valuePos1 = difference[a];
                    pos2 = pos1;
                    pos1 = a + 1;
                }
                else if (difference[a] > valuePos2)
                {
                    valuePos2 = difference[a];
                    pos2 = a + 1;
                }
            }

            if (pos1 > pos2)
            {
                int temp = pos1;
                pos1 = pos2;
                pos2 = temp;
            }

        }

        private enum kenType { DONTKNOW, NUMBER, CHAR };

        public void CalculateBestMatch(BlobMatcher_Int16 matcher)
        {
            getPos();
            kenType[] partsNum = new kenType[3];
            int partNumPlace = 0;

            // The difference is to small
            // must be wrong so stop this algoritm
            if (pos2 - pos1 < 2 ||
              !((pos1 == 1 && pos2 == 4) || 
               (pos1 == 2 && (pos2 == 5 || pos2 == 4))
               ))
                return;
            Console.WriteLine(pos1 + " " + pos2);

            // Build the expected partern
            double err = double.MaxValue;
            for (int i = 0; i < chars.Count; i++)
            {
                if (i == pos1 || i == pos2)
                {
                    partNumPlace += 1;
                    err = double.MaxValue;
                }

                if (chars[i].error <= 0.13 && chars[i].error < err)// && chars[i].confidence > 0.40)
                {
                    err = chars[i].error;
                    // Sure enough about this char to use if to determine if part is num or not
                    if (Char.IsNumber(chars[i].character[0]))
                    {
                        partsNum[partNumPlace] = kenType.NUMBER;
                    }
                    else
                    {
                        partsNum[partNumPlace] = kenType.CHAR;
                    }

                }
            }

            if ((partsNum[0] == kenType.NUMBER && partsNum[1] == kenType.NUMBER) ||
                (partsNum[1] == kenType.NUMBER && partsNum[2] == kenType.NUMBER))
                return;

            bool matched = true;
            foreach (kenType abc in partsNum)
            {
                if (abc == kenType.DONTKNOW)
                    matched = false;
            }


            if (!matched)
            {
                // moet altijd cijfers chars cijfers zijn
                if ((pos1 == 1 && pos2 == 4) ||
                    (pos1 == 2 && pos2 == 5))
                {
                    Console.WriteLine("Changed em!");
                    partsNum[0] = kenType.NUMBER;
                    partsNum[1] = kenType.CHAR;
                    partsNum[2] = kenType.NUMBER;
                }
                else // Dan hebben we nog de andere mogelijkheden
                {
                    if (partsNum[0] == kenType.NUMBER)
                    {
                        partsNum[1] = kenType.CHAR;
                        partsNum[2] = kenType.CHAR;
                    }
                    else if (partsNum[1] == kenType.NUMBER)
                    {
                        partsNum[0] = kenType.CHAR;
                        partsNum[2] = kenType.CHAR;
                    }
                    else if (partsNum[2] == kenType.NUMBER)
                    {
                        partsNum[0] = kenType.CHAR;
                        partsNum[1] = kenType.CHAR;

                    }
                    else if (partsNum[0] == kenType.CHAR && partsNum[1] == kenType.CHAR)
                        partsNum[2] = kenType.NUMBER;
                    else if (partsNum[0] == kenType.CHAR && partsNum[2] == kenType.CHAR)
                        partsNum[1] = kenType.NUMBER;
                    else if (partsNum[1] == kenType.CHAR && partsNum[2] == kenType.CHAR)
                        partsNum[0] = kenType.NUMBER;

                }
            }


            string s = "";
            foreach (kenType abc in partsNum)
                s += abc.ToString() + " ";
            Console.WriteLine(s);

            partNumPlace = 0;
            for (int i = 1; i < chars.Count; i++)//LicenseCharacter LicChar in chars)
            {
                if (i == pos1 || i == pos2)
                    partNumPlace += 1;

                if (partsNum[partNumPlace] == kenType.NUMBER && chars[i].error < 0.20)
                {
                    // Numbers expected
                    if(!Char.IsNumber(chars[i].character[0]))
                    {
                        foreach(PatternMatchResult patrn in chars[i].PatternResults)
                            if(Char.IsNumber(matcher.PatternName(patrn.id)[0]))
                            { 
                                // Match found not a number! Second match is
                                // So change it
                                chars[i] = new LicenseCharacter(matcher.PatternName(patrn.id), -0.04, 41);
                                break;
                            }
                    }
                    else if (chars[i].error > 0.11 && chars[i].error < 0.20)
                    {
                        if (!Char.IsNumber(matcher.PatternName(chars[i].PatternResults[1].id)[0]))
                        {
                            // Is a number and second match isn't so throw up confidence
                            chars[i] = new LicenseCharacter(chars[i].character, -0.02, 41);
                        }
                        else if (!Char.IsNumber(matcher.PatternName(chars[i].PatternResults[2].id)[0]))
                        {
                            chars[i] = new LicenseCharacter(chars[i].character, -0.01, chars[i].confidence + 0.1);
                        }
                    }
                }
                else if (partsNum[partNumPlace] == kenType.CHAR && chars[i].error < 0.20)
                { 
                    // Chars expected
                    if(Char.IsNumber(chars[i].character[0]))
                    {
                        foreach (PatternMatchResult patrn in chars[i].PatternResults)
                            if (!Char.IsNumber(matcher.PatternName(patrn.id)[0]))
                            {
                                // Match found not a number! Second match is
                                // So change it
                                chars[i] = new LicenseCharacter(matcher.PatternName(patrn.id), -0.04, 41);
                                break;
                            }
                    }
                    else if (chars[i].error > 0.11 && chars[i].error < 0.20)
                    {
                        if (Char.IsNumber(matcher.PatternName(chars[i].PatternResults[1].id)[0]))
                        {
                            // Is a number and second match isn't so throw up confidence
                            chars[i] = new LicenseCharacter(chars[i].character, -0.02, 41);
                        }
                        else if (Char.IsNumber(matcher.PatternName(chars[i].PatternResults[2].id)[0]))
                        {
                            chars[i] = new LicenseCharacter(chars[i].character, -0.01, chars[i].confidence + 0.1);
                        }
                    }
                }
            }

        }

        public LicensePlate()
        {
            confidence = -1;
            chars = new List<LicenseCharacter>();
        }
        private double getSmallestConfidence()
        {
            double min = double.MaxValue;
            for (int i = 0; i < chars.Count; i++)
            {
                if (chars[i].confidence < min)
                {
                    min = chars[i].confidence;
                }
            }
            return min;
        }
        public double confidence
        {
            get
            {
                if (conf != -1)
                {
                    return conf;
                }
                else
                {
                    return getSmallestConfidence();
                }
            }
            set
            {
                conf = value;
            }
        }
        public List<LicenseCharacter> characters
        {
            get
            {
                return chars;
            }
        }

        public new string ToString()
        {
            return getLicensePlateString() + " " + Math.Round(confidence, 2).ToString() + " " + getLicensePlateErrorsString();
        }

        public string getLicensePlateErrorsString()
        {
            string res = "";
            foreach (LicenseCharacter c in characters)
            {
                res = res + " " + Math.Round(c.error, 2).ToString();
            }
            return res.Trim();
        }

        public string getLicensePlateString()
        {
            string res = "";
            foreach (LicenseCharacter c in characters)
            {
                res = res + c.character;
            }
            return res;
        }
    }
}