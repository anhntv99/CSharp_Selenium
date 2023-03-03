using Codeuctivity.ImageSharpCompare;
using CoreFramework.Configs;
using CoreFramework.DriverCore;
using CoreFramework.Reporter;
using Emgu.CV;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using Image = SixLabors.ImageSharp.Image;
using Point = SixLabors.ImageSharp.Point;

namespace CoreFramework.Utilities
{
    public class ImageProcessor
    {     
        public static Image<Rgba32> ClearBlackBackGround(Image<Rgba32> imageRgba32)
        {
            float threshold = 0;
            Color sourceColor = Color.Black;
            Color targetColor = Color.Transparent;
            RecolorBrush brush = new RecolorBrush(sourceColor, targetColor, threshold);

            DrawingOptions drawingOptions = new DrawingOptions
            {
                GraphicsOptions = new GraphicsOptions
                {
                    AlphaCompositionMode = PixelAlphaCompositionMode.Src,
                    ColorBlendingMode = PixelColorBlendingMode.Normal
                }
            };

            imageRgba32.Mutate(x => x.Fill(drawingOptions, brush));
            return imageRgba32;
        }

        public static Image<Rgba32> ConvertRgb24ToRgba32(Image<Rgb24> imageRgb24)
        {
            var maskRgba32 = new Image<Rgba32>(imageRgb24.Width, imageRgb24.Height);
            for (var x = 0; x < imageRgb24.Width; x++)
            {
                for (var y = 0; y < imageRgb24.Height; y++)
                {
                    var pixel = new Rgba32();
                    pixel.FromRgb24(imageRgb24[x, y]);
                    maskRgba32[x, y] = pixel;
                }
            }
            return maskRgba32;
        }

        public static void HighlightDifferenceImage(string baselinepath, string actualpath, string mergepath) 
        {
            Image<Rgb24> maskImage24 = (Image<Rgb24>)ImageSharpCompare.CalcDiffMaskImage(actualpath, baselinepath);
            Image<Rgba32> rgba32Mask = ImageProcessor.ConvertRgb24ToRgba32(maskImage24);
            Image<Rgba32> clearedMask = ImageProcessor.ClearBlackBackGround(rgba32Mask);

            Image<Rgba32> imgBgr = Image.Load<Rgba32>(baselinepath);
            Image<Rgba32> outputImage = new Image<Rgba32>(imgBgr.Width, imgBgr.Height);
            outputImage.Mutate(o => o
                .DrawImage(imgBgr, new Point(0, 0), 1f)
                .DrawImage(clearedMask, new Point(0, 0), 1f));
            outputImage.Save(mergepath);
        }

        public static double CompareImage(string baselinepath, string actualpath, string mergepath, double exprate)
        {          
            ICompareResult compareResult = ImageSharpCompare.CalcDiff(actualpath, baselinepath);
            double similarRate = 100 - compareResult.PixelErrorPercentage;

            if (similarRate > exprate)
            {
                return similarRate;
            }
            else
            {
                HighlightDifferenceImage(baselinepath, actualpath, mergepath);
                return similarRate;
            }

        }


    }


}