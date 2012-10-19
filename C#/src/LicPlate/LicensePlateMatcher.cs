using System;

namespace LicPlate
{
    public enum TresholdConditions { NORMAAL, OVERBELICHT, ONDERBELICHT };

    public class LicensePlateMatcher
    {
        /*  Description:
                Find the largest license plate in the image
                    - Segment using ThresholdHSVchannels
                    - Remove blobs which are not license plates
            Input:
	            //Original image
	            RGB888Image plateImage	
            Output:
	            //Segmented license plate
	            ref Int16Image binaryPlateImage
            Return:	
	            //License plate found?
	            bool 
         */	
        public static bool FindPlate(RGB888Image plateImage, ref Int16Image binaryPlateImage, TresholdConditions state)
        {
            //Constants
            int c_threshold_h_min = 0;
            int c_threshold_h_max = 0;
            int c_threshold_s_min = 0;
            int c_threshold_s_max = 0;
            int c_threshold_v_min = 0;
            int c_threshold_v_max = 0;
            int c_remove_blobs_min = 0;
            int c_remove_blobs_max = 0;
            
            switch(state)
            {
                case(TresholdConditions.NORMAAL):
                    c_threshold_h_min = 21;
                    c_threshold_h_max = 50;
                    c_threshold_s_min = 100;
                    c_threshold_s_max = 255;
                    c_threshold_v_min = 100;
                    c_threshold_v_max = 255;
                    c_remove_blobs_min = 1;
                    c_remove_blobs_max = 500;
                    break;
                case(TresholdConditions.ONDERBELICHT):
                    c_threshold_h_min = 5;
                    c_threshold_h_max = 130;
                    c_threshold_s_min = 20;
                    c_threshold_s_max = 170;
                    c_threshold_v_min = 20;
                    c_threshold_v_max = 115;
                    c_remove_blobs_min = 1;
                    c_remove_blobs_max = 500;
                    break;
                case(TresholdConditions.OVERBELICHT):
                    c_threshold_h_min = 0;
                    c_threshold_h_max = 241;
                    c_threshold_s_min = 0;
                    c_threshold_s_max = 16;
                    c_threshold_v_min = 249;
                    c_threshold_v_max = 255;
                    c_remove_blobs_min = 1;
                    c_remove_blobs_max = 500;
                    break;
            }
            //const int c_threshold_h_min = 21;
            //const int c_threshold_h_max = 70;
            //const int c_threshold_s_min = 100;
            //const int c_threshold_s_max = 255;
            //const int c_threshold_v_min = 100;
            //const int c_threshold_v_max = 255;
            //const int c_remove_blobs_min = 1;
            //const int c_remove_blobs_max = 500;
            


            //*******************************//
            //** Exercise:                 **//
            //**   adjust licenseplate     **//
            //**   segmentation            **//
            //*******************************//            
        
            //Find licenseplate
            HSV888Image plateImageHSV = new HSV888Image();
            //Convert to RGB to HSV
            VisionLab.FastRGBToHSV(plateImage, plateImageHSV);

            //Threshold HSV image
            VisionLab.Threshold3Channels(plateImageHSV, binaryPlateImage, c_threshold_h_min, c_threshold_h_max, c_threshold_s_min, c_threshold_s_max, c_threshold_v_min, c_threshold_v_max);
            
            //Convert to a 32 bit format 
            Int32Image binaryPlateImage32 = new Int32Image();
            VisionLab.Convert(binaryPlateImage, binaryPlateImage32);

            //Int32Image binPlateImage32 = new Int32Image();
            //.Opening(binaryPlateImage32, binPlateImage32, new Mask_Int32(10, 10, 1));
           
            //Remove blobs with small areas
            VisionLab.RemoveBlobs(binaryPlateImage32, Connected.EightConnected, BlobAnalyse.BA_Area, c_remove_blobs_min, c_remove_blobs_max);

            //Remove border blobs
            VisionLab.RemoveBorderBlobs(binaryPlateImage32, Connected.EightConnected, Border.AllBorders);

            //Length Breath Ratio
            VisionLab.RemoveBlobs(binaryPlateImage32, Connected.EightConnected, BlobAnalyse.BA_LengthBreadthRatio, 0, 2.5);
            VisionLab.RemoveBlobs(binaryPlateImage32, Connected.EightConnected, BlobAnalyse.BA_LengthBreadthRatio, 6.7, 10);

            VisionLab.RemoveBlobs(binaryPlateImage32, Connected.EightConnected, BlobAnalyse.BA_NrOfHoles, 0, 5);
            VisionLab.RemoveBlobs(binaryPlateImage32, Connected.EightConnected, BlobAnalyse.BA_AreaHoles, 0, 200);
            //VisionLab.RemoveBlobs(binaryPlateImage32, Connected.EightConnected, BlobAnalyse.BA_NrOfHoles, 20, 1000);

            //Convert back to a 16 bit format
            VisionLab.Convert(binaryPlateImage32, binaryPlateImage);

            //binPlateImage32.Dispose();
            binaryPlateImage32.Dispose();
            plateImageHSV.Dispose();

            GC.Collect();

            //Return true, if pixels found
            return (VisionLab.SumIntPixels(binaryPlateImage) > 0);
            //return VisionLab.LabelBlobs(binaryPlateImage, Connected.EightConnected) == 1;      
        }

        /*  Description:
                Locates the characters of the license plate
	                - Warp image (Rectify)
	                - Segment characters
	                - Remove blobs which are to small (Lines between characters)
            Input:
	            //Original image
	            RGB888Image plateImage
	            //Segmented license plate
	            Int16Image binaryPlateImage
            Output:
	            //Image containing binary six characters	
	            ref Int16Image binaryCharacterImage 
            Return:
	            //Function executed successfully
	            bool
        */
        public static bool FindCharacters(RGB888Image plateImage, Int16Image binaryPlateImage, ref Int16Image binaryCharacterImage)
        {
            //Constants
            const int c_height = 100;
            const int c_width = 470;
            const int c_remove_blobs_min = 1;
            const int c_remove_blobs_max = 400;

            //const int c_threshold_h_min = 21;
            //const int c_threshold_h_max = 70;
            //const int c_threshold_s_min = 100;
            //const int c_threshold_s_max = 255;
            //const int c_threshold_v_min = 100;
            //const int c_threshold_v_max = 255;


            XYCoord leftTop = new XYCoord();
            XYCoord rightTop = new XYCoord();
            XYCoord leftBottom = new XYCoord();
            XYCoord rightBottom = new XYCoord();

            XYCoord leftTop2 = new XYCoord();
            XYCoord rightTop2 = new XYCoord();
            XYCoord leftBottom2 = new XYCoord();
            XYCoord rightBottom2 = new XYCoord();

            //Find licenseplate
            Int32Image binaryPlateImage32 = new Int32Image();
            VisionLab.Convert(binaryPlateImage, binaryPlateImage32);
            
            VisionLab.FindCornersRectangle(
                binaryPlateImage32, 
                Connected.EightConnected, 
                0.5, 
                Orientation.Landscape, 
                leftTop, 
                rightTop, 
                leftBottom, 
                rightBottom
            );

            VisionLab.FindCornersRectangleSq(
                    binaryPlateImage32,
                    Connected.EightConnected,
                    leftTop2,
                    rightTop2,
                    leftBottom2,
                    rightBottom2
                );

            binaryPlateImage32.Dispose();

            Int16Image plateImageGray = new Int16Image();

            VisionLab.Convert(plateImage, plateImageGray);
            binaryCharacterImage.Assign_Op(plateImageGray);
            try
            {
                //Rectify plate
                VisionLab.Warp(
                    plateImageGray, 
                    binaryCharacterImage, 
                    TransformDirection.ForwardT, 
                    new Coord2D(leftTop), 
                    new Coord2D(rightTop), 
                    new Coord2D(leftBottom), 
                    new Coord2D(rightBottom), 
                    c_height, 
                    c_width, 
                    0
                );
            }
            catch (Exception )
            {
                //Warp, 3 coords on one line
                try
                {
                    VisionLab.Warp(plateImageGray,
                        binaryCharacterImage,
                        TransformDirection.ForwardT,
                        new Coord2D(leftTop2),
                        new Coord2D(rightTop2),
                        new Coord2D(leftBottom2),
                        new Coord2D(rightBottom2),
                        c_height,
                        c_width,
                        0
                    );
                }
                catch (Exception)
                {
                    return false;
                }                
            }

            plateImageGray.Dispose();

            //*******************************//
            //** Exercise:                 **//
            //**   adjust licenseplate     **//
            //**   segmentation            **//
            //*******************************//
            Int16Image MaxMin = new Int16Image();
            Int16Image MaxMin2 = new Int16Image();
            

            //2 x max rondje ding
            //dan 2 x min rondje ding
            //dan van elkaar aftrekken
            //(zoeken op heldere object)
            Mask_Int32 mask = new Mask_Int32(11, 11, 5, 5);
            VisionLab.MaximumFilter(binaryCharacterImage, MaxMin, FixEdge.EdgeExtend, mask);
            VisionLab.MaximumFilter(MaxMin, MaxMin2, FixEdge.EdgeExtend, mask);

            VisionLab.MinimumFilter(MaxMin2, MaxMin, FixEdge.EdgeExtend, mask);
            VisionLab.MinimumFilter(MaxMin, MaxMin2, FixEdge.EdgeExtend, mask);
            // Maxmin holds the result now of the filter oparations
            VisionLab.Subtract(binaryCharacterImage, MaxMin2);

            MaxMin2.Dispose();
            MaxMin.Dispose();

            //Find dark text on bright licenseplate using ThresholdISOData
            VisionLab.ThresholdIsoData(binaryCharacterImage, ObjectBrightness.DarkObject);

            //Convert to a 32 bit format 
            Int32Image binaryCharacterImage32 = new Int32Image();
            VisionLab.Convert(binaryCharacterImage, binaryCharacterImage32);
            
            //Remove blobs connected to the border
            VisionLab.RemoveBorderBlobs(binaryCharacterImage32, Connected.EightConnected, Border.AllBorders);
            //Remove small blobs
            VisionLab.RemoveBlobs(binaryCharacterImage32, Connected.EightConnected, BlobAnalyse.BA_Area, c_remove_blobs_min, c_remove_blobs_max);

            //Convert to a 16 bit format             
            VisionLab.Convert(binaryCharacterImage32, binaryCharacterImage);

            binaryCharacterImage32.Dispose();
            leftTop.Dispose();
            rightTop.Dispose();
            leftBottom.Dispose();
            rightBottom.Dispose();
            GC.Collect();
            return true;
        }

        /*
            Description:
                Read the license plate
            Input:
	            //Rectified license plate image containing six characters	
	            Int16Image labeledRectifiedPlateImage
	            BlobMatcher_Int16 matcher	//initialized blobmatcher
	            ClassLexicon lexicon		//initialized lexicon
            Output:
	            //Result by the blob matcher
	            ref LicensePlate result
	            //Result by the lexicon
	            ref LicensePlate lexiconResult
            Return:
	            //six characters found
	        bool 
        */
        public static bool MatchPlate(Int16Image binaryCharacterImage, BlobMatcher_Int16 matcher, ClassLexicon lexicon, ref LicensePlate result, ref LicensePlate lexiconResult)
        {
            //Check if 6 characters/blobs have been found and label image.
            if (VisionLab.LabelBlobs(binaryCharacterImage, Connected.EightConnected) != 6)
                return false;

            //Calculate dimensions and locations of all characters/blobs.
            vector_BlobAnalyse ba_vec = new vector_BlobAnalyse();
            ba_vec.Add(BlobAnalyse.BA_TopLeft);
            ba_vec.Add(BlobAnalyse.BA_Height);
            ba_vec.Add(BlobAnalyse.BA_Width);
            vector_Blob returnBlobs = new vector_Blob();
            VisionLab.BlobAnalysis(binaryCharacterImage, VisionLab.VectorToSet_BlobAnalyse(ba_vec), VisionLab.MaxPixel(binaryCharacterImage), returnBlobs, SortOrder.SortDown, BlobAnalyse.BA_TopLeft, UseXOrY.UseX);
            ba_vec.Dispose();
            Int16Image binaryCharacter = new Int16Image();

            //Create data structure for lexicon.
            vector_vector_LetterMatch match = new vector_vector_LetterMatch();
            
            //Process each character/blob.
            foreach (Blob b in returnBlobs)
            {
                //Cut out character
                VisionLab.ROI(binaryCharacterImage, binaryCharacter, b.TopLeft(), new HeightWidth(b.Height(), b.Width()));
                //Convert ROI result to binary
                VisionLab.ClipPixelValue(binaryCharacter, 0, 1);
                //Calculate character's classification for all classes.
                vector_PatternMatchResult returnMatches = new vector_PatternMatchResult();
                float conf = matcher.AllMatches(binaryCharacter, (float)-0.5, (float)0.5, returnMatches);
                float err = returnMatches[0].error;
                int id = returnMatches[0].id;
                string chr = matcher.PatternName(id);
                
                //Fill datastructure for lexicon.
                match.Add(VisionLabEx.PatternMatchResultToLetterMatch(returnMatches));
                
                //Store best match in result
                result.characters.Add(new LicenseCharacter(chr, err, conf));
            }

            //Validate match with lexicon.
            vector_int bestWord = new vector_int();
            lexiconResult.confidence = lexicon.FindBestWord(match, bestWord, Optimize.OptimizeForMinimum);
            for (int i = 0; i < bestWord.Count; i++)
            {
                string character = matcher.PatternName(bestWord[i]);
                //Store lexicon result
                lexiconResult.characters.Add(new LicenseCharacter(character));
            }

            binaryCharacter.Dispose();
            returnBlobs.Dispose();
            match.Dispose();
            bestWord.Dispose();
            GC.Collect();
            return true;
        }
    }
}

