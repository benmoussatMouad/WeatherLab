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
using System.IO;

namespace LiveChart
{
    /// <summary>
    /// Logique d'interaction pour Climat.xaml
    /// </summary>
    public partial class Climat : Page
    {
        private string Climatpath = @"C:\Users\acer\Desktop\Climat\"; //Contient le chemin vers le dossier Climat
        private List<string> ClimatList = new List<string>();   //Contient touts les lignes de texte qu'il faut afficher
        public Climat()
        {
            ClimatList = File.ReadAllLines(Climatpath+ "Climat.txt").ToList();
            InitializeComponent();
        }
     /*   private void InitWilaya()
        {
            string path = @"C:\Users\acer\Desktop\wilaya.txt";
            string fichier = File.ReadAllText(path);
            List<ClasseWilaya> Wilayas = new List<ClasseWilaya>();
            string[] wilayas = fichier.Split(',');
            foreach (var wilaya in wilayas)
            {
                ClasseWilaya Wilaya = new ClasseWilaya();
                Wilaya.Wilaya = wilaya;
                Wilayas.Add(Wilaya);
            }
            wilaya.ItemsSource = Wilayas;
        }

        private class ClasseWilaya
        {
            public string Wilaya { get; set; }
        }*/

        private void Wilaya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.climat.Text = ClimatList.ElementAt(wilaya.SelectedIndex);
            string imagePath = Climatpath + (this.wilaya.SelectedIndex + 1) + ".jpg";
            Image image = new Image();
            ImageBrush brush = new ImageBrush();

            image.Source = new BitmapImage(new Uri(imagePath));
            brush.ImageSource = image.Source;
            this.grid.Background = brush;
        }
    }
}
    