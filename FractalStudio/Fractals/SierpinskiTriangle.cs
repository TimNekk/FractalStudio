using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    /// <summary>
    /// Sierpinski Triangle
    /// </summary>
    public class SierpinskiTriangle : Fractal
    {
        private readonly int _strokeThickness;
        private readonly double _length;
        
        /// <summary>
        /// Sierpinski Triangle constructor
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
        public SierpinskiTriangle(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            _length = 300;
            _strokeThickness = 2;
            UpdateGradientDepth();
        }
        
        /// <summary>
        /// Updates gradient depth
        /// </summary>
        protected sealed override void UpdateGradientDepth()
        {
            _gradient.Length = _recursion;
        }

        /// <summary>
        /// Draws fractal
        /// </summary>
        public override void Draw()
        {
            _canvas.Children.Clear();

            double height = _length * _scale * Math.Sqrt(3) / 2;
            var (x1, y1) = (_x + _length * _scale, _y);
            var (x2, y2) = (_x + _length * _scale / 2, _y - height);
            
            var line1 = new Line
            {
                X1 = _x, Y1 = _y, 
                X2 = x1, Y2 = y1,
                Stroke = new SolidColorBrush(Gradient[0]), StrokeThickness = _strokeThickness
            };
            _canvas.Children.Add(line1);
            
            var line2 = new Line
            {
                X1 = _x, Y1 = _y, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[0]), StrokeThickness = _strokeThickness
            };
            _canvas.Children.Add(line2);
            
            var line3 = new Line
            {
                X1 = x1, Y1 = y1, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[0]), StrokeThickness = _strokeThickness
            };
            _canvas.Children.Add(line3);

            int newLength = (int) (_length * _scale / 2);
            
            if (_recursion > 1) DrawTriangle(_x + newLength, _y, newLength);
        }
        
        /// <summary>
        /// Draws triangle
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="length">Length of triangle side</param>
        /// <param name="step">Current recursion step</param>
        private void DrawTriangle(
            int x,
            int y,
            double length,
            int step = 0)
        {
            if (step >= _recursion - 1) return;

            int height = (int)(length * Math.Sqrt(3) / 2);
            var (x1, y1) = (x - (int)length / 2, y - height);
            var (x2, y2) = (x + (int)length / 2, y - height);
            
            var line1 = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x1, Y2 = y1,
                Stroke = new SolidColorBrush(Gradient[step + 1]), StrokeThickness = _strokeThickness
            };
            _canvas.Children.Add(line1);
            
            var line2 = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[step + 1]), StrokeThickness = _strokeThickness
            };
            _canvas.Children.Add(line2);
            
            var line3 = new Line
            {
                X1 = x1, Y1 = y1, 
                X2 = x2, Y2 = y2,
                Stroke = new SolidColorBrush(Gradient[step + 1]), StrokeThickness = _strokeThickness
            };
            _canvas.Children.Add(line3);
            
            int newLength = (int) length / 2;
            DrawTriangle(x1, y, newLength, step + 1);
            DrawTriangle(x2, y, newLength, step + 1);
            DrawTriangle(x, y1, newLength, step + 1);
        }
    }
}