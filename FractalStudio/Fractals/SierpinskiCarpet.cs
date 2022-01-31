using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    public class SierpinskiCarpet : Fractal
    {
        private readonly double _length;

        public SierpinskiCarpet(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            _length = 500;
            UpdateGradientDepth();
        }
        
        protected sealed override void UpdateGradientDepth()
        {
            _gradient.Length = 2;
        }

        public override void Draw()
        {
            _canvas.Children.Clear();
            
            var gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1)
            };
            gradientBrush.GradientStops.Add(new GradientStop(_gradient[0], 0));
            gradientBrush.GradientStops.Add(new GradientStop(_gradient[1], 1));
            
            var square = new Rectangle
            {
                Width = _length * _scale,
                Height = _length * _scale,
                Fill = gradientBrush
            };
            Canvas.SetTop(square, _y);
            Canvas.SetLeft(square, _x);
            _canvas.Children.Insert(0, square);

            int newLength = (int) (_length * _scale / 3);
            DrawHoles(_x + newLength, _y + newLength, newLength);
        }

        private void DrawHoles(
            int x,
            int y,
            double length,
            int step = 0)
        {
            if (step >= _recursion) return;
            
            var square = new Rectangle
            {
                Width = length,
                Height = length,
                // ReSharper disable once PossibleNullReferenceException
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0f142b"))
            };
            Canvas.SetTop(square, y);
            Canvas.SetLeft(square, x);
            _canvas.Children.Insert(step + 1, square);
            
            int newLength = (int) length / 3;
            int newLengthX2 = newLength * 2;
            int firstColumn = x + newLength + (int) length;
            int secondColumn = x + newLength;
            int thirdColumn = x - newLengthX2;
            int firstRow = y - newLengthX2;
            int secondRow = y + newLength;
            int thirdRow = y + newLength + (int)length;
            
            DrawHoles(firstColumn, secondRow, newLength, step + 1);
            DrawHoles(firstColumn, firstRow, newLength, step + 1);
            DrawHoles(firstColumn, thirdRow, newLength, step + 1);

            DrawHoles(secondColumn, firstRow, newLength, step + 1);
            DrawHoles(secondColumn, thirdRow, newLength, step + 1);

            DrawHoles(thirdColumn, secondRow, newLength, step + 1);
            DrawHoles(thirdColumn, firstRow, newLength, step + 1);
            DrawHoles(thirdColumn, thirdRow, newLength, step + 1);
        }
    }
}