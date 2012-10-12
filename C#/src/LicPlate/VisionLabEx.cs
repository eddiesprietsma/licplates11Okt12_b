using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace LicPlate {
    public class VisionLabEx
    {

        [DllImport("msvcrt.dll", EntryPoint = "memcpy")]
        public unsafe static extern void CopyMemory(IntPtr pDest, IntPtr pSrc, int length);

        public static RGB888Image BitmapToJL(Bitmap bm)
        {
            RGB888Image vlimage;
            BitmapData bmdata = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            try
            {
                vlimage = new RGB888Image(bmdata.Height, bm.Width);
                CopyMemory(vlimage.GetBufPtr(), bmdata.Scan0, bm.Width * bm.Height * 4 /*RGB8*/);
            }
            finally
            {
                bm.UnlockBits(bmdata);
            }
            return vlimage;
        }

        public static Bitmap JLToBitmap(Image img)
        {
            Bitmap bm = new Bitmap(img.GetWidth(), img.GetHeight(), PixelFormat.Format32bppRgb);
            BitmapData bmdata = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
            try
            {
                RGB888Image vlimage = new RGB888Image();
                VisionLab.Convert(img, vlimage);
                CopyMemory(bmdata.Scan0, vlimage.GetBufPtr(), vlimage.GetWidth() * vlimage.GetHeight() * 4 /*RGB8*/);
                vlimage.Dispose();
            }
            finally
            {
                bm.UnlockBits(bmdata);
            }
            return bm;
        }

        public static vector_LetterMatch PatternMatchResultToLetterMatch(vector_PatternMatchResult vpmr)
        {
            vector_LetterMatch ret = new vector_LetterMatch();
            for (int i = 0; i < vpmr.Count; i++)
            {
                ret.Add(PatternMatchResultToLetterMatch(vpmr[i]));
            }
            return ret;
        }
        public static LetterMatch PatternMatchResultToLetterMatch(PatternMatchResult pmr)
        {
            return new LetterMatch(pmr.id, pmr.error);
        }
    }
}