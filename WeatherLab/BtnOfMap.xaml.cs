using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WeatherLab.Synthése;

namespace WeatherLab
{
    /// <summary>
    /// Logique d'interaction pour BtnOfMap.xaml
    /// </summary>
    public partial class BtnOfMap : Window
    {   
        double initLeft;
        double initTop;
        public BtnOfMap(double x, double y)
        {
            InitializeComponent();
            Owner = App.Current.MainWindow;
            ShowInTaskbar = false;
            InitTop = -y;
            InitLeft = x;
            prediction.Click += (App.Current.MainWindow as MainWindow).Btn1_Click;
            prediction.Click += (object sender, RoutedEventArgs e) =>
            {
                MainWindow mw = App.Current.MainWindow as MainWindow;
                MapPage map = mw.main.Content as MapPage;
                foreach (ComboBoxItem i in mw.comboWilaya.Items)
                {
                    if (i.Content.Equals(map.actualPath.Name)){
                        mw.comboWilaya.SelectedIndex = mw.comboWilaya.Items.IndexOf(i);
                        return;
                    }
                }
                mw.comboWilaya.SelectedValue = (mw.main.Content as MapPage).actualPath.Name;
            };
            //synthese.Click += (App.Current.MainWindow as MainWindow).Btn2_Click;
            synthese.Click += (object sender, RoutedEventArgs e) =>{
                MainWindow mw = (App.Current.MainWindow as MainWindow);
                mw.fermerFlyout();
                mw.btn2.Background = (Brush)App.Current.Resources["DarkBlue"];
                mw.btn2.Content = App.Current.Resources["WhiteBarChart"];
                mw.Deselect_Btn(mw.btnSelec);
                mw.btnSelec = 2;
                MapPage map = mw.main.Content as MapPage;
                string wilaya = map.actualPath.Name;
                mw.main.Content = new Synthese(wilaya,0);
               
                this.Close();
            };




        }

       

        public double InitTop { get => initTop; set => initTop = value; }
        public double InitLeft { get => initLeft; set => initLeft = value; }

        public static void Relocate(BtnOfMap btn)
        {
           
            btn.Left =btn.InitLeft+((MainWindow)App.Current.MainWindow).Left - 50;
            btn.Top = btn.InitTop + ((MainWindow)App.Current.MainWindow).Top;
        }


        private void Synthese_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Prediction_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = App.Current.MainWindow as MainWindow;
            w.Deselect_Btn(w.btnSelec);
            w.btnSelec = 1;
            w.btn1.Background = (Brush)App.Current.Resources["DarkBlue"];
            w.btn1.Content = App.Current.Resources["WhiteCloud"];
            w.ouvrirFlyout();
        }
    }
}
