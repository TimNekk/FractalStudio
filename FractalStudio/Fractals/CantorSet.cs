using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    /// <summary>
    /// Cantor Set fractal
    /// </summary>
    public class CantorSet : Fractal
    {
        private readonly int _strokeThickness;
        private readonly double _length;
        
        /// <summary>
        /// CantorSet constructor
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
        public CantorSet(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            _length = 1000;
            _strokeThickness = 10;
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
            DrawLine(_x, _y, _length * _scale);

        }

        /// <summary>
        /// Draws line of fractal
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="length">Length of line</param>
        /// <param name="step">Current recursion step</param>
        private void DrawLine(
            int x,
            int y,
            double length,
            int step = 0)
        {
            if (step >= _recursion) return;

            var line = new Line
            {
                X1 = x, Y1 = y, 
                X2 = x + length, Y2 = y,
                Stroke = new SolidColorBrush(Gradient[step]), StrokeThickness = _strokeThickness
            };
            _canvas.Children.Add(line);
            
            int newLength = (int) length / 3;
            DrawLine(x, y + (int)_spacing, newLength, step + 1);
            DrawLine(x + (int)length * 2 / 3, y + (int)_spacing, newLength, step + 1);
        }
    }
}