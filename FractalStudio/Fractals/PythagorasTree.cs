using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    /// <summary>
    /// Pythagoras Tree
    /// </summary>
    public class PythagorasTree : Fractal
    {
        private readonly double _length;
        private readonly double _angle;

        /// <summary>
        /// Pythagoras Tree constructor
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
        public PythagorasTree(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            _length = 80;
            _angle = 1.57;
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
            DrawTree(_x, _y, _length * _scale, _angle, _lengthRatio, _angleRatioLeft, _angleRatioRight);
        }

        /// <summary>
        /// Draws part of tree
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="length">Length of line</param>
        /// <param name="angle">Angle of line</param>
        /// <param name="lengthRatio">Ratio of previous and current lengths</param>
        /// <param name="angleRatioLeft">Left angle added value</param>
        /// <param name="angleRatioRight">Right angle added value</param>
        /// <param name="step">Current recursion step</param>
        private void DrawTree(int x, int y, double length, double angle, double lengthRatio, double angleRatioLeft, double angleRatioRight, int step = 0)
        {
            if (step >= _recursion) return;

            var (x1, y1) = (x + length * Math.Cos(angle), y - length * Math.Sin(angle));
            
            var line = new Line
            {
                X1 = x,
                Y1 = y,
                X2 = x1,
                Y2 = y1,
                Stroke = new SolidColorBrush(_gradient[step]),
                StrokeThickness = _recursion - step
            };
            _canvas.Children.Add(line);

            DrawTree((int)x1, (int)y1, length * lengthRatio, angle + Math.PI * angleRatioLeft, 
                lengthRatio, angleRatioLeft, angleRatioRight, step + 1);
            DrawTree((int)x1, (int)y1, length * lengthRatio, angle - Math.PI * angleRatioRight, 
                lengthRatio, angleRatioLeft, angleRatioRight, step + 1);
        }
    }
}