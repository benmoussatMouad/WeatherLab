using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WeatherLab.Data;
using WeatherLab.ConfigUtils;

namespace FenetreDataSet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClickAjouter(object sender, RoutedEventArgs e)
        {
            ConfigParser cp = new ConfigParser();

            cp.import("config.json");

            Wilaya w = cp.getWilaya((string)ComboWilaya.SelectedItem);
            w.modifierAttr("Température", (string)combo1.SelectedItem);
            w.modifierAttr("Pression", (string)combo2.SelectedItem);
            w.modifierAttr("Humidité", (string)combo3.SelectedItem);
            w.modifierAttr("Direction du vent", (string)combo4.SelectedItem);
            w.modifierAttr("Vitesse du vent", (string)combo5.SelectedItem);
            w.modifierAttr("Nuage %", (string)combo6.SelectedItem);
            w.modifierAttr("Distance de visibilité", (string)combo7.SelectedItem);
            w.modifierAttr("Etat du sol", (string)combo8.SelectedItem);
            w.modifierAttr("Hauteur de neige", (string)combo9.SelectedItem);

            cp.modifierWilaya((string)ComboWilaya.SelectedItem, w);
            cp.export("config.json");
            wnd.Close();
        }

        private void btnClickParcourir(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            file.DefaultExt = ".csv";
            file.Filter = "Comma Separated Values|*.csv";

            Nullable<bool> result = file.ShowDialog();
            if(result == true)
            {
                loadPath(file.FileName);
            }
        }

        public void setWilayas()
        {
            ConfigParser cp = new ConfigParser();
            cp.import("config.json");
            foreach (string item in cp.getNomWilayas())
            {
                ComboWilaya.Items.Add(item);
            }
            ComboWilaya.SelectedIndex = 0;
        }

        public void loadPath(string path)
        {
            txtPath.Text = path;

            CsvParser csv = new CsvParser(path);
            ConfigParser cp = new ConfigParser();

            cp.import("config.json");

            if (9 != csv.getAttributs().Length)
                throw new ArgumentException("le format du fichier est incorrecte.\nla longueur des attributs n'est pas " + csv.getAttributs().Length);

            setCombo(csv.getAttributs());

        }

        public void setCombo(string[] attrs)
        {
            foreach(string attr in attrs)
            {
                combo1.Items.Add(attr);
            }
            combo1.SelectedIndex = 0;

            foreach (string attr in attrs)
            {
                combo2.Items.Add(attr);
            }
            combo2.SelectedIndex = 1;

            foreach (string attr in attrs)
            {
                combo3.Items.Add(attr);
            }
            combo3.SelectedIndex = 2;

            foreach (string attr in attrs)
            {
                combo4.Items.Add(attr);
            }
            combo4.SelectedIndex = 3;

            foreach (string attr in attrs)
            {
                combo5.Items.Add(attr);
            }
            combo5.SelectedIndex = 4;

            foreach (string attr in attrs)
            {
                combo6.Items.Add(attr);
            }
            combo6.SelectedIndex = 5;

            foreach (string attr in attrs)
            {
                combo7.Items.Add(attr);
            }
            combo7.SelectedIndex = 6;

            foreach (string attr in attrs)
            {
                combo8.Items.Add(attr);
            }
            combo8.SelectedIndex = 7;

            foreach (string attr in attrs)
            {
                combo9.Items.Add(attr);
            }
            combo9.SelectedIndex = 8;
        }

        private void Wnd_Loaded(object sender, RoutedEventArgs e)
        {
            setWilayas();
        }
    }
}
