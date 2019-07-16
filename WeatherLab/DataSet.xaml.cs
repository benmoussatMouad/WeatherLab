using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WeatherLab.ConfigUtils;
using WeatherLab.Data;

namespace WeatherLab
{
    /// <summary>
    /// Logique d'interaction pour DataSet.xaml
    /// </summary>
    public partial class DataSet : Window, System.ComponentModel.INotifyPropertyChanged
    {

        private ConfigParser cp;

        private string path;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                LoadPath(value);
                OnPropertyChanged("Path");
            }
        }

        public DataSet()
        {
            InitializeComponent();
            this.Owner = App.Current.MainWindow;
            cp = App.Config;

            MainWindow mw = (App.Current.MainWindow as MainWindow);
            this.Closing += (object sender, CancelEventArgs e) =>
            {

                mw.Deselect_Btn(mw.btnSelec);
                mw.btnSelec = 0;
            };

            cp.import("config.json");

            FichierGrid.DataContext = this;

            ComboWilaya.ItemsSource = cp.getNomWilayas();
            ComboWilaya.SelectedIndex = 0;
            

            try {
                Path = cp.getWilaya((string)ComboWilaya.SelectedValue).path;
            }
            catch (FileNotFoundException)
            {

            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnClickAjouter(object sender, RoutedEventArgs e)
        {
           
            if (!Validation.GetHasError(txtPath))
            {
                MainWindow mw = App.Current.MainWindow as MainWindow;
                mw.Deselect_Btn(mw.btnSelec);
                mw.btnSelec = 0;
                Wilaya w = cp.getWilaya((string)ComboWilaya.SelectedItem);
                w.path = Path;
                w.modifierAttr("Température", (string)combo1.SelectedItem);
                w.modifierAttr("Pression", (string)combo2.SelectedItem);
                w.modifierAttr("Humidité", (string)combo3.SelectedItem);
                w.modifierAttr("Direction du vent", (string)combo4.SelectedItem);
                w.modifierAttr("Vitesse du vent", (string)combo5.SelectedItem);
                w.modifierAttr("Nuage %", (string)combo6.SelectedItem);
                w.modifierAttr("Distance de visibilité", (string)combo7.SelectedItem);
                w.modifierAttr("Précipitation", (string)combo8.SelectedItem);
                w.modifierAttr("Etat du sol", (string)combo9.SelectedItem);
                w.modifierAttr("Hauteur de neige", (string)combo10.SelectedItem);

                cp.modifierWilaya((string)ComboWilaya.SelectedItem, w);
                cp.export("config.json");
                this.Close();
            }
        }

        private void btnClickParcourir(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog
            {
                DefaultExt = ".csv",
                Filter = "Comma Separated Values|*.csv"
            };

            Nullable<bool> result = file.ShowDialog();
            if (result == true)
            {
                txtPath.Text = file.FileName;
            }
        }

        public void LoadPath(string path)
        {
            CsvParser csv = new CsvParser(path);

            // on peut l'accepter
            if (10 != csv.getAttributs().Length)
                throw new ArgumentException("le format du fichier est incorrecte.");

            setCombo(csv.getAttributs());

        }

        public void setCombo(string[] attrs)
        {
            combo1.ItemsSource = attrs;
            combo1.SelectedIndex = 0;

            combo2.ItemsSource = attrs;
            combo2.SelectedIndex = 1;

            combo3.ItemsSource = attrs;
            combo3.SelectedIndex = 2;

            combo4.ItemsSource = attrs;
            combo4.SelectedIndex = 3;

            combo5.ItemsSource = attrs;
            combo5.SelectedIndex = 4;

            combo6.ItemsSource = attrs;
            combo6.SelectedIndex = 5;

            combo7.ItemsSource = attrs;
            combo7.SelectedIndex = 6;

            combo8.ItemsSource = attrs;
            combo8.SelectedIndex = 7;

            combo9.ItemsSource = attrs;
            combo9.SelectedIndex = 8;

            combo10.ItemsSource = attrs;
            combo10.SelectedIndex = 9;
        }

        private void ComboWilaya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtPath.Text = cp.getWilaya((string)ComboWilaya.SelectedValue).path;
            }catch (FileNotFoundException ea)
            {
                Console.WriteLine(ea.StackTrace);
            }
            
        }
    }
}
