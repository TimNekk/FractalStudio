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
            Fractal fractal = new KochСurve(Colors.Fuchsia, Colors.MediumSpringGreen, 3);
            fractal.Draw(FractalCanvas, (int)Width / 2 - 250, (int)Height / 2, 500, 1.57, 0.8, 0.1, 0.1);
        }
    }
}