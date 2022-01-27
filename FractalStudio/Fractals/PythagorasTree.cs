using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    public class PythagorasTree : Fractal
    {
        public PythagorasTree(Color startColor, Color endColor, int maxRecursion, int gradientLength) : base(startColor, endColor, maxRecursion, gradientLength)
        {
            
        }

        public override void Draw(
            Canvas canvas,
            int x, 
            int y, 
            double length, double angle, 
            double lengthRatio,
            double angleRatioLeft, 
            double angleRatioRight,
            int step = 0)
        {
            if (step >= MaxRecursion) return;

            var (x1, y1) = (x + length * Math.Cos(angle), y - length * Math.Sin(angle));

            var line = new Line
            {
                X1 = x,
                Y1 = y,
                X2 = x1,
                Y2 = y1,
                Stroke = new SolidColorBrush(Gradient[step]),
                StrokeThickness = (double)(MaxRecursion - step) / 2
            };
            canvas.Children.Add(line);
            
            Draw(canvas, (int)x1, (int)y1, length * lengthRatio, angle + Math.PI * angleRatioLeft, 
                lengthRatio, angleRatioLeft, angleRatioRight, step + 1);
            Draw(canvas, (int)x1, (int)y1, length * lengthRatio, angle - Math.PI * angleRatioRight, 
                lengthRatio, angleRatioLeft, angleRatioRight, step + 1);
        }
    }
}