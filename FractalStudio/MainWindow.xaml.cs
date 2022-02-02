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

        private int MaxRecursion
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

        private int Recursion
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

        private double LengthRatio
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

        private double AngleRatioLeft
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

        private double AngleRatioRight
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

        private double Spacing
        {
            get => _spacing;
            set
            {
                _spacing = value;
                SpacingSlider.Value = value;
                FunctionComboBox.SelectedIndex = (int) value;
                if (Fractal != null) Fractal.Spacing = value;
            }
        }
        
        private double _scale;

        private double Scale
        {
            get => _scale;
            set
            {
                if (value < 0) return;
                ScaleSlider.Value = value;
                _scale = value;
                if (Fractal != null) Fractal.Scale = value;
            }
        }
        
        private int _resolution;

        private int Resolution
        {
            get => _resolution;
            set
            {
                if (value < 0) return;
                ResolutionSlider.Value = value;
                _resolution = value;
                if (Fractal != null) Fractal.LengthRatio = value;
            }
        }
        
        private double _reRatio;

        private double ReRatio
        {
            get => _reRatio;
            set
            {
                ReRatioSlider.Value = value;
                _reRatio = value;
                if (Fractal != null) Fractal.AngleRatioLeft = value;
            }
        }
        
        private double _imRatio;

        private double ImRatio
        {
            get => _imRatio;
            set
            {
                ImRatioSlider.Value = value;
                _imRatio = value;
                if (Fractal != null) Fractal.AngleRatioRight = value;
            }
        }
        
        private int _x;

        private int X
        {
            get => _x;
            set
            {
                _x = value;
                if (Fractal != null) Fractal.X = value;
            }
        }
        
        private int _y;

        private int Y
        {
            get => _y;
            set
            {
                _y = value;
                if (Fractal != null) Fractal.Y = value;
            }
        }

        private Fractal _fractal;

        private Fractal Fractal
        {
            get => _fractal;
            set
            {
                _fractal = value;
            }
        }
        
        private Gradient _gradient;

        private Gradient Gradient
        {
            get => _gradient;
            set
            {
                _gradient = value;
                SetFrameGradient();
            }
        }

        /// <summary>
        /// MainWindow constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            MaxRecursion = 10;
            LengthRatio = 0.8;
            Recursion = 5;
            Scale = 1;
            AngleRatioRight = 0.3;
            AngleRatioLeft = 0.3;
            Spacing = 35;
            Gradient = new Gradient(Colors.Fuchsia, Colors.Cyan, 2);
            Resolution = 100;
            ReRatio = 0.1;
        }

        /// <summary>
        /// Moves window around
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ControlPanelOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Closes application
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        /// <summary>
        /// Turns fullscreen on double click
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ControlPanelClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) FullscreenButtonClick(sender, e);
        }
        
        /// <summary>
        /// Turns fullscreen on and off
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void FullscreenButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
        }
        
        /// <summary>
        /// Minimizes window
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Draws Pythagoras Tree Fractal
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void PythagorasTreeButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 12;
            X = 500;
            Y = 500;
            Fractal = new PythagorasTree(FractalCanvas, Recursion, 500, 500, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }

        /// <summary>
        /// Draws Koch Сurve Fractal
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void KochСurveButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 6;
            X = 100;
            Y = 500;
            Fractal = new KochСurve(FractalCanvas, Recursion, 100, 500, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        /// <summary>
        /// Draws Sierpinski Carpet Fractal
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void SierpinskiCarpetButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 5;
            X = 100;
            Y = 100;
            Fractal = new SierpinskiCarpet(FractalCanvas, Recursion, 100, 100, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        /// <summary>
        /// Draws Sierpinski Triangle Fractal
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void SierpinskiTriangleButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 7;
            X = 400;
            Y = 400;
            Fractal = new SierpinskiTriangle(FractalCanvas, Recursion, 400, 400, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        /// <summary>
        /// Draws Cantor Set Fractal
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void CantorSetButtonClick(object sender, RoutedEventArgs e)
        {
            Spacing = 35;
            MaxRecursion = 7;
            X = 100;
            Y = 100;
            Fractal = new CantorSet(FractalCanvas, Recursion, 100, 100, 1, LengthRatio, AngleRatioLeft, AngleRatioRight, Spacing, Gradient);
            Fractal.Draw();
        }
        
        /// <summary>
        /// Draws Julia Set Fractal
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void JuliaSetButtonClick(object sender, RoutedEventArgs e)
        {
            MaxRecursion = 50;
            X = 0;
            Y = 0;
            Scale = 1;
            Spacing = 0;
            Fractal = new JuliaSet(FractalCanvas, Recursion, 0, 0, 1, Resolution, AngleRatioLeft, AngleRatioRight, 0, Gradient, ImageControl);
            Fractal.Draw();
        }

        /// <summary>
        /// Sets Recursion
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void RecursionSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Recursion = (int) RecursionSlider.Value;
        }

        /// <summary>
        /// Sets gradient start color
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
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

        /// <summary>
        /// Sets gradient end color
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
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

        /// <summary>
        /// Opens color picker
        /// </summary>
        /// <param name="startColor"></param>
        /// <returns>Selected color</returns>
        private Color ShowColorPicker(Color startColor)
        {
            var dialog = new ColorPickerDialog(startColor);
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value) return dialog.Color;
            return startColor;
        }

        /// <summary>
        /// Sets gradient on gradient frame
        /// </summary>
        private void SetFrameGradient()
        {
            GradientFrame.Background = new LinearGradientBrush(Gradient.StartColor, Gradient.EndColor, new Point(0, 0.5), new Point(1, 0.5));
        }

        /// <summary>
        /// Sets length ratio
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void LengthRatioSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LengthRatio = LengthRatioSlider.Value;
        }

        /// <summary>
        /// Sets left angle ratio
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void AngleRatioLeftSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AngleRatioLeft = AngleRatioLeftSlider.Value;
        }

        /// <summary>
        /// Sets right angle ratio
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void AngleRatioRightSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AngleRatioRight = AngleRatioRightSlider.Value;
        }
        
        /// <summary>
        /// Sets spacing
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void SpacingSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Spacing = SpacingSlider.Value;
        }
        
        /// <summary>
        /// Sets scale
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ScaleSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Scale = ScaleSlider.Value;
        }        
        
        /// <summary>
        /// Sets resolution
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ResolutionSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Resolution = (int)ResolutionSlider.Value;
        }       
        
        /// <summary>
        /// Sets Re ratio
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ReRatioSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ReRatio = ReRatioSlider.Value;
        }
        
        /// <summary>
        /// Sets Im ratio
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ImRatioSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ImRatio = ImRatioSlider.Value;
        }

        /// <summary>
        /// Zooms canvas
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void Zoom(object sender, MouseWheelEventArgs e)
        {
            double zoomFactor = e.Delta > 0 ? 0.2 : -0.2;
            Scale += zoomFactor;
        }

        /// <summary>
        /// Captures mouse position on press
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void PanMouseDown(object sender, MouseButtonEventArgs e)
        {
            _previousMousePosition = e.GetPosition(CanvasBorder);
            CanvasBorder.CaptureMouse();
        }
        
        /// <summary>
        /// Releases mouse capture
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void PanMouseUp(object sender, MouseButtonEventArgs e)
        {
            CanvasBorder.ReleaseMouseCapture();
        }
        
        /// <summary>
        /// Moves canvas relative to mouse movement
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
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

        /// <summary>
        /// Saves canvas as an image
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var rtb = new RenderTargetBitmap((int)FractalCanvas.RenderSize.Width, (int)FractalCanvas.RenderSize.Height, 96d, 96d, PixelFormats.Default);
            rtb.Render(JuliaSetButton.IsChecked is true ? ImageControl : FractalCanvas);
            
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            var dialog = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png",
                InitialDirectory = new DirectoryInfo(Environment.CurrentDirectory).Parent?.Parent?.FullName ?? string.Empty
            };
            
            if (dialog.ShowDialog() == false)
                return;

            using var fs = File.OpenWrite(dialog.FileName);
            pngEncoder.Save(fs);
        }

        /// <summary>
        /// Sets function
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void FunctionComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Spacing = FunctionComboBox.SelectedIndex;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}