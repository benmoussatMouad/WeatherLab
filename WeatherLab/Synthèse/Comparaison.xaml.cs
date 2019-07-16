using LiveCharts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WeatherLab.ConfigUtils;
using WeatherLab.Data;

namespace WeatherLab.Synthése
{
    /// <summary>
    /// Logique d'interaction pour Comparaison.xaml
    /// </summary>
    public partial class Comparaison : Page
    {
        private Boolean done = false;
        private InputSynthesePrime inputSynthese;//= new InputSynthesePrime();
        string WilayaName;
        ConfigParser config;

        public Comparaison(string WilayaName)
        {
            try
            {
                config = App.Config;
                config.import("config.json");
                inputSynthese = new InputSynthesePrime(DateTime.Today,
                                                       DateTime.Today,
                                                       config.getWilaya(WilayaName).path,
                                                       config.getWilaya(WilayaName).path);
                InitializeComponent();
                date1.SelectedDate = date2.SelectedDate = DateTime.Today;
                this.WilayaName = WilayaName;
                InitWilaya();
                done = true;
            }
            catch (FileNotFoundException error)
            {
                
                MessageBoxWindow mb = new MessageBoxWindow("Le chemin de la wilaya " + WilayaName + "\n n'existe pas ", false);
                mb.messageErreur.Visibility = Visibility.Hidden;
                mb.casSpecial.Content = "Ajouter Dataset";
                mb.casSpecial.Visibility = Visibility.Visible;
                mb.casSpecial.Click += (object Sender, RoutedEventArgs a) =>
                {
                    MainWindow mw = App.Current.MainWindow as MainWindow;
                    if (mw.isLoggedIn)
                    {
                        DataSet d = new DataSet();
                        d.ComboWilaya.Text = WilayaName;
                        d.ShowDialog();
                        mb.Close();
                    }
                    else
                    {
                        mb.messageErreur.Visibility = Visibility.Visible;
                        mb.messageErreur.Text = "Vous n'etes pas un admin";
                    }
                };
                mb.ShowDialog();
            }
            if (File.Exists(config.getWilaya(WilayaName).path))
            {
                InitializeComponent();
                date1.SelectedDate = date2.SelectedDate = DateTime.Today;
                this.WilayaName = WilayaName;
                inputSynthese = new InputSynthesePrime(DateTime.Today,
                                                       DateTime.Today,
                                                       config.getWilaya(WilayaName).path,
                                                       config.getWilaya(WilayaName).path);
                done = true;
            }
            else
            {
                Synthese synth = (App.Current.MainWindow as MainWindow).main.Content as Synthese;
                //synth.pageouverte = 0;
                //synth.Comparaison.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#556060"));
                synth.ErreurMessage.Text = "Le chemin de la wilaya " + WilayaName + "\n n'existe pas ";
                synth.contenuErreur.Visibility = Visibility.Visible;
            }
            done = true;
        }
        /**********   Méthodes  **************/

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

        private int WilayaToID()
        {
            List<string> ListeWilaya = File.ReadAllLines(@"..\..\assets\Climat\wilaya.txt").ToList();
            return ListeWilaya.IndexOf(WilayaName);
        }

        public List<string> GetLabels()
        {
            String test;
            List<Observation> Obs = new List<Observation>();
            Obs = inputSynthese.periode;
            List<String> labels = new List<string>();

            if (inputSynthese.ByMonth())
            {
                foreach (var item in Obs)
                {
                    test = item.GetDate().ToString().Substring(3, 7);
                    if (labels.Contains(test) == false)
                        labels.Add(test);
                }
                return labels;
            }

            foreach (var item in Obs)
            {
                test = item.GetDate().ToString().Substring(0, 10);
                if (labels.Contains(test) == false)
                    labels.Add(test);
            }
            return labels;

        }

        public string GetTitle()
        {
            string title = "Comparaison de ";
            if (TemperatureCheckbox.IsChecked == true)
            {
                title += "la température";
            }
            else
            {
                if (HumiditeCheckbox.IsChecked == true)
                {
                    title += "l'humidité";
                }
                else
                {
                    if (DirectionVentCheckbox.IsChecked == true)
                    {
                        title += "la direction du vent";
                    }
                    else
                    {
                        if (VitesseVentCheckbox.IsChecked == true)
                        {
                            title += "la vitesse du vent";
                        }
                        else
                        {
                            if (PrecipitationCheckbox.IsChecked == true)
                            {
                                title += "la précipitaion";
                            }
                            else
                            {
                                if (PressionCheckbox.IsChecked == true)
                                {
                                    title += "la pression";
                                }
                            }
                        }
                    }
                }
            }

            title += " entre la wilaya de " + WilayaName + " et la wilaya de " + GetWilaya2();
            title += " du " + date1.SelectedDate.ToString().Substring(0, 10) + " au " + date2.SelectedDate.ToString().Substring(0, 10);
            return title;
        }

        public string GetWilaya2()      //Retourne la wilaya à partir d'un ComboBox
        {
            
                Wilaya value = (Wilaya)ListDeWilaya.SelectedValue;
                return value.wilaya;

        }

        private ChartValues<double> GetValues(String attribut)
        {
            List<Observation> Obs = new List<Observation>();
            Obs = inputSynthese.periode;
            List<String> labels = GetLabels();
            axisX.Labels = labels;  //On mets les labels dans l'axis
            List<double> Values = new List<double>();

            if (inputSynthese.ByMonth())
            {
                Values = inputSynthese.GetMoyAttribut(attribut);
                return (DoubleToChartValues(Values));
            }
            //else
            Values = inputSynthese.GetAttribut(attribut);
            return DoubleToChartValues(Values);
        }

        private ChartValues<double> GetValues2(String attribut)
        {
            List<Observation> Obs = new List<Observation>();
            Obs = inputSynthese.periode2;
            List<String> labels = GetLabels();
            axisX.Labels = labels;  //On mets les labels dans l'axis
            List<double> Values = new List<double>();

            if (inputSynthese.ByMonth())
            {
                Values = inputSynthese.GetMoyAttribut2(attribut);
                return (DoubleToChartValues(Values));
            }
            //else
            Values = inputSynthese.GetAttribut2(attribut);
            return DoubleToChartValues(Values);
        }

        private void Uncheked(string param)    //Elle permet de supprimer les deux graphes des deux Wilayas
        {
            switch (param)
            {
                case "Température":
                    this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked = this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked
                    = this.PressionCheckbox.IsChecked = false;
                    break;

                case "Humidité":
                    this.TemperatureCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked = this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked
                    = this.PressionCheckbox.IsChecked = false; break;

                case "Vitesse du vent":
                    this.TemperatureCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked = this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked
                    = this.PressionCheckbox.IsChecked = false; break;

                case "Direction du vent":
                    this.TemperatureCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked
                    = this.PressionCheckbox.IsChecked = false; break;

                case "Précipitation":
                    this.TemperatureCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked = this.DirectionVentCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked
                    = this.PressionCheckbox.IsChecked =false; break;

                case "Pression":
                    this.TemperatureCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked = this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked
                    = this.HumiditeCheckbox.IsChecked = false; break;

                default: break; 
            }
            DataGraphClass Nothing = new DataGraphClass();
            X1.Values = X2.Values = Nothing.Values;
            X1.Title = X2.Title = Y.Title = "";
            titre.Text = "";
            Y.LabelFormatter = Nothing.Formatter;
            this.axisX.Labels = new List<String>();

        }

        public ChartValues<double> DoubleToChartValues(List<double> Donnes)  //Cette métohde permet de convertir De ChartValues en Liste de double
        {
            ChartValues<double> D = new ChartValues<double>();
            foreach (var item in Donnes)
            {
                D.Add(item);
            }
            return D;
        }

        public void addGraphe (string nomParam)
        {
            //On recipere les donnes des deux Wilaya 
            string attr = config.getWilaya(WilayaName).getAttr(nomParam);
            ChartValues<double> Values = GetValues(attr);
            ChartValues<double> Values2 = GetValues2(attr);
            if(Values.Count ==0)
            {
                new MessageBoxWindow("Données introuvables entre les dates entrées",false).ShowDialog();
                Uncheked("");
            }
            else
            {
                if (Values2.Count==0)
                {
                    new MessageBoxWindow("Données introuvables entre les dates entrées",false).ShowDialog();
                    Uncheked("");
                }
                else
                {
                    Uncheked(nomParam);
                    DataGraphClass Wilaya1 = new DataGraphClass(nomParam, Values);
                    DataGraphClass Wilaya2 = new DataGraphClass(nomParam, Values2);
                    ///On affecte les valeurs
                    X1.Values = Wilaya1.Values;
                    X2.Values = Wilaya2.Values;

                    Y.LabelFormatter = Wilaya1.Formatter;
                    //On affecte les titres
                    X1.Title = WilayaName;
                    X2.Title = GetWilaya2();
                    axisX.Labels = GetLabels();
                    Y.Title = nomParam;
                    Y.LabelFormatter = Wilaya1.Formatter;
                    titre.Text = GetTitle();
                }
            }
            
        }

        /*************************/
        /******  EVENT   *********/
        /*************************/


        /*******************    Checked    *******************/

        private void TemperatureCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Température");
        }

        private void HumiditeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Humidité");
        }

        private void DirectionVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Direction du vent");
        }

        private void VitesseVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Vitesse du vent");
        }

        private void PrecipitationCheckbox_Checked(object sender, RoutedEventArgs e)
        {
           addGraphe("Précipitation");
        }

        private void PressionCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Pression");
        }


        /*******************    UnChecked    *******************/
        private void TemperatureCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked("");
        }

        private void HumiditeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked("");
        }

        private void DirectionVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked(" ");
        }

        private void VitesseVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked("");
        }

        private void PrecipitationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked("");
        }

        private void PressionCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked("");
        }


        /***********     Changed   ********/

        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (done)
            {
                this.inputSynthese.SetDate1(this.date1);
                TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked =
                     PrecipitationCheckbox.IsChecked = PressionCheckbox.IsChecked = false;
                titre.Text = "";
            }
        }

        private void Date2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (done)
            {
                this.inputSynthese.SetDate2(this.date2);
                TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked =
                    PrecipitationCheckbox.IsChecked = PressionCheckbox.IsChecked = false;
                titre.Text = "";
            }
        }

        private void Wilaya2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (done)
                {
                    ConfigParser config = new ConfigParser();
                    config.import("config.json");
                    string path = config.getWilaya(GetWilaya2()).path;
                    this.inputSynthese.SetPath2(path);
                    TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked =
                    PrecipitationCheckbox.IsChecked = PressionCheckbox.IsChecked = false;
                    titre.Text = "";
                    //On change les donnés
                }
            }
            catch (FileNotFoundException error)
            {

                MessageBoxWindow mb = new MessageBoxWindow("Le chemin de la wilaya " + GetWilaya2() + "\n n'existe pas ", false);
                mb.messageErreur.Visibility = Visibility.Hidden;
                mb.casSpecial.Content = "Ajouter Dataset";
                mb.casSpecial.Visibility = Visibility.Visible;
                mb.casSpecial.Click += (object Sender, RoutedEventArgs a) =>
                {
                    MainWindow mw = App.Current.MainWindow as MainWindow;
                    if (mw.isLoggedIn)
                    {
                        DataSet d = new DataSet();
                        d.ComboWilaya.Text = GetWilaya2();
                        d.ShowDialog();
                        mb.Close();
                    }
                    else
                    {
                        mb.messageErreur.Visibility = Visibility.Visible;
                        mb.messageErreur.Text = "Vous n'etes pas un admin";
                    }
                };
                mb.ShowDialog();
            }
            if (File.Exists(config.getWilaya(GetWilaya2()).path))
            {
                string path = config.getWilaya(GetWilaya2()).path;
                this.inputSynthese.SetPath2(path);
                TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked =
                PrecipitationCheckbox.IsChecked = PressionCheckbox.IsChecked = false;
                titre.Text = "";
            }
            else
            {
                foreach (Wilaya item in ListDeWilaya.Items)
                {
                    if (item.wilaya== WilayaName)
                    {
                        ListDeWilaya.SelectedIndex= ListDeWilaya.Items.IndexOf(item);
                    }
                }
            }
        }


        private class Wilaya
        {
            public string wilaya { get; set; }

            public Wilaya(string wilaya)
            {
                this.wilaya = wilaya;
            }

        }
    }
}
