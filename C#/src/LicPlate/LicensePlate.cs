using System;
using System.Collections.Generic;

namespace LicPlate
{
    public class LicenseCharacter
    {
        private string chr;
        private double err;
        private double conf;

        // The center point of a character
        public int middle; 
        // A vector containing all the different results from the matcher
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
        public LicenseCharacter(string character, double error, double confidence, int middlePos, vector_PatternMatchResult results)
        {
            chr = character;
            err = error;
            conf = confidence;
            middle = middlePos;
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

        /// <summary>
        /// Self created enum which holds the possible license plate thingies
        /// </summary>
        private enum kenType { DONTKNOW, NUMBER, CHAR };

        #region Stripe position matching

        /// <summary>
        /// A method which will loop trough each char and calculate the offset between
        /// all the characters
        /// </summary>
        /// <returns>An array filled with the differene</returns>
        private int[] calculateDifference()
        {
            // An array which will be filled with the difference between each char
            int[] difference = new int[5];
            // A reference to the previous License char
            LicenseCharacter previous = chars[0];

            // Loop through
            for (int i = 1; i < chars.Count; i++)
            {
                // Calculate the differene between each
                difference[i - 1] = chars[i].middle - previous.middle;
                previous = chars[i];
            }
            return difference;
        }

        /// <summary>
        /// A method which find the position of the stripes based on the 
        /// offset between the chars.
        /// 
        /// It changes pos1 and pos2 into these positions
        /// </summary>
        /// <param name="pos1">The position of the first stripe</param>
        /// <param name="pos2">The position of the second stripe</param>
        /// <param name="difference">An int[] containing the offset between each
        /// character</param>
        private void findPos(ref int pos1, ref int pos2, int[] difference)
        {
            // Two variables which will hold the value found for each variable
            int valuePos1 = 0;
            int valuePos2 = 0;

            // Loop through each found difference
            for (int a = 0; a < difference.Length; a++)
            {
                // Check if we got a new value for pos1
                if (difference[a] > valuePos1)
                {
                    // Change pos2 into pos1 and store the new differences
                    valuePos2 = valuePos1;
                    valuePos1 = difference[a];
                    pos2 = pos1;
                    pos1 = a + 1;
                } // Check if the found number is bigger then pos2
                else if (difference[a] > valuePos2)
                {
                    // Store it in pos2
                    valuePos2 = difference[a];
                    pos2 = a + 1;
                }
            }

            // Since passed by reference there is no return
        }

        /// <summary>
        /// A method which gives pos1 the lowest number 
        /// and pos2 the highest number.
        /// 
        /// This saves for posibilities for the pattern matcher
        /// </summary>
        /// <param name="pos1">The position of the first stripe</param>
        /// <param name="pos2">The position of the second stripe</param>
        private void sortPos(ref int pos1, ref int pos2)
        {
            // Check if pos1 is bigger then pos2
            if (pos1 > pos2)
            {
                // then we need to swap em
                int temp = pos1;
                pos1 = pos2;
                pos2 = temp;
            }
        }
        
        /// <summary>
        /// Calculate the position of the stripe
        /// 
        /// It changes pos1 and pos2 to the position of the new part
        /// </summary>
        private void getPos()
        {
            // Reset the pos1 and pos2
            pos1 = -1;
            pos2 = -1;

            // An array which will be filled with the difference between each char
            int[] difference = calculateDifference();

            // Find the positions of the lines
            findPos(ref pos1, ref pos2, difference);

            // And sort em
            sortPos(ref pos1, ref pos2);
        }
        
        #endregion

        #region Pattern finding methods

        /// <summary>
        /// A method which builds the expected pattern
        /// </summary>
        /// <param name="pos1">The position of the first stripe</param>
        /// <param name="pos2">The position of the second stripe</param>
        private kenType[] createpattern(int pos1, int pos2)
        {
            kenType[] partsNum = new kenType[3];
            // If it matches this then it must be this pattern
            if ((pos1 == 1 && pos2 == 4) ||
                    (pos1 == 2 && pos2 == 5))
            {
                partsNum[0] = kenType.NUMBER;
                partsNum[1] = kenType.CHAR;
                partsNum[2] = kenType.NUMBER;
                return partsNum;
            }

            // If the state ment didn't match then it must be a 
            // pattern with pos1 = 2 and pos2 = 4
            
            // Create a pointer 
            int partNumPlace = 0;

            // The error of a found pattern
            double err = double.MaxValue;
            for (int i = 0; i < chars.Count; i++)
            {
                // Check if should move up with the pointer
                if (i == pos1 || i == pos2)
                {
                    partNumPlace += 1;
                    // Reset the error value
                    err = double.MaxValue;
                }

                // Check if we found an char / digit and the matcher is sure about it
                // and it is more sure about this one compared to the error found
                if (chars[i].error <= 0.13 && chars[i].error < err)
                {
                    // Sure enough about this char to use if to determine if part is num or not

                    // Change error to this error value
                    err = chars[i].error;

                    // Check what we found
                    if (Char.IsNumber(chars[i].character[0]))
                    {
                        partsNum[partNumPlace] = kenType.NUMBER;
                    }
                    else
                    {
                        partsNum[partNumPlace] = kenType.CHAR;
                    }
                } // End if
            } // End loop

            // Return the pattern
            return partsNum;
        }

        /// <summary>
        /// A method which checks if the kenType array contains a 
        /// DONTKNOW
        /// </summary>
        /// <param name="partsNum">The kenType array which need to be checked</param>
        /// <returns>True if no DONTKNOW was found,
        ///          False otherwise</returns>
        private bool checkFullMatch(kenType[] partsNum)
        {
            // Loop through
            foreach (kenType abc in partsNum)
            {
                // If one is a DONTKNOW
                if (abc == kenType.DONTKNOW)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// If a pattern didn't get matched fully then this method will try to create a full
        /// pattern.
        /// </summary>
        /// <param name="partsNum">The array to check</param>
        private void tryFullPattern(ref kenType[] partsNum)
        {
            // ****************************************************************************
            // **   Reconstructs based on the fact that a license plate:                 **
            // **     - If we got into this method it always has pos1 = 2 and pos2 = 4   **
            // **         and thus:                                                      **
            // **     - Always has one number (can be on all three places)               **
            // **     - Always has two char (can also be on all places)                  **     
            // **                                                                        **
            // **   Based on these facts it can be reconstruced                          **
            // *****************************************************************************
            
            // Check if there is a number on some place
            // Then the other parts must be chars
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

            } // Check if there are two chars on some place, then the other must be a number
            else if (partsNum[0] == kenType.CHAR && partsNum[1] == kenType.CHAR)
                partsNum[2] = kenType.NUMBER;
            else if (partsNum[0] == kenType.CHAR && partsNum[2] == kenType.CHAR)
                partsNum[1] = kenType.NUMBER;
            else if (partsNum[1] == kenType.CHAR && partsNum[2] == kenType.CHAR)
                partsNum[0] = kenType.NUMBER;
        }

        /// <summary>
        /// A method which creates the best possible match for a license plate
        /// </summary>
        /// <param name="pos1">The position of the first stripe</param>
        /// <param name="pos2">The posisiton of the second stripe</param>
        /// <param name="partsNum">The kenType array</param>
        /// <param name="matcher">The BlobMatcher</param>
        private void createBestMatch(int pos1, int pos2, kenType[] partsNum, BlobMatcher_Int16 matcher)
        {
            // **************************************************************
            // **  Creates the best match based on:                        **
            // **     - If there should be a number, but char found:       **
            // **        - Find a number in the results and change it      **
            // **     - If there should be a number and number found       **
            // **        - If the next match is a character then we can    **
            // **               up the confidence a lot                    **
            // **        - If the then following match is a number we      **
            // **               can up it a little                         **
            // **                                                          **
            // **     - This also aplies for chars                         **
            // **************************************************************

            // A pointer which points to the partsNum place where we are at
            int partNumPlace = 0;
            const double maxError = 0.20;

            // Go through all the chars
            for (int i = 0; i < chars.Count; i++)
            {
                // Check if we need to up the pointer
                if (i == pos1 || i == pos2)
                    partNumPlace += 1;
                
                // If there should be a number found
                if (partsNum[partNumPlace] == kenType.NUMBER && chars[i].error < maxError)
                {
                    // If it isn't a number
                    if (!Char.IsNumber(chars[i].character[0]))
                    {
                        // Loop through the results and find a number
                        foreach (PatternMatchResult patrn in chars[i].PatternResults)
                        {
                            // if this is a number
                            if (Char.IsNumber(matcher.PatternName(patrn.id)[0]))
                            {
                                // Change the chars value to this number
                                chars[i] = new LicenseCharacter(matcher.PatternName(patrn.id), -0.04, 41);
                                break;
                            }
                        } // End for loop
                    } // End number expected but not found
                    else if (chars[i].error > 0.11) // If there should be a number and a number was found
                    {
                        // Check if the next match is a char
                        if (!Char.IsNumber(matcher.PatternName(chars[i].PatternResults[1].id)[0]))
                        {
                            // If so we can up the confidence
                            chars[i] = new LicenseCharacter(chars[i].character, -0.02, 41);
                        } // If the then following match is a char
                        else if (!Char.IsNumber(matcher.PatternName(chars[i].PatternResults[2].id)[0]))
                        {
                            // Then we up the confidence a little
                            chars[i] = new LicenseCharacter(chars[i].character, -0.01, chars[i].confidence + 0.1);
                        }
                    }
                } // Check if we should expect a char and the error value isn't to high
                else if (partsNum[partNumPlace] == kenType.CHAR && chars[i].error < maxError)
                {
                    // Char expected but number found
                    if (Char.IsNumber(chars[i].character[0]))
                    {
                        // Loop through the matches
                        foreach (PatternMatchResult patrn in chars[i].PatternResults)
                        {
                            if (!Char.IsNumber(matcher.PatternName(patrn.id)[0]))
                            {
                                // If a char was found change the chars[i] value to it
                                chars[i] = new LicenseCharacter(matcher.PatternName(patrn.id), -0.04, 41);
                                break;
                            }
                        } // End for loop
                    } // If we already have a char
                    else if (chars[i].error > 0.11)
                    {
                        // Check if the second match is a number
                        if (Char.IsNumber(matcher.PatternName(chars[i].PatternResults[1].id)[0]))
                        {
                            // If so we can up the confidence a lot
                            chars[i] = new LicenseCharacter(chars[i].character, -0.02, 41);
                        } // Check if the then following match is a number
                        else if (Char.IsNumber(matcher.PatternName(chars[i].PatternResults[2].id)[0]))
                        {
                            // If so then we can up the confidence a little
                            chars[i] = new LicenseCharacter(chars[i].character, -0.01, chars[i].confidence + 0.1);
                        }
                    } // End char expected and char found
                } // End char expected
            } // End loop

            // Done
        }

        /// <summary>
        /// Create the best match for a license plate
        /// </summary>
        /// <param name="matcher">The Blobmatcher used for matching the chars</param>
        public void CalculateBestMatch(BlobMatcher_Int16 matcher)
        {
            // Calculate the positon of the stripes
            getPos();

            // Check if the difference between each is valid
            if (pos2 - pos1 < 2 ||
               // Check if the pattern is valid
               !((pos1 == 1 && pos2 == 4) ||
                (pos1 == 2 && (pos2 == 5 || pos2 == 4))
               ))
                // If not then we are done and can't match the pattern
                // Else if would result in strange license plates
                return;

            // An array which will hold the expected pattern
            kenType[] partsNum = createpattern(pos1, pos2);

            // Check if valid pattern
            if ((partsNum[0] == kenType.NUMBER && partsNum[1] == kenType.NUMBER) ||
                (partsNum[1] == kenType.NUMBER && partsNum[2] == kenType.NUMBER))
                return;

            // Check if we got a full pattern
            if (!checkFullMatch(partsNum))
                tryFullPattern(ref partsNum); // if not try if we can make it full

            // Check the found chars and create the best possible license plate
            createBestMatch(pos1, pos2, partsNum, matcher);
        }
        
        #endregion
        
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