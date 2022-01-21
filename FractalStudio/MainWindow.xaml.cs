using System;
using System.Windows.Media;

namespace FractalStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Fractal fractal = new KochСurve(Colors.Fuchsia, Colors.MediumSpringGreen, 5, (int)Math.Pow(2, (5 - 1) * 2));
            fractal.Draw(FractalCanvas, 50, (int)Height - 100, 1000, 1.57, 0.8, 0.1, 0.1);
        }
    }
}