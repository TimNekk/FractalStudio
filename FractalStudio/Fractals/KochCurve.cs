using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    /// <summary>
    /// Koch Curve fractal
    /// </summary>
    public class KochСurve : Fractal
    {
        private int _sideIndex;
        private readonly double _length;

        /// <summary>
        /// Koch Curve constructor
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
        public KochСurve(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            _length = 1000;
            UpdateGradientDepth();
        }
        
        /// <summary>
        /// Updates gradient depth
        /// </summary>
        protected sealed override void UpdateGradientDepth()
        {
            _gradient.Length = (int)Math.Pow(4, _recursion - 1);
        }

        /// <summary>
        /// Draws fractal
        /// </summary>
        public override void Draw()
        {
            _canvas.Children.Clear();
            _sideIndex = 0;
            DrawCurve(_x, _y, (int)(_x + _length * _scale), _y);
        }

        /// <summary>
        /// Draws part of the curve
        /// </summary>
        /// <param name="stX">Start X</param>
        /// <param name="stY">Start Y</param>
        /// <param name="enX">End X</param>
        /// <param name="enY">End Y</param>
        /// <param name="step">Current recursion step</param>
        private void DrawCurve(int stX, int stY, int enX, int enY, int step = 0)
        {
            if (step >= _recursion) return;
            if (step == _recursion - 1)
            {
                var line = new Line {
                    X1 = stX, Y1 = stY, X2 = enX, Y2 = enY, 
                    Stroke = new SolidColorBrush(Gradient[_sideIndex]), StrokeThickness = 1
                };
                _canvas.Children.Add(line);
                _sideIndex++;
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
            
            DrawCurve(stX, stY, mlX, mlY, step + 1);
            DrawCurve(mlX, mlY, mhX, mhY, step + 1);
            DrawCurve(mhX, mhY, mrX, mrY, step + 1);
            DrawCurve(mrX, mrY, enX, enY, step + 1);
        }
    }
}