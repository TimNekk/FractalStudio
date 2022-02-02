using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace FractalStudio
{
    public class Gradient
    {
        private List<Color> Colors { set; get; }

        private int _length;

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

        public Color EndColor
        {
            get => _endColor;
            set
            {
                _endColor = value;
                UpdateColors();
            }
        }
        
        public Gradient(Color startColor, Color endColor, int length)
        {
            _startColor = startColor;
            _endColor = endColor;
            _length = length;
            Colors = GetColorGradient(startColor, endColor, length).ToList();
        }

        private void UpdateColors()
        {
            Colors = GetColorGradient(StartColor, EndColor, Length).ToList();
        }
        
        public Color this[int index] => Colors[index];

        /// <summary>
        /// Source: https://stackoverflow.com/questions/2011832/generate-color-gradient-in-c-sharp
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="totalNumberOfColors"></param>
        /// <returns></returns>
        private static IEnumerable<Color> GetColorGradient(Color from, Color to, int totalNumberOfColors)
        {
            if (totalNumberOfColors < 2)
            {
                yield return from;
            }

            double diffA = to.A - from.A;
            double diffR = to.R - from.R;
            double diffG = to.G - from.G;
            double diffB = to.B - from.B;

            var steps = totalNumberOfColors - 1;

            var stepA = diffA / steps;
            var stepR = diffR / steps;
            var stepG = diffG / steps;
            var stepB = diffB / steps;

            yield return from;

            for (var i = 1; i < steps; ++i)
            {
                yield return Color.FromArgb(
                    (byte)C(from.A, stepA),
                    (byte)C(from.R, stepR),
                    (byte)C(from.G, stepG),
                    (byte)C(from.B, stepB));

                int C(int fromC, double stepC)
                {
                    return (int)Math.Round(fromC + stepC * i);
                }
            }

            yield return to;
        }
    }
}