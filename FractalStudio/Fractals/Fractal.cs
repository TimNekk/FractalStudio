using System.Windows.Controls;

namespace FractalStudio.Fractals
{
    /// <summary>
    /// Fractal
    /// </summary>
    public abstract class Fractal
    {
        private protected readonly Canvas _canvas;
        
        private protected int _recursion;

        /// <summary>
        /// Depth of recursion
        /// </summary>
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

        /// <summary>
        /// Gradient
        /// </summary>
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
        
        /// <summary>
        /// Length ration
        /// </summary>
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
        
        /// <summary>
        /// Left angle ratio
        /// </summary>
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
        
        /// <summary>
        /// Right angle ratio
        /// </summary>
        public double AngleRatioRight
        {
            get => _angleRatioRight;
            set
            {
                _angleRatioRight = value;
                Draw();
            }
        }
        
        /// <summary>
        /// Scale
        /// </summary>
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

        /// <summary>
        /// Spacing between elements
        /// </summary>
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

        /// <summary>
        /// X coordinate
        /// </summary>
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

        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                Draw();
            }
        }
        
        /// <summary>
        /// Fractal constructor
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
        private protected Fractal(Canvas canvas, int recursion, int x, int y, double scale, double lengthRatio, 
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

        /// <summary>
        /// Draws fractal
        /// </summary>
        public abstract void Draw();
        
        /// <summary>
        /// Updates gradient depth
        /// </summary>
        protected abstract void UpdateGradientDepth();
    }
}