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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LiveChart
{
    /// <summary>
    /// Logique d'interaction pour Synthese.xaml
    /// </summary>
    public partial class Synthese : Page
    {
        public Synthese()
        {
            InitializeComponent();
        }

        private void Graphe_Click(object sender, RoutedEventArgs e)
        {
            Graphe.Foreground = Brushes.Aqua;
            Climat.Foreground = Tableau.Foreground = Comparaison.Foreground = Brushes.White;
            contenu.Navigate(new Uri("Graphe.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Climat_Click(object sender, RoutedEventArgs e)
        {
            Climat.Foreground = Brushes.Aqua;
            Graphe.Foreground = Tableau.Foreground = Comparaison.Foreground = Brushes.White;
            contenu.Navigate(new Uri("Climat.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Tableau_Click(object sender, RoutedEventArgs e)
        {
            Tableau.Foreground = Brushes.Aqua;
            Graphe.Foreground = Climat.Foreground = Comparaison.Foreground = Brushes.White;
            contenu.Navigate(new Uri("Tableau.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Comparaison_Click(object sender, RoutedEventArgs e)
        {
            Comparaison.Foreground = Brushes.Aqua;
            Graphe.Foreground = Climat.Foreground = Tableau.Foreground = Brushes.White;
            contenu.Navigate(new Uri("Comparaison.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}

