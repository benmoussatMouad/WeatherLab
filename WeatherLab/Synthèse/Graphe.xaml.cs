using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
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
    /// Logique d'interaction pour Graphe.xaml
    /// </summary>
    public partial class Graphe : Page
    {
        private Boolean firstgraph = true;
        private Boolean done = false; //J'en aurais besoin pour les initialisations des wilayas et des dates
        private InputSynthese inputSynthese;
        private ConfigParser config;
        public string WilayaName;

        public Graphe(string WilayaName)
        {
            try
            {
                config = App.Config;
                config.import("config.json");
                inputSynthese = new InputSynthese(DateTime.Today, DateTime.Today, config.getWilaya(WilayaName).path);
                InitializeComponent();
                date1.SelectedDate =  date2.SelectedDate = DateTime.Today;
                this.WilayaName = WilayaName;
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
                inputSynthese = new InputSynthese(DateTime.Today, DateTime.Today, config.getWilaya(WilayaName).path);
                done = true;
            }
            else
            {
                Synthese synth = (App.Current.MainWindow as MainWindow).main.Content as Synthese;
                //synth.pageouverte = 0;
                //synth.Graphe.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#556060"));
                synth.ErreurMessage.Text = "Le chemin de la wilaya " + WilayaName + "\n n'existe pas ";
                synth.contenuErreur.Visibility = Visibility.Visible;
            }
        }

        /**********   Méthodes  **************/

        public List<string> GetLAbels()
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
            string title = "Évolution de ";
            if (TemperatureCheckbox.IsChecked == true)
            {
                title += "la température";
                if (HumiditeCheckbox.IsChecked == true)
                {
                    title += ", l'humidité";
                }
                if (VitesseVentCheckbox.IsChecked == true)
                {
                    title += ", la vitesse du vent";
                }
                if (DirectionVentCheckbox.IsChecked == true)
                {
                    title += ", la direction du vent";
                }
                if (PrecipitationCheckbox.IsChecked == true)
                {
                    title += ", la précipitation";
                }
                if (PressionCheckbox.IsChecked == true)
                {
                    title += ", la pression";
                }
            }
            else
            {
                if (HumiditeCheckbox.IsChecked == true)
                {
                    title += "l'humidité";
                    if (VitesseVentCheckbox.IsChecked == true)
                    {
                        title += ", la vitesse du vent";
                    }
                    if (DirectionVentCheckbox.IsChecked == true)
                    {
                        title += ", la direction du vent";
                    }
                    if (PrecipitationCheckbox.IsChecked == true)
                    {
                        title += ", la précipitation";
                    }
                    if (PressionCheckbox.IsChecked == true)
                    {
                        title += ", la pression";
                    }
                }
                else
                {
                    if (VitesseVentCheckbox.IsChecked == true)
                    {
                        title += "la vitese du vent";
                        if (DirectionVentCheckbox.IsChecked == true)
                        {
                            title += ", la direction du vent";
                        }
                        if (PrecipitationCheckbox.IsChecked == true)
                        {
                            title += ", la précipitation";
                        }
                        if (PressionCheckbox.IsChecked == true)
                        {
                            title += ", la pression";
                        }
                    }
                    else
                    {
                        if (DirectionVentCheckbox.IsChecked == true)
                        {
                            title += "la direction du vent";
                            if (PrecipitationCheckbox.IsChecked == true)
                            {
                                title += ", la précipitation";
                            }
                            if (PressionCheckbox.IsChecked == true)
                            {
                                title += ", la pression";
                            }
                        }
                        else
                        {
                            if (PrecipitationCheckbox.IsChecked == true)
                            {
                                title += "la précipitation";
                                if (PressionCheckbox.IsChecked == true)
                                {
                                    title += ", la pression";

                                }
                                if (PressionCheckbox.IsChecked == true)
                                {
                                    title += ", la pression";
                                }
                            }
                            else
                            {
                                if (PressionCheckbox.IsChecked == true)
                                {
                                    title += "la pression";
                                    if (PressionCheckbox.IsChecked == true)
                                    {
                                        title += ", la pression";
                                    }

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
            }


            title += " de la wilaya de " + WilayaName;
            title += " du " + date1.SelectedDate.ToString().Substring(0, 10);
            title += " au " + date2.SelectedDate.ToString().Substring(0, 10);
            return title;
        }

        private ChartValues<double> GetValues(String attribut)
        {
            List<Observation> Obs = new List<Observation>();
            Obs = inputSynthese.periode;
            List<String> labels = GetLAbels();
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

        public LiveCharts.IChartValues GetDataChart(string name)   //Cette méthode retourne des donnés de graphe
        {
            foreach (var item in Chart.Series)
            {
                if (item.Title == name)
                {
                    return item.Values;
                }
            }
            return (Chart.Series.ElementAt(Chart.Series.Count - 1).Values);
        }

        public List<double> ChartValuesToDouble(LiveCharts.IChartValues Donnes)  //Cette métohde permet de convertir De ChartValues en Liste de double
        {
            List<double> D = new List<double>();
            foreach (var item in Donnes)
            {
                D.Add((double)item);
            }
            return D;
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

        public string GetColor(String param)
        {
            switch (param)
            {
                case "Température": return "#7b9eae";
                case "Humidité": return "#75bac8";
                case "Vitesse du vent": return "#302904";
                case "Direction du vent": return "#707070";
                case "Précipitation": return "#4ebba7"; 
                case "Pression": return "#afafaf";
                default: return "#FFFFFF";
            }
        }

        public void addGraphe (string nomParam)
        {
            string attr = config.getWilaya(WilayaName).getAttr(nomParam);
            ChartValues<double> Values = GetValues(attr);
            List<string> Labels = GetLAbels();
            DataGraphClass T = new DataGraphClass(nomParam, Values);
            if (T.Values.Count != 0)
            {
                this.axisX.Labels = Labels;
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString(GetColor(nomParam));

                if (this.firstgraph)
                {
                    X.Values = T.Values;                // Xrepresente le premier Series
                    X.ScalesYAt = 0;                    // Y represente le premier AxisY 
                    X.Title = nomParam;
                    Y.Title = nomParam;
                    Y.LabelFormatter = T.Formatter;
                    Y.Foreground = X.Stroke = brush;
                    firstgraph = false;
                    titre.Text = GetTitle();
                }
                else
                {
                    Chart.AxisY.Add(new Axis { Title = nomParam, LabelFormatter = T.Formatter, Foreground = brush });
                    Chart.Series.Add(new LineSeries
                    {
                        Title = nomParam,
                        ScalesYAt = Chart.AxisY.Count - 1,
                        Values = T.Values,
                        Stroke = brush,
                        Fill = Brushes.Transparent,
                        LineSmoothness = 1,
                        PointGeometrySize = 5
                    });   /////////////////////////////je dois midfier partout
                    firstgraph = false;
                    titre.Text = GetTitle();
                }
            }
            else
            {
                new MessageBoxWindow("Le Dataset ne contient aucune valeur sur "+ nomParam + " entre le " +
                    date1.SelectedDate.ToString().Substring(0, 11) +
                    " et le " + date2.SelectedDate.ToString().Substring(0, 11),
                    false).Show();
            }
        }

        public void deleteGraphe (string nomParam)
        {
            int i = 0;
            foreach (LineSeries A in Chart.Series)
            {
                if (A.Title == nomParam)
                    break;
                i++;
            }
            if (Chart.Series.Count > 1)
            {
                if (i > 0)
                {
                    Chart.Series.RemoveAt(i);
                    Chart.AxisY.RemoveAt(i);
                    for (int j = i; j < Chart.Series.Count; j++)
                    {
                        Chart.Series.ElementAt(j).ScalesYAt = Chart.Series.ElementAt(j).ScalesYAt - 1;
                    }
                }
                else
                if (i == 0)
                {
                    Y.Title = Chart.AxisY.ElementAt(1).Title;
                    Y.LabelFormatter = Chart.AxisY.ElementAt(1).LabelFormatter;
                    X.Title = Chart.Series.ElementAt(1).Title;
                    X.Values = Chart.Series.ElementAt(1).Values;
                    var converter = new BrushConverter();
                    var brush = (Brush)converter.ConvertFromString(GetColor(X.Title));
                    X.Stroke = brush;
                    X.ScalesYAt = 0;
                    Chart.Series.RemoveAt(1);
                    Chart.AxisY.RemoveAt(1);
                    for (int j = 1; j < Chart.Series.Count; j++)
                    {
                        Chart.Series.ElementAt(j).ScalesYAt = Chart.Series.ElementAt(j).ScalesYAt - 1;
                    }
                }
                titre.Text = GetTitle();
            }
            else        //Il reste un seule graphe
            {
                firstgraph = true;
                X.Title = "";
                X.Values = new ChartValues<double> { };
                DataGraphClass Nothing = new DataGraphClass();
                Y.Title = "";
                Y.LabelFormatter = Nothing.Formatter;
                titre.Text = "";
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

        private void VitesseVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Vitesse du vent");
        }

        private void DirectionVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Direction du vent");
        }

        private void PrecipitationCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Précipitation");
        }

        private void PressionCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            addGraphe("Pression");
        }

        /******************  Unchecked    *******************/

        private void TemperatureCheckbox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            deleteGraphe("Température");
        }

        private void HumiditeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            deleteGraphe("Humidité");
        }

        private void VitesseVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            deleteGraphe("Vitesse du vent");
        }

        private void DirectionVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            deleteGraphe("Direction du vent");
        }

        private void PrecipitationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            deleteGraphe("Précipitation");
        }

        private void PressionCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            deleteGraphe("Pression");
        }

        /*************   Changed   **************/

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ImportData> Data = new List<ImportData>(); //Inputs.periode

                if (this.TemperatureCheckbox.IsChecked == true)
                {
                    var ChartsData = GetDataChart("Température");       //Je récupere les données des charts
                    var DoubleData = ChartValuesToDouble(ChartsData);   //Je les convertie en liste de double List<double>
                    ImportData TempData = new ImportData(DoubleData, "Température");
                    Data.Add(TempData);
                }

                if (this.HumiditeCheckbox.IsChecked == true)
                {
                    var ChartsData = GetDataChart("Humidité");       //Je récupere les données des charts
                    var DoubleData = ChartValuesToDouble(ChartsData);   //Je les convertie en liste de double List<double>
                    ImportData HumData = new ImportData(DoubleData, "Humidité");
                    Data.Add(HumData);
                }

                if (this.VitesseVentCheckbox.IsChecked == true)
                {
                    var ChartsData = GetDataChart("Vitesse du vent");       //Je récupere les données des charts
                    var DoubleData = ChartValuesToDouble(ChartsData);   //Je les convertie en liste de double List<double>
                    ImportData VVData = new ImportData(DoubleData, "Vitesse du vent");
                    Data.Add(VVData);
                }

                if (this.DirectionVentCheckbox.IsChecked == true)
                {
                    var ChartsData = GetDataChart("Direction du vent");       //Je récupere les données des charts
                    var DoubleData = ChartValuesToDouble(ChartsData);   //Je les convertie en liste de double List<double>
                    ImportData DVData = new ImportData(DoubleData, "Direction du vent");
                    Data.Add(DVData);
                }

                if (this.PrecipitationCheckbox.IsChecked == true)
                {
                    var ChartsData = GetDataChart("Précipitation");       //Je récupere les données des charts
                    var DoubleData = ChartValuesToDouble(ChartsData);   //Je les convertie en liste de double List<double>
                    ImportData PrecData = new ImportData(DoubleData, "Précipitation");
                    Data.Add(PrecData);
                }

                if (this.PressionCheckbox.IsChecked == true)
                {
                    var ChartsData = GetDataChart("Pression");       //Je récupere les données des charts
                    var DoubleData = ChartValuesToDouble(ChartsData);   //Je les convertie en liste de double List<double>
                    ImportData PrecData = new ImportData(DoubleData, "Pression");
                    Data.Add(PrecData);
                }

                OpenFileDialog openFiledialog = new OpenFileDialog();
                openFiledialog.Filter = "Text files (*.xlsx)|*.xlsx";
                openFiledialog.ShowDialog();            //Je recupere le nom du fichier 
                if (File.Exists(openFiledialog.FileName))
                {
                    ExcelClass excel = new ExcelClass(openFiledialog.FileName, 1);
                    excel.WriteDate(axisX.Labels.ToList());
                    excel.WriteRange(Data);
                    //excel.SaveAs(openFiledialog.FileName+"\\"+ GetTitle());
                    excel.Save();
                    excel.Close();
                    new MessageBoxWindow("Les données on était exportées avec succés !!!", true).ShowDialog();
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }

            catch (FileNotFoundException exception)
            {
                new MessageBoxWindow("Veuillez choisir un fichier valide", false).ShowDialog();
            }
        }

        private void Chart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
