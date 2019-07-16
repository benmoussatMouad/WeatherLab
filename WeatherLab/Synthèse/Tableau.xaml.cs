using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WeatherLab.ConfigUtils;
using WeatherLab.Data;

namespace WeatherLab.Synthése
{

    /// <summary>
    /// Logique d'interaction pour Tableau.xaml
    /// </summary>
    public partial class Tableau : Page
    {
        bool firstHist = true;
        private Boolean done = false;
        private InputSynthese inputSynthese;
        string WilayaName;
        ConfigParser config;

        public Tableau(String WilayaName)
        {
            try
            {
                config = App.Config;
                config.import("config.json");
                inputSynthese = new InputSynthese(DateTime.Today, DateTime.Today, config.getWilaya(WilayaName).path);
                InitializeComponent();
                date1.SelectedDate = date2.SelectedDate = DateTime.Today;
                this.WilayaName = WilayaName;
                done = true;
            }
            catch (FileNotFoundException error)
            {
                MessageBoxWindow mb = new MessageBoxWindow("Le chemmin de la wilaya " + WilayaName + "\n n'existe pas ", false);
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
                inputSynthese = new InputSynthese(DateTime.Today, DateTime.Today, config.getWilaya(WilayaName).path);
                done = true;
            }
            else
            {
                Synthese synth = (App.Current.MainWindow as MainWindow).main.Content as Synthese;
                //synth.pageouverte = 0;
                //synth.Tableau.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#556060"));
                synth.ErreurMessage.Text = "Le chemin de la wilaya " + WilayaName + "\n n'existe pas ";
                synth.contenuErreur.Visibility = Visibility.Visible;
            }

        }

        //*********   Méthodes**************
        ///

        public void addHyst(string nomParam, string abrev)  //Ajoute un Hystogramme
        {

            if (Can()) //Si un parametre est checké
            {
                string attr = GetTheChekcedAttr();
                List<double> Donnee = inputSynthese.GetAttribut(attr);
                ChartValues<double> values = GetValues(attr, abrev);
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString(GetColor(nomParam));

                if (firstHist)
                {
                    this.Hysto.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = nomParam,
                            Values =values,
                            Stroke=brush,
                            Fill=brush
                        }
                    };
                    firstHist = false;
                }
                else
                {
                    this.Hysto.Series.Add(
                        new ColumnSeries
                        {
                            Title = nomParam,
                            Values = values,
                            Stroke = brush,
                            Fill = brush
                        }
                    );
                }
                DataContext = this;
            }

            else
            {
                (new MessageBoxWindow("Veuillez choisir un paramètre d'abord", false)).ShowDialog();
                Uncheck();
            }
            titre.Text = GetTitle1();
            AxisY.LabelFormatter = values => values + GetFormatter();
        }

        public void removeHyst(string nomParam)  //Supprime un hystogramme
        {
            int i = 0;
            foreach (var item in Hysto.Series)
            {
                if (item.Title == nomParam)
                    break;
                i++;
            }

            if (Hysto.Series.Count > 1)
            {
                if (i < Hysto.Series.Count)
                {
                    Hysto.Series.RemoveAt(i);
                    titre.Text = GetTitle1();
                }
            }
            else
            {
                firstHist = true;
                Hysto.Series.Clear();
                titre.Text = "";
            }
        }

        public string GetColor(String param)
        {
            switch (param)
            {
                case "Minimum": return "#7b9eae";
                case "Maximum": return "#75bac8";
                case "Médiane": return "#afafaf";
                case "Moyenne": return "#707070";
                case "Variance": return "#4ebba7";
                default: return "#FFFFFF";
            }
        }

        public string GetTitle1()
        {
            string title = "Histogramme sur le parametre " + GetTheCheckedAttribut() + " contenant les valeurs Statistiques : ";
            if (min.IsChecked == true)
            {
                title += "le minimum ";
                if (max.IsChecked == true)
                {
                    title += ", le maximum";

                }
                else
                {
                    if (moy.IsChecked == true)
                    {
                        title += ", la moyenne";
                    }
                    else
                    {
                        if (med.IsChecked == true)
                        {
                            title += ", la médiane";
                        }
                        else
                        {
                            if (var.IsChecked == true)
                            {
                                title += ", la variance";
                            }
                        }
                    }
                }
            }
            else
            {
                if (max.IsChecked == true)
                {
                    title += "le maximum";
                    if (moy.IsChecked == true)
                    {
                        title += ", la moyenne";
                    }
                    else
                    {
                        if (med.IsChecked == true)
                        {
                            title += ", la médiane";
                        }
                        else
                        {
                            if (var.IsChecked == true)
                            {
                                title += ", la variance";
                            }
                        }
                    }

                }
                else
                {
                    if (moy.IsChecked == true)
                    {
                        title += "la moyenne";
                        if (med.IsChecked == true)
                        {
                            title += ", la médiane";
                        }
                        else
                        {
                            if (var.IsChecked == true)
                            {
                                title += ", la variance";
                            }
                        }
                    }
                    else
                    {
                        if (med.IsChecked == true)
                        {
                            title += "la médiane";
                            if (var.IsChecked == true)
                            {
                                title += ", la variance";
                            }
                        }
                        else
                        {
                            if (var.IsChecked == true)
                            {
                                title += "la variance";
                            }
                        }
                    }
                }
            }

            title += " de la wilaya " + WilayaName;
            title += " du " + date1.SelectedDate.ToString().Substring(0, 10);
            title += " au " + date2.SelectedDate.ToString().Substring(0, 10) + ".";
            return title;
        }



        public string[] GetLabels()  //Retourne les labels
        {
            String test;
            List<Observation> Obs = new List<Observation>();
            Obs = inputSynthese.periode;
            List<String> labels = new List<string>();
            foreach (var item in Obs)
            {
                test = item.GetDate().ToString().Substring(6, 4);
                if (labels.Contains(test) == false)
                    labels.Add(test);
            }

            return labels.ToArray();
        }

        public string GetTheChekcedAttr() //Retourne le config de l'attrubut checké
        {
            if (TemperatureCheckbox.IsChecked == true)
            {
                return "T";
            }
            else
            {
                if (HumiditeCheckbox.IsChecked == true)
                {
                    return "U";
                }
                else
                {
                    if (DirectionVentCheckbox.IsChecked == true)
                    {
                        return "DD";
                    }
                    else
                    {
                        if (VitesseVentCheckbox.IsChecked == true)
                        {
                            return "Ff";
                        }
                        else
                        {
                            if (PrecipitationCheckbox.IsChecked == true)
                            {
                                return "RRR";
                            }
                            else
                            {
                                if (PressionCheckbox.IsChecked == true)
                                {
                                    return "P";
                                }
                                else return "";
                            }
                        }
                    }
                }
            }
        }

        public string GetTheCheckedAttribut()
        {
            if (TemperatureCheckbox.IsChecked == true)
            {
                return "Température";
            }
            else
            {
                if (HumiditeCheckbox.IsChecked == true)
                {
                    return "Humidité";
                }
                else
                {
                    if (DirectionVentCheckbox.IsChecked == true)
                    {
                        return "Direction du vent";
                    }
                    else
                    {
                        if (VitesseVentCheckbox.IsChecked == true)
                        {
                            return "Vitesse du vent";
                        }
                        else
                        {
                            if (PrecipitationCheckbox.IsChecked == true)
                            {
                                return "Précipitation";
                            }
                            else
                            {
                                if (PressionCheckbox.IsChecked == true)
                                {
                                    return "Pression";
                                }
                                else return "";
                            }
                        }
                    }
                }
            }
        }

        public string GetFormatter()  //Retourne le label formatter
        {
            if (TemperatureCheckbox.IsChecked == true)
            {
                return "°";
            }
            else
            {
                if (HumiditeCheckbox.IsChecked == true)
                {
                    return "%";
                }
                else
                {
                    if (DirectionVentCheckbox.IsChecked == true)
                    {
                        return "°";
                    }
                    else
                    {
                        if (VitesseVentCheckbox.IsChecked == true)
                        {
                            return "km/h";
                        }
                        else
                        {
                            if (PrecipitationCheckbox.IsChecked == true)
                            {
                                return "mm";
                            }
                            else
                            {
                                if (PressionCheckbox.IsChecked == true)
                                {
                                    return "%";
                                }
                                else return "";
                            }
                        }
                    }
                }
            }
        }

        public bool Can()  //Retourne vrai si un parametre est checker, faux sinon
        {
            return (this.TemperatureCheckbox.IsChecked == true ||
                    this.HumiditeCheckbox.IsChecked == true ||
                    this.VitesseVentCheckbox.IsChecked == true ||
                    this.DirectionVentCheckbox.IsChecked == true ||
                    this.PrecipitationCheckbox.IsChecked == true ||
                    this.PressionCheckbox.IsChecked == true
                    );
        }

        public ChartValues<double> GetValues(string attr, string stat)
        {
            Dictionary<int, List<double>> map = new Dictionary<int, List<double>>();
            map = inputSynthese.GetMap(attr);
            ChartValues<double> valuesToReturn = new ChartValues<double>();
            List<string> labels = new List<string>();
            switch (stat)
            {
                case "min":
                    foreach (var item in map)
                    {
                        labels.Add(item.Key.ToString());
                        valuesToReturn.Add(Math.Round(Static.Minimum(item.Value), 2));

                    }
                    break;

                case "max":
                    foreach (var item in map)
                    {
                        labels.Add(item.Key.ToString());
                        valuesToReturn.Add(Math.Round(Static.Maximum(item.Value), 2));

                    }
                    break;

                case "moy":
                    foreach (var item in map)
                    {
                        labels.Add(item.Key.ToString());
                        valuesToReturn.Add(Math.Round(Static.Moyenne(item.Value), 2));

                    }
                    break;

                case "med":
                    foreach (var item in map)
                    {
                        labels.Add(item.Key.ToString());
                        valuesToReturn.Add(Math.Round(Static.Mediane(item.Value), 2));

                    }
                    break;

                case "var":
                    foreach (var item in map)
                    {
                        labels.Add(item.Key.ToString());
                        valuesToReturn.Add(Math.Round(Static.Variance(item.Value), 2));

                    }
                    break;

                default: break;
            }

            AxisX.Labels = labels;
            return valuesToReturn;
        }

        public void Uncheck()
        {
            this.min.IsChecked = this.max.IsChecked = this.moy.IsChecked = this.med.IsChecked = this.var.IsChecked = false;
        }

        /************************
        /******  EVENT   *********/
        /*************************/


        /*******************    Checked    *******************/

        private void TemperatureCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            string attr = config.getWilaya(WilayaName).getAttr("Température");
            List<double> Donnee = inputSynthese.GetAttribut(attr);
            this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = this.PressionCheckbox.IsChecked = false;
            Uncheck();
            AxisY.Title = "Température";
        }

        private void HumiditeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            string attr = config.getWilaya(WilayaName).getAttr("Humidité");
            List<double> Donnee = inputSynthese.GetAttribut(attr);
            this.TemperatureCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = this.PressionCheckbox.IsChecked = false;
            Uncheck();
            AxisY.Title = "Humidité";
        }

        private void VitesseVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            string attr = config.getWilaya(WilayaName).getAttr("Vitesse du vent");
            List<double> Donnee = inputSynthese.GetAttribut(attr);
            this.HumiditeCheckbox.IsChecked = this.TemperatureCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = this.PressionCheckbox.IsChecked = false;
            Uncheck();
            AxisY.Title = "Vitesse du vent";
        }

        private void DirectionVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            string attr = config.getWilaya(WilayaName).getAttr("Direction du vent");
            List<double> Donnee = inputSynthese.GetAttribut(attr);
            this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.TemperatureCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = this.PressionCheckbox.IsChecked = false;
            Uncheck();
            AxisY.Title = "Direction du vent";
        }

        private void PrecipitationCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            string attr = config.getWilaya(WilayaName).getAttr("Précipitation");
            List<double> Donnee = inputSynthese.GetAttribut(attr);
            this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.TemperatureCheckbox.IsChecked = this.PressionCheckbox.IsChecked = false;
            Uncheck();
            AxisY.Title = "Précipitation";
        }

        private void PressionCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            string attr = config.getWilaya(WilayaName).getAttr("Pression");
            List<double> Donnee = inputSynthese.GetAttribut(attr);
            this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = this.TemperatureCheckbox.IsChecked = false;
            Uncheck();
            AxisY.Title = "Pression";
        }

        ///  /// /////////////////////////
        ///  


        private void Min_Checked(object sender, RoutedEventArgs e)
        {
            addHyst("Minimum", "min");
        }

        private void Max_Checked(object sender, RoutedEventArgs e)
        {
            addHyst("Maximum", "max");
        }

        private void Moy_Checked(object sender, RoutedEventArgs e)
        {
            addHyst("Moyenne", "moy");
        }

        private void Med_Checked(object sender, RoutedEventArgs e)
        {
            addHyst("Médiane", "med");
        }

        private void Var_Checked(object sender, RoutedEventArgs e)
        {
            addHyst("Variance", "var");
        }


        /******************  Unchecked    *******************/

        private void TemperatureCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheck();
            AxisX.Title = AxisY.Title = "";
            AxisY.LabelFormatter = null;
            AxisX.Labels = null;

        }

        private void HumiditeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheck();
            AxisX.Title = AxisY.Title = "";
            AxisY.LabelFormatter = null;
            AxisX.Labels = null;
        }

        private void VitesseVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheck();
            AxisX.Title = AxisY.Title = "";
            AxisY.LabelFormatter = null;
            AxisX.Labels = null;
        }

        private void DirectionVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheck();
            AxisX.Title = AxisY.Title = "";
            AxisY.LabelFormatter = null;
            AxisX.Labels = null;
        }

        private void PrecipitationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheck();
            AxisX.Title = AxisY.Title = "";
            AxisY.LabelFormatter = null;
            AxisX.Labels = null;
        }

        private void PressionCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheck();
            AxisX.Title = AxisY.Title = "";
            AxisY.LabelFormatter = null;
            AxisX.Labels = null;
        }

        private void Min_Unchecked(object sender, RoutedEventArgs e)
        {
            removeHyst("Minimum");
        }

        private void Max_Unchecked(object sender, RoutedEventArgs e)
        {
            removeHyst("Maximum");
        }

        private void Moy_Unchecked(object sender, RoutedEventArgs e)
        {
            removeHyst("Moyenne");
        }

        private void Med_Unchecked(object sender, RoutedEventArgs e)
        {
            removeHyst("Médiane");
        }

        private void Var_Unchecked(object sender, RoutedEventArgs e)
        {
            removeHyst("Variance");
        }


        /*************   Changed   **************/

        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (done)
            {
                this.inputSynthese.SetDate1(this.date1);
                this.TemperatureCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = this.PressionCheckbox.IsChecked = false;

                Uncheck();
                titre.Text = "";
                //On recupere les autre donne => inputs.donne= ###
            }
        }

        private void Date2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (done)
            {
                this.inputSynthese.SetDate2(this.date2);
                this.TemperatureCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = this.PressionCheckbox.IsChecked = false;
                Uncheck();
                titre.Text = "";
                //On recupere les autre donne => inputs.donne= ###
            }
        }

    }
}
