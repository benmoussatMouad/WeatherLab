using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherLab.Synthése;

namespace WeatherLab
{
    /// <summary>
    /// Logique d'interaction pour MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        bool window1Active = false;
        public BtnOfMap Prediction_Synthese = new BtnOfMap(0,0);
        MainWindow w;
        ScaleTransform scaleTransformActual;
        public Path actualPath;
        public bool isDragged;
        private Point _last;

        public MapPage()
        {
            InitializeComponent();
            w = App.Current.MainWindow as MainWindow;
            w.SizeChanged += new SizeChangedEventHandler(Window_SizeChanged);
            w.StateChanged += new EventHandler(Window_StateChanged);
            w.LocationChanged += new EventHandler(Window_LocationChanged);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            MyWindow.Cursor = Cursors.Hand;
            CaptureMouse();
            _last = e.GetPosition(this);
            isDragged = true;

        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            ReleaseMouseCapture();
            isDragged = false;
            MyWindow.Cursor = Cursors.Arrow;

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isDragged == false)
                return;
            base.OnMouseMove(e);

            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
                Prediction_Synthese.Close();
                var pos = e.GetPosition(this);
                var matrix = mt.Matrix;
                matrix.Translate(pos.X - _last.X, pos.Y - _last.Y);
                mt.Matrix = matrix;
                _last = pos;
            }

        }

        private void Path_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!((Path)sender).Equals(actualPath))
            {
                ((Path)sender).Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7B9EAE"));
                //(sender as Path).StrokeThickness = 3;
                DropShadowBitmapEffect dropShadow = new DropShadowBitmapEffect();
                dropShadow.Direction = 270;
                dropShadow.Color = Colors.White;
                dropShadow.Opacity = 0.4;
                dropShadow.ShadowDepth = 2;
                dropShadow.Softness = 0.02;
                ((Path)sender).BitmapEffect = dropShadow;

                (sender as Path).Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF353E29"));
            }
        }



        private void Path_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!((Path)sender).Equals(actualPath))
            {
                Brush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCCCCCC"));
                ((Path)sender).Fill = Application.Current.Resources["DarkBlue"] as Brush; 
                (sender as Path).BitmapEffect = null;
                (sender as Path).StrokeThickness = .5;
                (sender as Path).Stroke = Brushes.White;


            }
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (window1Active)
            {
                Prediction_Synthese.Close();
                window1Active = false;
                actualPath.Fill = App.Current.Resources["DarkBlue"] as Brush;
                actualPath.StrokeThickness = 0.5;
                actualPath.Stroke = Brushes.White;
                actualPath.BitmapEffect = null;
                actualPath = null;
            }
            actualPath = (Path)sender;
            window1Active = true;
            actualPath.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#75BAC8"));
            DropShadowBitmapEffect dropShadow = new DropShadowBitmapEffect();
            dropShadow.Direction = 270;
            dropShadow.Color = Colors.White;
            dropShadow.Opacity = 0.8;
            dropShadow.ShadowDepth = 3;
            dropShadow.Softness = 0;
            actualPath.BitmapEffect = dropShadow;
            Prediction_Synthese.Close();
            Point p = Mouse.GetPosition(App.Current.MainWindow);
            Prediction_Synthese = new BtnOfMap(p.X,p.Y);
            if (((MainWindow)App.Current.MainWindow).WindowState.Equals(WindowState.Maximized))
            {
                Prediction_Synthese.Left = p.X- 50;
                Prediction_Synthese.Top = p.Y;
            }
            else
            {
                Prediction_Synthese.Left =((MainWindow)App.Current.MainWindow).Left + p.X- 50;
                Prediction_Synthese.Top = ((MainWindow)App.Current.MainWindow).Top + p.Y;
            }

            Prediction_Synthese.Show();
        }

        private void Svg2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Svg2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Svg2_MouseMove(object sender, MouseEventArgs e)
        {

        }



        private void Svg2_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SldZoom_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Prediction_Synthese.Close();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (!(w.main.Content is null))
            {
                if (w.main.Content.GetType().Equals(typeof(MapPage)))
                {
                    ((MapPage)w.main.Content).Prediction_Synthese.Close();
                }
            }


        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

            if (!(w.main.Content is null))
            {
                if (w.main.Content.GetType().Equals(typeof(MapPage)))
                {
                    ((MapPage)w.main.Content).Prediction_Synthese.Close();
                }
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (!(w.main.Content is null))
            {
                if (w.main.Content.GetType().Equals(typeof(MapPage)))
                {
                    BtnOfMap.Relocate(((MapPage)w.main.Content).Prediction_Synthese);
                }
            }

        }

        private void Path_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            
        }
    }
}
