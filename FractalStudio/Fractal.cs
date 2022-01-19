using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalStudio
{
    public abstract class Fractal
    {
        private protected readonly List<Color> Gradient;
        private protected readonly int MaxRecursion;

        protected Fractal(Color startColor, Color endColor, int maxRecursion)
        {
            Gradient = GetGradients(startColor, endColor, maxRecursion).ToList();
            MaxRecursion = maxRecursion;
        }

        public abstract void Draw(Canvas canvas, int x, int y, double length, double angle, double lengthRatio, double angleRatioLeft, double angleRatioRight, int n = 0);
        
        /// <summary>
        /// Source: https://stackoverflow.com/questions/2011832/generate-color-gradient-in-c-sharp
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        private static IEnumerable<Color> GetGradients(Color start, Color end, int steps)
        {
            int stepA = (end.A - start.A) / (steps - 1);
            int stepR = (end.R - start.R) / (steps - 1);
            int stepG = (end.G - start.G) / (steps - 1);
            int stepB = (end.B - start.B) / (steps - 1);

            for (int i = 0; i < steps; i++)
            {
                yield return Color.FromArgb((byte)(start.A + stepA * i),
                    (byte)(start.R + stepR * i),
                    (byte)(start.G + stepG * i),
                    (byte)(start.B + stepB * i));
            }
        }
    }
}