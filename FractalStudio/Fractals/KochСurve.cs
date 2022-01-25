using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio
{
    public class KochСurve : Fractal
    {
        private static int _n;
        
        public KochСurve(Color startColor, Color endColor, int maxRecursion, int gradientLength) : base(startColor, endColor, maxRecursion, gradientLength)
        {
            
        }

        public override void Draw(Canvas canvas, int x, int y, double length, double angle, double lengthRatio, double angleRatioLeft, double angleRatioRight, int step = 0)
        {
            DrawCurve(canvas, x, y, (int)(x + length), y, step);
        }

        private void DrawCurve(Canvas canvas, int stX, int stY, int enX, int enY, int step = 0)
        {
            if (step >= MaxRecursion) return;
            if (step == MaxRecursion - 1)
            {
                var line = new Line {
                    X1 = stX, Y1 = stY, X2 = enX, Y2 = enY, 
                    Stroke = new SolidColorBrush(Gradient[_n]), StrokeThickness = 1
                };
                canvas.Children.Add(line);
                _n++;
            }

            var (mlX, mlY, mrX, mrY) = (stX + (enX - stX) / 3, stY + (enY - stY) / 3, stX + (enX - stX) * 2 / 3, stY + (enY - stY) * 2 / 3);
            var (mX, mY) = ((mlX + mrX) / 2, (mlY + mrY) / 2);
            int length = (int) Math.Sqrt(Math.Pow(mrX - mlX, 2) + Math.Pow(mrY - mlY, 2));
            int mhX, mhY;

            if (mlX < mrX)
            {
                if (mlY > mrY) (mhX, mhY) = (mlX - length / 2, mrY);
                else if (mlY == mrY) (mhX, mhY) = (mX, (int) (mY - Math.Sin(Math.PI / 3) * length));
                else (mhX, mhY) = (mrX + length / 2, mlY);
            }
            else
            {
                if (mlY < mrY) (mhX, mhY) = (mlX + length / 2, mrY);
                else if (mlY == mrY) (mhX, mhY) = (mX, (int) (mY + Math.Sin(Math.PI / 3) * length));
                else (mhX, mhY) = (mrX - length / 2, mlY);
            }
            
            DrawCurve(canvas, stX, stY, mlX, mlY, step + 1);
            DrawCurve(canvas, mlX, mlY, mhX, mhY, step + 1);
            DrawCurve(canvas, mhX, mhY, mrX, mrY, step + 1);
            DrawCurve(canvas, mrX, mrY, enX, enY, step + 1);
        }
    }
}