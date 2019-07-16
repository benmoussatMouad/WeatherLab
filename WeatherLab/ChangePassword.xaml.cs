using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WeatherLab
{
    /// <summary>
    /// Logique d'interaction pour AUthentification.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private string passwordString;
        public static readonly string password_path_file = "./hash.txt";
        public string hashedPassword;
        public bool isClosed;
        public ChangePassword()
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
            passwordString = motDePasse.Password;
            hashedPassword = hashPassword(passwordString,new SHA256CryptoServiceProvider());
            WriteToFile(password_path_file,hashedPassword);
            Visibility = Visibility.Hidden;
        }

        private void MotDePasse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                passwordString = motDePasse.Password;
                hashedPassword = hashPassword(passwordString, new SHA256CryptoServiceProvider());
                WriteToFile(password_path_file, hashedPassword);
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
        public static string hashPassword(String str , HashAlgorithm algorithm)
        {
            if(str!= null)
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
                return BitConverter.ToString(hashedBytes);
            }
            return "";
        }
        private void WriteToFile(string path ,string content) {
            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }
            File.WriteAllText(path, content);
            File.SetAttributes(path, FileAttributes.Hidden);

        }
    }
}
