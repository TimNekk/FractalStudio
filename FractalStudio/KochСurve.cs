using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio
{
    public class KochСurve : Fractal
    {
        public KochСurve(Color startColor, Color endColor, int maxRecursion) : base(startColor, endColor, maxRecursion)
        {
            
        }

        public override void Draw(Canvas canvas, int x, int y, double length, double angle, double lengthRatio, double angleRatioLeft, double angleRatioRight, int step = 0)
        {
            Draw2(canvas, x, y, (int)(x + length), y, step);
        }

        private void Draw2(Canvas canvas, int stX, int stY, int enX, int enY, int step = 0)
        {
            if (step >= MaxRecursion) return;

            if (step == MaxRecursion - 1)
            {
                var line = new Line
                {
                    X1 = stX,
                    Y1 = stY,
                    X2 = enX,
                    Y2 = enY,
                    Stroke = new SolidColorBrush(Gradient[step]),
                    StrokeThickness = 4
                };
                canvas.Children.Add(line);
            }

            int mlX = stX + (enX - stX) / 3;
            int mlY = stY + (enY - stY) / 3;
            
            int mrX = stX + (enX - stX) * 2 / 3;
            int mrY = stY + (enY - stY) * 2 / 3;

            int mX = (mlX + mrX) / 2;
            int mY = (mlY + mrY) / 2;

            int mhX = mX;
            int mhY = (int)(mY - Math.Sin(Math.PI / 3) * Math.Sqrt(Math.Pow(mrX - mlX, 2) + Math.Pow(mrY - mlY, 2)));

            Draw2(canvas, stX, stY, mlX, mlY, step + 1);
            Draw2(canvas, mlX, mlY, mhX, mhY, step + 1);
            Draw2(canvas, mhX, mhY, mrX, mrY, step + 1);
            Draw2(canvas, mrX, mrY, enX, enY, step + 1);
        }
    }
}