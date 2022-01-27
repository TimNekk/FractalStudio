using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    public class CantorSet : Fractal
    {
        private readonly int _strokeThickness;
        private readonly int _spacing;
        
        public CantorSet(Color startColor, Color endColor, int maxRecursion, int gradientLength) : base(startColor, endColor, maxRecursion, gradientLength)
        {
            _strokeThickness = 10;
            _spacing = 35;
        }

        public override void Draw(
            Canvas canvas,
            int x,
            int y,
            double length, 
            double angle,
            double lengthRatio,
            double angleRatioLeft, double angleRatioRight,
            int step = 0)
        {
            DrawLine(canvas, x, y, length);
        }
        
        private void DrawLine(
            Canvas canvas,
            int x,
            int y,
            double length,
            int step = 0)
        {
            if (step >= MaxRecursion) return;

            var line = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x + length, Y2 = y,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            canvas.Children.Add(line);
            
            int newLength = (int) length / 3;
            DrawLine(canvas, x, y + _spacing, newLength, step + 1);
            DrawLine(canvas, x + (int)length * 2 / 3, y + _spacing, newLength, step + 1);
        }
    }
}