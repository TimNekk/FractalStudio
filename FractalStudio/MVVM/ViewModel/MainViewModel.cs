using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using FractalStudio.Core;
using FractalStudio.Fractals;

namespace FractalStudio.MVVM.ViewModel
{
    public class RectItem
    {
        public double X { get; set; } = 100;
        public double Y { get; set; } = 100;
        public double Width { get; set; } = 100;
        public double Height { get; set; } = 100;
    }
    
    public class MainViewModel : ObservableObject
    {
        public RelayCommand RecursionAddCommand { get; set; }
        public RelayCommand RecursionReduceCommand { get; set; }
        public RelayCommand PythagorasTreeViewCommand { get; set; }
        public RelayCommand KochСurveViewCommand { get; set; }
        
        private PythagorasTreeViewModel PythagorasTreeVM { get; set; }
        private KochСurveViewModel KochСurveVM{ get; set; }
        
        public ObservableCollection<RectItem> RectItems{ get; set; }
        
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        
        private int _recursion;

        public int Recursion
        {
            get => _recursion;
            set
            {
                _recursion = value;
                OnPropertyChanged();
            }
        }
        
        private int _maxRecursion;

        public int MaxRecursion
        {
            get => _maxRecursion;
            set
            {
                _maxRecursion = value;
                if (Recursion > MaxRecursion) Recursion = MaxRecursion;
                OnPropertyChanged();
            }
        }
        
        private Canvas _canvas;
        public Canvas FractalCanvas
        {
            get => _canvas;
            set
            {
                _canvas = value;
                OnPropertyChanged();
            }
        }
        
        private Fractal _fractal;

        public Fractal CurrentFractal
        {
            get => _fractal;
            set
            {
                _fractal = value;
                OnPropertyChanged();
            }
        }
        
        public MainViewModel()
        {
            var canvas = new Canvas();
            var line = new Line
            {
                X1 = 100, Y1 = 100, X2 = 200, Y2 = 200, Stroke = new SolidColorBrush(Colors.Aqua)
            };
            canvas.Children.Add(line);
            FractalCanvas = canvas;
            
            Recursion = 4;
            
            PythagorasTreeVM = new PythagorasTreeViewModel();
            KochСurveVM = new KochСurveViewModel();
            
            CurrentView = PythagorasTreeVM;

            PythagorasTreeViewCommand = new RelayCommand(o =>
            {
                MaxRecursion = 8;
                if (Recursion > MaxRecursion) Recursion = MaxRecursion;
                
                // CurrentFractal = new SierpinskiCarpet(Colors.Fuchsia, Colors.MediumSpringGreen, Recursion);
                // CurrentFractal.Draw(FractalCanvas, 100, 500, 1000, 1.57, 0.8, 0.1, 0.1);
            });
            
            KochСurveViewCommand = new RelayCommand(o =>
            {
                MaxRecursion = 6;
            });

            RecursionAddCommand = new RelayCommand(o =>
            {
                if (Recursion < MaxRecursion) Recursion++;
            });
            
            RecursionReduceCommand = new RelayCommand(o =>
            {
                if (Recursion > 1) Recursion--;
            });

            var r = new RectItem();
            r.X = 300;
            RectItems = new ObservableCollection<RectItem>
            {
                r, new RectItem()
            };
        }
    }
}