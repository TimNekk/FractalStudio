using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio.Fractals
{
    public class KochСurve : Fractal
    {
        private int _sideIndex;
        private readonly double _length;

        public KochСurve(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio,
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient) :
            base(canvas, recursion, x, y, scale, lengthRatio, angleRatioLeft, angleRatioRight, spacing, gradient)
        {
            _length = 1000;
            UpdateGradientDepth();
        }
        
        protected sealed override void UpdateGradientDepth()
        {
            _gradient.Length = (int)Math.Pow(4, _recursion - 1);
        }

        public override void Draw()
        {
            _canvas.Children.Clear();
            _sideIndex = 0;
            DrawCurve(_x, _y, (int)(_x + _length * _scale), _y);
        }

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