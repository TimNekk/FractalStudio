using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace FractalStudio
{
    /// <summary>
    /// Gradient
    /// </summary>
    public class Gradient
    {
        private List<Color> Colors { set; get; }

        private int _length;

        /// <summary>
        /// Length of gradient
        /// </summary>
        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                UpdateColors();
            }
        }

        private Color _startColor;

        /// <summary>
        /// Start color
        /// </summary>
        public Color StartColor
        {
            get => _startColor;
            set
            {
                _startColor = value;
                UpdateColors();
            }
        }
        
        private Color _endColor;

        /// <summary>
        /// End color
        /// </summary>
        public Color EndColor
        {
            get => _endColor;
            set
            {
                _endColor = value;
                UpdateColors();
            }
        }
        
        /// <summary>
        /// Gradient constructor
        /// </summary>
        /// <param name="startColor">Start color</param>
        /// <param name="endColor">End color</param>
        /// <param name="length">Gradient length</param>
        public Gradient(Color startColor, Color endColor, int length)
        {
            _startColor = startColor;
            _endColor = endColor;
            _length = length;
            Colors = GetColorGradient(startColor, endColor, length).ToList();
        }

        /// <summary>
        /// Updates gradient colors
        /// </summary>
        private void UpdateColors()
        {
            Colors = GetColorGradient(StartColor, EndColor, Length).ToList();
        }
        
        /// <summary>
        /// Returns color based on index
        /// </summary>
        /// <param name="index"></param>
        public Color this[int index] => Colors[index];

        /// <summary>
        /// Generates color gradient
        /// Source: https://stackoverflow.com/questions/2011832/generate-color-gradient-in-c-sharp
        /// </summary>
        /// <param name="startColor">Start color</param>
        /// <param name="endColor">End color</param>
        /// <param name="lengt">Gradient length</param>
        /// <returns>Collection of colors</returns>
        private static IEnumerable<Color> GetColorGradient(Color startColor, Color endColor, int lengt)
        {
            if (lengt < 2)
            {
                yield return startColor;
            }

            double diffA = endColor.A - startColor.A;
            double diffR = endColor.R - startColor.R;
            double diffG = endColor.G - startColor.G;
            double diffB = endColor.B - startColor.B;

            var steps = lengt - 1;

            var stepA = diffA / steps;
            var stepR = diffR / steps;
            var stepG = diffG / steps;
            var stepB = diffB / steps;

            yield return startColor;

            for (var i = 1; i < steps; ++i)
            {
                yield return Color.FromArgb(
                    C(startColor.A, stepA),
                    C(startColor.R, stepR),
                    C(startColor.G, stepG),
                    C(startColor.B, stepB));

                byte C(int fromC, double stepC)
                {
                    return (byte)Math.Round(fromC + stepC * i);
                }
            }

            yield return endColor;
        }
    }
}