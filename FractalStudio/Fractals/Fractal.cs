using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace FractalStudio.Fractals
{
    public abstract class Fractal
    {
        private protected readonly List<Color> Gradient;
        private protected readonly int MaxRecursion;

        protected Fractal(Color startColor, Color endColor, int maxRecursion, int gradientLength)
        {
            Gradient = GetColorGradient(startColor, endColor, gradientLength).ToList();
            MaxRecursion = maxRecursion;
        }

        public abstract void Draw(Canvas canvas, int x, int y, double length, double angle, double lengthRatio, 
            double angleRatioLeft, double angleRatioRight, int step = 0);
        
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
                throw new ArgumentException("Gradient cannot have less than two colors.", nameof(totalNumberOfColors));
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
                    (byte)c(from.A, stepA),
                    (byte)c(from.R, stepR),
                    (byte)c(from.G, stepG),
                    (byte)c(from.B, stepB));

                int c(int fromC, double stepC)
                {
                    return (int)Math.Round(fromC + stepC * i);
                }
            }

            yield return to;
        }
    }
}