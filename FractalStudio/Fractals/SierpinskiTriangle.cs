using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    public class SierpinskiTriangle : Fractal
    {
        private readonly int _strokeThickness;
        
        public SierpinskiTriangle(Color startColor, Color endColor, int maxRecursion, int gradientLength) : base(startColor, endColor, maxRecursion, gradientLength)
        {
            _strokeThickness = 2;
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
            double height = length * Math.Sqrt(3) / 2;
            var (x1, y1) = (x + length, y);
            var (x2, y2) = (x + length / 2, y - height);
            
            var line1 = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x1, Y2 = y1,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            canvas.Children.Add(line1);
            
            var line2 = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            canvas.Children.Add(line2);
            
            var line3 = new Line
            {
                X1 = x1, Y1 = y1, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            canvas.Children.Add(line3);

            int newLength = (int) length / 2;
            DrawTriangle(canvas, x + newLength, y, newLength);
        }
        
        private void DrawTriangle(
            Canvas canvas,
            int x,
            int y,
            double length,
            int step = 0)
        {
            if (step >= MaxRecursion) return;

            int height = (int)(length * Math.Sqrt(3) / 2);
            var (x1, y1) = (x - (int)length / 2, y - height);
            var (x2, y2) = (x + (int)length / 2, y - height);
            
            var line1 = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x1, Y2 = y1,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            canvas.Children.Add(line1);
            
            var line2 = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            canvas.Children.Add(line2);
            
            var line3 = new Line
            {
                X1 = x1, Y1 = y1, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            canvas.Children.Add(line3);
            
            int newLength = (int) length / 2;
            DrawTriangle(canvas, x1, y, newLength, step + 1);
            DrawTriangle(canvas, x2, y, newLength, step + 1);
            DrawTriangle(canvas, x, y1, newLength, step + 1);
        }
    }
}