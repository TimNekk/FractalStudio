using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dsafa.WpfColorPicker;
using FractalStudio.Fractals;
using Microsoft.Win32;

namespace FractalStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private int _maxRecursion;
        private Point _previousMousePosition;
        
        public int MaxRecursion
        {
            get => _maxRecursion;
            set
            {
                _maxRecursion = value;
                RecursionSlider.Maximum = value;
                if (Recursion > _maxRecursion) Recursion = value;
            }
        }

        private int _recursion;
        
        public int Recursion
        {
            get => _recursion;
            set
            {
                if (value <= 0 || value > MaxRecursion) return;
                RecursionSlider.Value = value;
                _recursion = value;
                if (Fractal != null) Fractal.Recursion = value;
            }
        }
        
        private double _lengthRatio;
        
        public double LengthRatio
        {
            get => _lengthRatio;
            set
            {
                _lengthRatio = value;
                LengthRatioSlider.Value = value;
                if (Fractal != null) Fractal.LengthRatio = value;
            }
        }
        
        private double _angleRatioLeft;
        
        public double AngleRatioLeft
        {
            get => _angleRatioLeft;
            set
            {
                _angleRatioLeft = value;
                AngleRatioLeftSlider.Value = value;
                if (Fractal != null) Fractal.AngleRatioLeft = value;
            }
        }
        
        private double _angleRatioRight;
        
        public double AngleRatioRight
        {
            get => _angleRatioRight;
            set
            {
                _angleRatioRight = value;
                AngleRatioRightSlider.Value = value;
                if (Fractal != null) Fractal.AngleRatioRight = value;
            }
        }
        
        private double _spacing;
        
        public double Spacing
        {
            get => _spacing;
            set
            {
                _spacing = value;
                SpacingSlider.Value = value;
                if (Fractal != null) Fractal.Spacing = value;
            }
        }
        
        private double _scale;
        
        public double Scale
        {
            get => _scale;
            set
            {
                if (value < 0) return;
                _scale = value;
                if (Fractal != null) Fractal.Scale = value;
            }
        }
        
        private int _x;
        
        public int X
        {
            get => _x;
            set
            {
                if (value < 0) return;
                _x = value;
                if (Fractal != null) Fractal.X = value;
            }
        }
        
        private int _y;
        
        public int Y
        {
            get => _y;
            set
            {
                if (value < 0) return;
                _y = value;
                if (Fractal != null) Fractal.Y = value;
            }
        }

        private Fractal _fractal;

        public Fractal Fractal
        {
            get => _fractal;
            set
            {
                _fractal = value;
            }
        }
        
        private Gradient _gradient;

        public Gradient Gradient
        {
            get => _gradient;
            set
            {
                _gradient = value;
                SetFrameGradient();
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();

            MaxRecursion = 10;
            LengthRatio = 0.8;
            Recursion = 2;
            AngleRatioRight = 0.1;
            AngleRatioLeft = 0.1;
            Spacing = 35;
            Gradient = new Gradient(Colors.Fuchsia, Colors.Cyan, 2);
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

        private void PythagorasTreeButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 10;
            Fractal = new PythagorasTree(FractalCanvas, Recursion, 500, 500, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        private void KochСurveButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 6;
            Fractal = new KochСurve(FractalCanvas, Recursion, 100, 500, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        private void SierpinskiCarpetButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 5;
            Fractal = new SierpinskiCarpet(FractalCanvas, Recursion, 100, 100, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        private void SierpinskiTriangleButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 7;
            Fractal = new SierpinskiTriangle(FractalCanvas, Recursion, 400, 400, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        private void CantorSetButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 7;
            Fractal = new CantorSet(FractalCanvas, Recursion, 100, 100, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }

        private void RecursionSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Recursion = (int) RecursionSlider.Value;
        }

        private void StartGradientButtonClick(object sender, RoutedEventArgs e)
        {
            Color newColor = ShowColorPicker(Gradient.StartColor);
            if (newColor != Gradient.StartColor)
            {
                Gradient.StartColor = newColor;
                if (Fractal != null) Fractal.Gradient = Gradient;
                SetFrameGradient();
            }
        }

        private void EndGradientButtonClick(object sender, RoutedEventArgs e)
        {
            Color newColor = ShowColorPicker(Gradient.EndColor);
            if (newColor != Gradient.EndColor)
            {
                Gradient.EndColor = newColor;
                if (Fractal != null) Fractal.Gradient = Gradient;
                SetFrameGradient();
            }
        }

        private Color ShowColorPicker(Color startColor)
        {
            var dialog = new ColorPickerDialog(startColor);
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value) return dialog.Color;
            return startColor;
        }

        private void SetFrameGradient()
        {
            GradientFrame.Background = new LinearGradientBrush(Gradient.StartColor, Gradient.EndColor, new Point(0, 0.5), new Point(1, 0.5));
        }

        private void LengthRatioSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LengthRatio = LengthRatioSlider.Value;
        }

        private void AngleRatioLeftSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AngleRatioLeft = AngleRatioLeftSlider.Value;
        }

        private void AngleRatioRightSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AngleRatioRight = AngleRatioRightSlider.Value;
        }
        
        private void SpacingSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Spacing = SpacingSlider.Value;
        }

        private void Zoom(object sender, MouseWheelEventArgs e)
        {
            double zoomFactor = e.Delta > 0 ? 0.2 : -0.2;
            Scale += zoomFactor;
        }

        private void PanMouseDown(object sender, MouseButtonEventArgs e)
        {
            CanvasBorder.CaptureMouse();
            _previousMousePosition = e.GetPosition(CanvasBorder);
        }
        
        private void PanMouseUp(object sender, MouseButtonEventArgs e)
        {
            CanvasBorder.ReleaseMouseCapture();
        }

        private void PanMouseMove(object sender, MouseEventArgs e)
        {
            if (CanvasBorder.IsMouseCaptured)
            {
                Point mousePosition = e.GetPosition(CanvasBorder);
                X += (int) (mousePosition.X - _previousMousePosition.X);
                Y += (int) (mousePosition.Y - _previousMousePosition.Y);
                _previousMousePosition = mousePosition;
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var rtb = new RenderTargetBitmap(
                (int)FractalCanvas.RenderSize.Width,
                (int)FractalCanvas.RenderSize.Height, 96d, 96d, PixelFormats.Default);
            rtb.Render(FractalCanvas);
            
            var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, (int) FractalCanvas.RenderSize.Width, (int) FractalCanvas.RenderSize.Height));

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));

            var dialog = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png",
                InitialDirectory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName
            };
            

            if (dialog.ShowDialog() == false)
                return;

            using var fs = File.OpenWrite(dialog.FileName);
            pngEncoder.Save(fs);
        }
    }
}