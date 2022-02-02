using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FractalStudio.Fractals
{
    public class JuliaSet : Fractal
    {
        private new WriteableBitmap _canvas;
        private readonly Image _img;

        public JuliaSet(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient, Image img) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            UpdateGradientDepth();
            _img = img;
        }

        protected sealed override void UpdateGradientDepth()
        {
            _gradient.Length = _recursion + 1;
        }

        public override void Draw()
        {
            _canvas = BitmapFactory.New((int) _lengthRatio, (int) _lengthRatio);
            _img.Source = _canvas;
            DrawTree((int)_lengthRatio, (int)_lengthRatio);
        }

        private void DrawTree(int w, int h)
        {
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    double  newRe = 1.5 * (x - w / 2) / (0.5 * _scale * w) - (double)_x / 1000;
                    double newIm = (y - h / 2) / (0.5 * _scale * h) - (double)_y / 1000;
                    
                    int i;
                    for (i = 0; i < _recursion; i++)
                    {
                        double oldRe = newRe;
                        double oldIm = newIm;

                        (newRe, newIm) = GenerateNewValues(oldRe, oldIm);

                        if (newRe * newRe + newIm * newIm > 4) break;
                    }

                    _canvas.SetPixel(x, y, i == _recursion ? Colors.Black : _gradient[i]);
                }
            }
        }

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