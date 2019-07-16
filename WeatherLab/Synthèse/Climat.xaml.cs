using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeatherLab.ConfigUtils;

namespace WeatherLab.Synthése
{
    /// <summary>
    /// Logique d'interaction pour Climat.xaml
    /// </summary>
    public partial class Climat : Page
    {
        private string Climatpath = @"assets\Climat\"; //Contient le chemin vers le dossier Climat
        private List<string> ClimatList = new List<string>();   //Contient touts les lignes de texte qu'il faut afficher
        string WilayaName;

        public Climat(string WilayaName)
        {
            ClimatList = File.ReadAllLines(Climatpath + "Climat.txt").ToList();
            InitializeComponent();
            this.WilayaName = WilayaName;
            Init();
        }

        private int WilayaToID()
        {
            ConfigParser cp = new ConfigParser();
            cp.import("config.json");
            List<string> ListeWilaya = cp.getNomWilayas().ToList();
            return cp.getWilaya(WilayaName).code - 1;
        }

        private void Init()
        {
            this.climat.Text = ClimatList.ElementAt(WilayaToID());
            string imagePath = Climatpath + (WilayaToID() + 1) + ".jpg";
            this.image.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(imagePath);

            //DoubleAnimation doubleAnimation = new DoubleAnimation() { From = 0.0, To = 1.0, Duration = new Duration(TimeSpan.FromSeconds(3)) };
            //Storyboard storyBoard = new Storyboard();
            //storyBoard.Children.Add(doubleAnimation);
            //Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(OpacityProperty));
            //storyBoard.Begin(image)
            }
    }

        
}
