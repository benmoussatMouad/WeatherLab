using System.Windows;
using System.Windows.Input;

namespace WeatherLab
{
    /// <summary>
    /// Logique d'interaction pour AUthentification.xaml
    /// </summary>
    public partial class Authentification : Window
    {
        public string password;
        public bool isClosed;
        public Authentification()
        {
            InitializeComponent();
            isClosed = false;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isClosed = true;
            this.Close();
        }

        private void Connecter_Click(object sender, RoutedEventArgs e)
        {
            password = motDePasse.Password;
            Visibility = Visibility.Hidden;
        }

        private void MotDePasse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                password = motDePasse.Password;
                Visibility = Visibility.Hidden;
            }
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isClosed = true;
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
    }
}
