using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    public class JuliaSet : Fractal
    {
        private readonly double _length;
        private readonly double _cRe;
        private readonly double _cIm;
        private readonly double _moveX;
        private readonly double _moveY;
        private WriteableBitmap _canvas;
        private readonly Image _img;

        public JuliaSet(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient, Image img) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            _length = 80;
            UpdateGradientDepth();
            _moveX = 1;
            _moveY = 0;
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
                        
                        switch (_spacing)
                        {
                            case 0:
                            {
                                newRe = oldRe * oldRe - oldIm * oldIm + _angleRatioLeft;
                                newIm = 2 * oldRe * oldIm + _angleRatioRight;
                                break;
                            }
                            case 1:
                            {
                                newRe = Math.Pow(oldRe * oldRe - oldIm * oldIm, 2) + _angleRatioLeft;
                                newIm = Math.Pow(2 * oldRe * oldIm, 2) + _angleRatioRight;
                                break;
                            }
                            case 2:
                            {
                                newRe = Math.Sin(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft;
                                newIm = Math.Sin(2 * oldRe * oldIm) + _angleRatioRight;
                                break;
                            }
                            case 3:
                            {
                                newRe = Math.Cos(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft;
                                newIm = Math.Cos(2 * oldRe * oldIm) + _angleRatioRight;
                                break;
                            }
                            case 4:
                            {
                                newRe = Math.Cos(oldRe * oldRe - oldIm * oldIm) * Math.Sin(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft;
                                newIm = Math.Cos(2 * oldRe * oldIm) * Math.Sin(2 * oldRe * oldIm) + _angleRatioRight;
                                break;
                            }
                            case 5:
                            {
                                newRe = Math.Atan(oldRe * oldRe - oldIm * oldIm) + _angleRatioLeft;
                                newIm = Math.Atan(2 * oldRe * oldIm) + _angleRatioRight;
                                break;
                            }
                        }

                        if (newRe * newRe + newIm * newIm > 4) break;
                    }

                    _canvas.SetPixel(x, y, i == _recursion ? Colors.Black : _gradient[i]);
                }
            }
        }
    }
}