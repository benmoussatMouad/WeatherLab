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
using LiveCharts;
using LiveCharts.Wpf;

namespace LiveChart
{
    /// <summary>
    /// Logique d'interaction pour Graphe.xaml
    /// </summary>
    public partial class Graphe : Page
    {
        public Boolean firstgraph = true;
        public Graphe()
        {
            InitializeComponent();
            date1.SelectedDate = DateTime.Today;
            date2.SelectedDate = DateTime.Today;
        }

        /**********   Méthodes  **************/

        public List<string> GetLabels(DateTime StartDate, DateTime EndDate, Boolean ByMonth)
        {
            string[] Months = new[] { "Jan ", "Fev ", "Mar ", "Avr ", "Mai ", "Juin ", "Juil ", "Aout ", "Sep ", "Oct ", "Nov ", "Dec " };
            List<string> Labels = new List<string>();
            string Label = "";

            if (ByMonth)
            {
                int year1 = StartDate.Year;
                int year2 = EndDate.Year;
                int month1 = StartDate.Month;
                int month2 = EndDate.Month;
                while (year1 < year2)
                {
                    Label = Months[month1 - 1] + year1;
                    Labels.Add(Label);
                    month1++;
                    if (month1 >= 12)
                    {
                        month1 = 1;
                        year1++;
                    }
                }
                //Dans ce cas je suis dans la même année
                while (month1 <= month2)
                {
                    Label = Months[month1 - 1] + year1;
                    Labels.Add(Label);
                    month1++;
                }
                return Labels;
            }

            else                    // On travaille par jour
            {
                int month1 = StartDate.Month;
                int month2 = EndDate.Month;
                int date1 = StartDate.Day;
                int date2 = EndDate.Day;
                int year1 = StartDate.Year;
                int year2 = EndDate.Year;

                if (month1 == 1 || month1 == 3 || month1 == 5 || month1 == 7 || month1 == 8 || month1 == 10)
                {
                    while (month1 < month2)
                    {
                        Label = date1 + " " + Months[month1 - 1] + year1;
                        Labels.Add(Label);
                        date1++;
                        if (date1 == 32)
                        {
                            date1 = 1;
                            month1++;
                        }
                    }
                    //Dans ce cas je suis dans le même mois
                    while (date1 <= date2)
                    {
                        Label = date1 + " " + Months[month1 - 1] + year1;
                        Labels.Add(Label);
                        date1++;
                    }
                }
                else
                {
                    if (month1 == 12)
                    {
                        if (month2 == 12) //Si c'est dans le même mois de fin d'année
                        {
                            while (date1 <= date2)
                            {
                                Label = date1 + " " + Months[month1 - 1] + year1;
                                Labels.Add(Label);
                                date1++;
                            }
                        }
                        else //Sinon => i.e On change l'année 
                        {
                            while (date1 <= 31)
                            {
                                Label = date1 + " " + Months[month1 - 1] + year1;
                                Labels.Add(Label);
                                date1++;
                            }
                            date1 = 1;
                            month1 = 1;
                            year1++;
                            while (month1 < month2)
                            {
                                Label = date1 + " " + Months[month1 - 1] + year1;
                                Labels.Add(Label);
                                date1++;
                                if (date1 == 32)
                                {
                                    month1++;
                                    date1 = 1;
                                }
                            }
                            while (date1 <= date2)
                            {
                                Label = date1 + " " + Months[month1 - 1] + year1;
                                Labels.Add(Label);
                                date1++;
                            }
                        }

                    }

                    else
                    {
                        if (month1 == 2)
                        {
                            if (month2 == 2) //Si c'est dans le même mois
                            {
                                while (date1 <= date2)
                                {
                                    Label = date1 + " " + Months[month1 - 1] + year1;
                                    Labels.Add(Label);
                                    date1++;
                                }
                            }
                            else //Sinon 
                            {
                                while (date1 <= 28)
                                {
                                    Label = date1 + " " + Months[month1 - 1] + year1;
                                    Labels.Add(Label);
                                    date1++;
                                }
                                date1 = 1;
                                month1 = 3;
                                while (month1 < month2)
                                {
                                    Label = date1 + " " + Months[month1 - 1] + year1;
                                    Labels.Add(Label);
                                    date1++;
                                    if (date1 == 31)
                                    {
                                        month1++;
                                        date1 = 1;
                                    }
                                }
                                while (date1 <= date2)
                                {
                                    Label = date1 + " " + Months[month1 - 1] + year1;
                                    Labels.Add(Label);
                                    date1++;
                                }
                            }
                        }
                        else
                        {
                            if (month1 == 4 || month1 == 6 || month1 == 9 || month1 == 11)
                            {
                                while (month1 < month2)
                                {
                                    Label = date1 + " " + Months[month1 - 1] + year1;
                                    Labels.Add(Label);
                                    date1++;
                                    if (date1 == 30)
                                    {
                                        date1 = 1;
                                        month1++;
                                    }
                                }
                                //Dans ce cas je suis dans le même mois
                                while (date1 <= date2)
                                {
                                    Label = date1 + " " + Months[month1 - 1] + year1;
                                    Labels.Add(Label);
                                    date1++;
                                }
                            }
                        }
                    }

                }
            }
            return Labels;
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
                        }
                        else
                        {
                            if (PrecipitationCheckbox.IsChecked == true)
                            {
                                title += "la précipitation";
                            }
                        }
                    }
                }
            }


            title += " de la wilaya de " + GetWilaya();
            title += " de " + date1.SelectedDate.ToString().Substring(0, 10);
            title += " à " + date2.SelectedDate.ToString().Substring(0, 10);
            return title;
        }

        public string GetWilaya()      //Retourne la wilaya à partir d'un ComboBox
        {
            var value = (ComboBoxItem)this.wilaya.SelectedValue;
            return (value.Content.ToString().Substring(3, value.Content.ToString().Length-3));
        }
                          
        
                                /*************************/
                                /******  EVENT   *********/
                                /*************************/
                                

        /*******************    Checked    *******************/
        private void TemperatureCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ChartValues<double> Values = new ChartValues<double> { 3, 4, 6, 8 };
            DataGraphClass T = new DataGraphClass("Température", Values);
            if (this.firstgraph)
            {
                X.Values = T.Values;                // Xrepresente le premier Series
                X.ScalesYAt = 0;                    // Y represente le premier AxisY 
                X.Title = "Température";
                Y.Title = "Température";
                Y.LabelFormatter = T.Formatter;
                firstgraph = false;
                titre.Text = GetTitle();
            }
            else
            {
                Chart.AxisY.Add(new Axis { Title = "Température", LabelFormatter = T.Formatter });
                Chart.Series.Add(new LineSeries { Title = "Température", ScalesYAt = Chart.AxisY.Count - 1, Values = T.Values });
                firstgraph = false;
                titre.Text = GetTitle();
            }

        }

        private void HumiditeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ChartValues<double> Values = new ChartValues<double> { 3, 5, 1, 10 };
            DataGraphClass H = new DataGraphClass("Humidité", Values);
            if (this.firstgraph)
            {
                X.Values = H.Values;                // Xrepresente le premier Series
                X.ScalesYAt = 0;                    // Y represente le premier AxisY 
                X.Title = "Humidité";
                Y.Title = "Humidité";
                Y.LabelFormatter = H.Formatter;
                firstgraph = false;
                titre.Text = GetTitle();
            }
            else
            {
                Chart.AxisY.Add(new Axis { Title = "Humidité", LabelFormatter = H.Formatter });
                Chart.Series.Add(new LineSeries { Title = "Humidité", ScalesYAt = Chart.AxisY.Count - 1, Values = H.Values });
                firstgraph = false;
                titre.Text = GetTitle();
            }
        }

        private void VitesseVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ChartValues<double> Values = new ChartValues<double> { 5, 3, 5, 7 };
            DataGraphClass VV = new DataGraphClass("Vitesse du vent", Values);
            if (this.firstgraph)
            {
                X.Values = VV.Values;                // Xrepresente le premier Series
                X.ScalesYAt = 0;                    // Y represente le premier AxisY 
                X.Title = "Vitesse du vent";
                Y.Title = "Vitesse du vent";
                Y.LabelFormatter = VV.Formatter;
                firstgraph = false;
                titre.Text = GetTitle();
            }
            else
            {
                Chart.AxisY.Add(new Axis { Title = "Vitesse du vent", LabelFormatter = VV.Formatter });
                Chart.Series.Add(new LineSeries { Title = "Vitesse du vent", ScalesYAt = Chart.AxisY.Count - 1, Values = VV.Values });
                firstgraph = false;
                titre.Text = GetTitle();
            }
        }

        private void DirectionVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ChartValues<double> Values = new ChartValues<double> { 15, 3, 6, 17 };
            DataGraphClass DV = new DataGraphClass("Direction du vent", Values);

            if (this.firstgraph)
            {
                X.Values = DV.Values;                // Xrepresente le premier Series
                X.ScalesYAt = 0;                    // Y represente le premier AxisY 
                X.Title = "Direction du vent";
                Y.Title = "Direction du vent";
                Y.LabelFormatter = DV.Formatter;
                firstgraph = false;
                titre.Text = GetTitle();
            }
            else
            {
                Chart.AxisY.Add(new Axis { Title = "Direction du vent", LabelFormatter = DV.Formatter });
                Chart.Series.Add(new LineSeries { Title = "Direction du vent", ScalesYAt = Chart.AxisY.Count - 1, Values = DV.Values });
                firstgraph = false;
                titre.Text = GetTitle();
            }
        }

        private void PrecipitationCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ChartValues<double> Values = new ChartValues<double> { 3, 4, 6, 8 };
            DataGraphClass T = new DataGraphClass("Précipitation", Values);
            if (this.firstgraph)
            {
                X.Values = T.Values;                // Xrepresente le premier Series
                X.ScalesYAt = 0;                    // Y represente le premier AxisY 
                X.Title = "Précipitation";
                Y.Title = "Précipitation";
                Y.LabelFormatter = T.Formatter;
                firstgraph = false;
                titre.Text = GetTitle();
            }
            else
            {
                Chart.AxisY.Add(new Axis { Title = "Précipitation", LabelFormatter = T.Formatter });
                Chart.Series.Add(new LineSeries { Title = "Précipitation", ScalesYAt = Chart.AxisY.Count - 1, Values = T.Values });
                firstgraph = false;
                titre.Text = GetTitle();
            }
        }

        /******************  Unchecked    *******************/

        private void TemperatureCheckbox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (LineSeries A in Chart.Series)
            {
                if (A.Title == "Température")
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

        private void HumiditeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (LineSeries A in Chart.Series)
            {
                if (A.Title == "Humidité")
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

        private void VitesseVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (LineSeries A in Chart.Series)
            {
                if (A.Title == "Vitesse du vent")
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

        private void DirectionVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (LineSeries A in Chart.Series)
            {
                if (A.Title == "Direction du vent")
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

        private void PrecipitationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (LineSeries A in Chart.Series)
            {
                if (A.Title == "Précipitation")
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
        
        /*************   Changed   **************/

        private void Wilaya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked = PrecipitationCheckbox.IsChecked = false;
            titre.Text = "";
            /* On change les donnés*/
        }

        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime DateChoisie = DateClass.DateRecover(date1.SelectedDate.ToString());
            if (DateClass.DateOK(DateChoisie, DateTime.Today))
            {
                TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked = PrecipitationCheckbox.IsChecked = false;
                titre.Text = "";
                //Si la date a été changée alors on va changer les donnes i.e : recuperer d'autre donne
            }
            else
            {
                MessageBox.Show("La date Choisie n'est pas valide");
                date1.SelectedDate = DateTime.Today;
            }
        }

        private void Date2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime DateChoisie = DateClass.DateRecover(date1.SelectedDate.ToString());
            if (DateClass.DateOK(DateChoisie, DateTime.Today))
            {
                TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked = PrecipitationCheckbox.IsChecked = false;
                titre.Text = "";
                //Si la date a été changée alors on va changer les donnes i.e : recuperer d'autre donne
            }
            else
            {
                MessageBox.Show("La date Choisie n'est pas valide");
                date1.SelectedDate = DateTime.Today;
            }
        }
    }
      
}

