using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FractalStudio.Fractals;

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
            // Fractal fractal = new KochСurve(Colors.Fuchsia, Colors.MediumSpringGreen, 4, (int)Math.Pow(2, (4 - 1) * 2));
            // fractal.Draw(FractalCanvas, 100, 500, 1000, 1.57, 0.8, 0.1, 0.1);

            // int depth = 1;
            // Fractal fractal = new SierpinskiCarpet(Colors.Fuchsia, Colors.MediumSpringGreen, depth);
            // fractal.Draw(
            //     canvas:FractalCanvas, 
            //     x:100, 
            //     y:100, 
            //     length:500, 
            //     1.57, 0.8, 0.1, 0.1);
            
            // int depth = 4;
            // Fractal fractal = new SierpinskiTriangle(Colors.Fuchsia, Colors.MediumSpringGreen, depth, depth + 1);
            // fractal.Draw(
            //     canvas:FractalCanvas, 
            //     x:400, 
            //     y:400, 
            //     length:300, 
            //     1.57, 0.8, 0.1, 0.1);
            
            // int depth = 7;
            // Fractal fractal = new CantorSet(Colors.Fuchsia, Colors.MediumSpringGreen, depth, depth);
            // fractal.Draw(
            //     canvas:FractalCanvas, 
            //     x:100, 
            //     y:100, 
            //     length:1000, 
            //     1.57, 0.8, 0.1, 0.1);
        }

        private void ControlPanelOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void ControlPanelClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) FullscreenButtonClick(sender, e);
        }
        
        private void FullscreenButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
        }
        
        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}