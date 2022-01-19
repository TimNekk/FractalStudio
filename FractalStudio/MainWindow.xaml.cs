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
            Fractal fractal = new PythagorasTree(Colors.Fuchsia, Colors.MediumSpringGreen, 14);
            fractal.Draw(FractalCanvas, (int)Width / 2, (int)Height * 4 / 5, 100, 1.57, 0.8, 0.1, 0.1);
        }
    }
}