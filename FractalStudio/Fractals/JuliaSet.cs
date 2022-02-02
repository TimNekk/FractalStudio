using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FractalStudio.Fractals
{
    /// <summary>
    /// Julia Set fractal
    /// </summary>
    public class JuliaSet : Fractal
    {
        private new WriteableBitmap _canvas;
        private readonly Image _img;

        /// <summary>
        /// Julia Set constructor
        /// </summary>
        /// <param name="canvas">Drawing canvas</param>
        /// <param name="recursion">Depth of recursion</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="scale">Fractal scale</param>
        /// <param name="lengthRatio">Length ration</param>
        /// <param name="angleRatioLeft">Left angle ratio</param>
        /// <param name="angleRatioRight">Right angle ratio</param>
        /// <param name="spacing">Spacing between elements</param>
        /// <param name="gradient">Gradient</param>
        /// <param name="img">Image with canvas</param>
        public JuliaSet(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient, Image img) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            UpdateGradientDepth();
            _img = img;
        }

        /// <summary>
        /// Updates gradient depth
        /// </summary>
        protected sealed override void UpdateGradientDepth()
        {
            _gradient.Length = _recursion + 1;
        }

        /// <summary>
        /// Draws fractal
        /// </summary>
        public override void Draw()
        {
            _canvas = BitmapFactory.New((int) _lengthRatio, (int) _lengthRatio);
            _img.Source = _canvas;
            DrawSet((int)_lengthRatio, (int)_lengthRatio);
        }

        /// <summary>
        /// Draws set
        /// </summary>
        /// <param name="width">Amount of pixels in width</param>
        /// <param name="height">Amount of pixels in height</param>
        private void DrawSet(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double  newRe = 1.5 * (x - width / 2) / (0.5 * _scale * width) - (double)_x / 1000;
                    double newIm = (y - height / 2) / (0.5 * _scale * height) - (double)_y / 1000;
                    
                    int step;
                    for (step = 0; step < _recursion; step++)
                    {
                        double oldRe = newRe;
                        double oldIm = newIm;

                        (newRe, newIm) = GenerateNewValues(oldRe, oldIm);

                        if (newRe * newRe + newIm * newIm > 4) break;
                    }

                    _canvas.SetPixel(x, y, step == _recursion ? Colors.Black : _gradient[step]);
                }
            }
        }
        
        /// <summary>
        /// Generates new values
        /// </summary>
        /// <param name="oldRe"></param>
        /// <param name="oldIm"></param>
        /// <returns>Re and Im values</returns>
        private (double, double) GenerateNewValues(double oldRe, double oldIm)
        {
            switch (_spacing)
                        {
                            case 0:
                            {
                                return (oldRe * oldRe - oldIm * oldIm + _angleRatioLeft, 2 * oldRe * oldIm + _angleRatioRight);
                            }
                            case 1:
                            {
                                return (Math.Pow(oldRe * oldRe - oldIm * oldIm, 2) + _angleRatioLeft, Math.Pow(2 * oldRe * oldIm, 2) + _angleRatioRight);
                            }
                            case 2:
                            {
                                return (Math.Sin(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft, Math.Sin(2 * oldRe * oldIm) + _angleRatioRight);
                            }
                            case 3:
                            {
                                return (Math.Cos(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft, Math.Cos(2 * oldRe * oldIm) + _angleRatioRight);
                            }
                            case 4:
                            {
                                return (Math.Cos(oldRe * oldRe - oldIm * oldIm) * Math.Sin(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft, 
                                    Math.Cos(2 * oldRe * oldIm) * Math.Sin(2 * oldRe * oldIm) + _angleRatioRight);
                            }
                            case 5:
                            {
                                return (Math.Atan(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft, Math.Atan(2 * oldRe * oldIm) + _angleRatioRight);
                            }
                        }
            return (0, 0);
        }
    }
}