using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using WeatherLab.ConfigUtils;
using WeatherLab.Synthése;

namespace WeatherLab.Synthèse
{
    /// <summary>
    /// Logique d'interaction pour ChangeWilaya.xaml
    /// </summary>
    public partial class ChangeWilaya : Window
    {
        //public string wilaya;
        Synthese synthese = new Synthese();

        public ChangeWilaya(Synthese s)
        {
            InitializeComponent();
            ShowInTaskbar = false;
            this.synthese = s;
            InitWilaya();
            this.Owner = App.Current.MainWindow;
        }

        public ChangeWilaya()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            InitWilaya();
            this.Owner = App.Current.MainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InitWilaya()
        {
            ConfigParser cp = new ConfigParser();
            cp.import("config.json");
            List<string> L = cp.getNomWilayas().ToList();
            List<Wilaya> Liste = new List<Wilaya>();
            foreach (string item in L)
            {
                Wilaya w = new Wilaya(item);
                Liste.Add(w);
            }
            this.ListDeWilaya.ItemsSource = Liste;
        }

       
        private class Wilaya
        {
            public string wilaya { get; set; }

            public Wilaya(string wilaya)
            {
                this.wilaya = wilaya;
            }

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            string  wilaya = ((Wilaya)ListDeWilaya.SelectedValue).wilaya;
            this.synthese = (App.Current.MainWindow as MainWindow).main.Content as Synthese;
            synthese.wilaya.Text = wilaya;
            synthese.WilayaName = wilaya;
            var converter = new BrushConverter();
            synthese.contenuErreur.Visibility = Visibility.Collapsed;
            synthese.contenu.Content = new Climat(wilaya);
            synthese.Climat.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //switch (synthese.getPageOuverte())
            //{
            //    case 1:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Climat(wilaya);
            //        synthese.Climat.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    case 2:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Graphe(wilaya);
            //        synthese.Graphe.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    case 3:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Tableau(wilaya);
            //        synthese.Tableau.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    case 4:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Comparaison(wilaya);
            //        synthese.Comparaison.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    default:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Climat(wilaya);
            //        synthese.Climat.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //}
            //switch (synthese.getPageOuverte())
            //{
            //    case 1:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Climat(wilaya);
            //        synthese.Climat.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    case 2:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Graphe(wilaya);
            //        synthese.Graphe.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    case 3:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Tableau(wilaya);
            //        synthese.Tableau.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    case 4:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Comparaison(wilaya);
            //        synthese.Comparaison.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //    default:
            //        synthese.contenuErreur.Visibility = Visibility.Collapsed;
            //        synthese.contenu.Content = new Climat(wilaya);
            //        synthese.Climat.Foreground = (Brush)converter.ConvertFromString("#7B9EAE");
            //        break;
            //}
            this.Close();
        }
    }
}
