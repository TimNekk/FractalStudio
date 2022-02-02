using System.Windows.Controls;

namespace FractalStudio.Fractals
{
    public abstract class Fractal
    {
        private protected readonly Canvas _canvas;
        
        private protected int _recursion;

        public int Recursion
        {
            get => _recursion;
            set
            {
                _recursion = value;
                UpdateGradientDepth();
                Draw();
            }
        }
        
        private protected Gradient _gradient;

        public Gradient Gradient
        {
            get => _gradient;
            set
            {
                _gradient = value;
                UpdateGradientDepth();
                Draw();
            }
        }
        
        private protected double _lengthRatio;
        
        public double LengthRatio
        {
            get => _lengthRatio;
            set
            {
                _lengthRatio = value;
                Draw();
                }
        }
        
        private protected double _angleRatioLeft;
        
        public double AngleRatioLeft
        {
            get => _angleRatioLeft;
            set
            {
                _angleRatioLeft = value;
                Draw();
            }
        }
        
        private protected double _angleRatioRight;
        
        public double AngleRatioRight
        {
            get => _angleRatioRight;
            set
            {
                _angleRatioRight = value;
                Draw();
            }
        }
        
        private protected double _scale;

        public double Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                Draw();
            }
        }
        
        private protected double _spacing;

        public double Spacing
        {
            get => _spacing;
            set
            {
                _spacing = value;
                Draw();
            }
        }
        
        private protected int _x;

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                Draw();
            }
        }
        
        private protected int _y;

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                Draw();
            }
        }
        
        protected Fractal(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio, 
            double angleRatioLeft, double angleRatioRight, double spacing, Gradient gradient)
        {
            _canvas = canvas;
            _recursion = recursion;
            _x = x;
            _y = y;
            _scale = scale;
            _gradient = gradient;
            _lengthRatio = lengthRatio;
            _angleRatioLeft = angleRatioLeft;
            _angleRatioRight = angleRatioRight;
            _spacing = spacing;
        }

        public abstract void Draw();
        
        protected abstract void UpdateGradientDepth();
    }
}