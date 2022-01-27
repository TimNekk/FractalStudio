using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    public class SierpinskiCarpet : Fractal
    {
        private readonly LinearGradientBrush _gradientBrush;
        
        public SierpinskiCarpet(Color startColor, Color endColor, int maxRecursion) : base(startColor, endColor, maxRecursion, 2)
        {
            _gradientBrush = new LinearGradientBrush();
            _gradientBrush.StartPoint = new Point(0, 0);
            _gradientBrush.EndPoint = new Point(1, 1);
            _gradientBrush.GradientStops.Add(new GradientStop(startColor, 0));
            _gradientBrush.GradientStops.Add(new GradientStop(endColor, 1));
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
            if (step >= MaxRecursion) return;

            var square = new Rectangle
            {
                Width = length,
                Height = length,
                Fill = _gradientBrush
            };
            Canvas.SetTop(square, y);
            Canvas.SetLeft(square, x);
            canvas.Children.Insert(0, square);

            int newLength = (int) length / 3;
            DrawHoles(canvas, x + newLength, y + newLength, newLength);
        }
        
        private void DrawHoles(
            Canvas canvas,
            int x,
            int y,
            double length,
            int step = 0)
        {
            if (step >= MaxRecursion) return;
            
            var square = new Rectangle
            {
                Width = length,
                Height = length,
                // ReSharper disable once PossibleNullReferenceException
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0f142b"))
            };
            Canvas.SetTop(square, y);
            Canvas.SetLeft(square, x);
            canvas.Children.Insert(step + 1, square);
            
            int newLength = (int) length / 3;
            int newLengthX2 = newLength * 2;
            int firstColumn = x + newLength + (int) length;
            int secondColumn = x + newLength;
            int thirdColumn = x - newLengthX2;
            int firstRow = y - newLengthX2;
            int secondRow = y + newLength;
            int thirdRow = y + newLength + (int)length;
            
            DrawHoles(canvas, firstColumn, secondRow, newLength, step + 1);
            DrawHoles(canvas, firstColumn, firstRow, newLength, step + 1);
            DrawHoles(canvas, firstColumn, thirdRow, newLength, step + 1);

            DrawHoles(canvas, secondColumn, firstRow, newLength, step + 1);
            DrawHoles(canvas, secondColumn, thirdRow, newLength, step + 1);

            DrawHoles(canvas, thirdColumn, secondRow, newLength, step + 1);
            DrawHoles(canvas, thirdColumn, firstRow, newLength, step + 1);
            DrawHoles(canvas, thirdColumn, thirdRow, newLength, step + 1);
        }
    }
}