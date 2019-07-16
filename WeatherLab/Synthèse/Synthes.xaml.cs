using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WeatherLab.Synthèse;

namespace WeatherLab.Synthése
{
    /// <summary>
    /// Logique d'interaction pour Synthese.xaml
    /// </summary>
    public partial class Synthese : Page
    {
        public string WilayaName="";
        public int pageouverte = 0;


        public Synthese()
        {
            InitializeComponent();
            ErreurAnnuler.Click += Climat_Click;
            ErreurAnnuler.Click += (object sencer, RoutedEventArgs e) => { contenuErreur.Visibility = Visibility.Collapsed; };
            ErreurReessayer.Click += (object sender, RoutedEventArgs e) =>
            {
                switch (pageouverte)
                {
                    case 2:
                        contenuErreur.Visibility = Visibility.Collapsed;
                        contenu.Content = new Graphe(WilayaName);
                        break;
                    case 3:
                        contenuErreur.Visibility = Visibility.Collapsed;
                        contenu.Content = new Tableau(WilayaName);
                        break;
                    case 4:
                        contenuErreur.Visibility = Visibility.Collapsed;
                        contenu.Content = new Comparaison(WilayaName);
                        break;
                    default:
                        break;
                }
            };
        }

        
            
        public Synthese(string WilayaName)
        {
            InitializeComponent();
            this.WilayaName = WilayaName;
            this.wilaya.Text = WilayaName;
        }
        public Synthese(string WilayaName,int i)
        {
            InitializeComponent();
            this.WilayaName = WilayaName;
            this.wilaya.Text = WilayaName;
            this.contenu.Content = new Climat(WilayaName);
            ErreurAnnuler.Click += Climat_Click;
            ErreurAnnuler.Click += (object sencer, RoutedEventArgs e) => { contenuErreur.Visibility = Visibility.Collapsed; };
            ErreurReessayer.Click += (object sender, RoutedEventArgs e) =>
            {
                switch (pageouverte)
                {
                    case 2:
                        contenuErreur.Visibility = Visibility.Collapsed;
                        contenu.Content = new Graphe(WilayaName);
                        break;
                    case 3:
                        contenuErreur.Visibility = Visibility.Collapsed;
                        contenu.Content = new Graphe(WilayaName);
                        break;
                    case 4:
                        contenuErreur.Visibility = Visibility.Collapsed;
                        contenu.Content = new Graphe(WilayaName);
                        break;
                    default:
                        break;
                }
            };

        }

        public int getPageOuverte()
        {
            return pageouverte;
        }

        private void Climat_Click(object sender, RoutedEventArgs e)
        {
            contenuErreur.Visibility = Visibility.Collapsed;
            try
            {
                if (WilayaName == "")
                    throw new NoWilayaNameException();
                pageouverte = 1;
                ShowClimate();
            }
            catch (NoWilayaNameException error)
            {
                (new MessageBoxWindow("Veuillez d'abord choisir une wilaya", false)).ShowDialog();
            }
        }

        private void Graphe_Click(object sender, RoutedEventArgs e)  
        {
            contenuErreur.Visibility = Visibility.Collapsed;
            try
            {
                if (WilayaName == "")
                    throw new NoWilayaNameException();
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#556060");
                var brush2 = (Brush)converter.ConvertFromString("#7B9EAE");
                Graphe.Foreground = brush2;
                Climat.Foreground = Tableau.Foreground = Comparaison.Foreground = brush;
                pageouverte = 2;
                contenu.Content = new Graphe(WilayaName);
            }
            catch(NoWilayaNameException error)
            {
                (new MessageBoxWindow("Veuillez d'abord choisir une wilaya", false)).ShowDialog();
                pageouverte = 0;
            }
           
        }

        

        public void ShowClimate ()
        {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#556060");
                var brush2 = (Brush)converter.ConvertFromString("#7B9EAE");
                Climat.Foreground = brush2;
                Graphe.Foreground = Tableau.Foreground = Comparaison.Foreground = brush;
                contenu.Content = new Climat(WilayaName);
        }

        private void Tableau_Click(object sender, RoutedEventArgs e)
        {
            contenuErreur.Visibility = Visibility.Collapsed;
            try
            {
                if (WilayaName == "")
                    throw new NoWilayaNameException();
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#556060");
                var brush2 = (Brush)converter.ConvertFromString("#7B9EAE");
                Tableau.Foreground = brush2;
                Graphe.Foreground = Climat.Foreground = Comparaison.Foreground = brush;
                pageouverte = 3;
                contenu.Content = new Tableau(WilayaName);
            }
            catch (NoWilayaNameException error)
            {
                (new MessageBoxWindow("Veuillez d'abord choisir une wilaya", false)).ShowDialog();
                pageouverte = 0;
            }
        }

        private void Comparaison_Click(object sender, RoutedEventArgs e)
        {
            contenuErreur.Visibility = Visibility.Collapsed;
            try
            {
                if (WilayaName == "")
                    throw new NoWilayaNameException();
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#556060");
                var brush2 = (Brush)converter.ConvertFromString("#7B9EAE");
                Comparaison.Foreground = brush2;
                Graphe.Foreground = Climat.Foreground = Tableau.Foreground = brush;
                pageouverte = 4 ;
                contenu.Content = new Comparaison(WilayaName);
            }
            catch (NoWilayaNameException error)
            {
                (new MessageBoxWindow("Veuillez d'abord choisir une wilaya", false)).ShowDialog();
                pageouverte = 0;
            }
        }

        public void ChangerWilaya_Click(object sender, RoutedEventArgs e)
        {
            ChangeWilaya fenetre = new ChangeWilaya(this);
            fenetre.Show();
        }
        
        private void Wilaya_TextChanged(object sender, TextChangedEventArgs e)
        {
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#556060");
            Climat.Foreground = Graphe.Foreground = Tableau.Foreground = Comparaison.Foreground = brush;
            contenu.Content = null;
        }

    }

    public class WpConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float val = float.Parse(value.ToString());
            return val / 4;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class FtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float val = float.Parse(value.ToString());

            return val / 70;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class ImgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float val = float.Parse(value.ToString());

            return val/3;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
