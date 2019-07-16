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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeatherLab
{
    /// <summary>
    /// Logique d'interaction pour MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        private bool correct;

        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        public MessageBoxWindow(string message , bool correct)
        {
            InitializeComponent();
            Owner = App.Current.MainWindow;
            ShowInTaskbar = false;

            this.correct = correct;
            this.WindowMessage.Text= correct ? message : "Erreur: " + message;

            this.image.Source = new BitmapImage(new Uri(correct ? @"pack://application:,,,/assets/imgs/correcte.png" : @"pack://application:,,,/assets/imgs/incorrecte.png"));
        }


        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void PackIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void CasSpecial_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("OK"))
            {
                this.Close();
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (casSpecial.Content.Equals("OK") && e.Key ==Key.Return)
            {
                this.Close();
            }
        }
    }
}
