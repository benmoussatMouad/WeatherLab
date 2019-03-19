using LiveCharts;
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
    /// Logique d'interaction pour Comparaison.xaml
    /// </summary>
    public partial class Comparaison : Page
    {
        private Boolean done = false;
        public Comparaison()
        {
            InitializeComponent();
            date1.SelectedDate = date2.SelectedDate = DateTime.Today;
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
                        }
                    }
                }
            }

            title += " entre la wilaya de " + GetWilaya1() + " et la wilaya de " + GetWilaya2();
            title += " de " + date1.SelectedDate.ToString().Substring(0, 10) + " à " + date2.SelectedDate.ToString().Substring(0, 10);
            return title;
        }

        public string GetWilaya1()      //Retourne la wilaya à partir d'un ComboBox
        {
            var value = (ComboBoxItem)this.wilaya1.SelectedValue;
            return (value.Content.ToString().Substring(3, value.Content.ToString().Length - 3));
        }

        public string GetWilaya2()      //Retourne la wilaya à partir d'un ComboBox
        {
            var value = (ComboBoxItem)this.wilaya2.SelectedValue;
            return (value.Content.ToString().Substring(3, value.Content.ToString().Length - 3));
        }

        private void Uncheked()    //Elle permet de supprimer les deux graphes des deux Wilayas
        {
            done = false;
            DataGraphClass Nothing = new DataGraphClass();
            X1.Values = X2.Values = Nothing.Values;
            X1.Title = X2.Title = Y.Title = "";
            titre.Text = "";
        }

                                    /*************************/
                                    /******  EVENT   *********/
                                    /*************************/


        /*******************    Checked    *******************/

        private void TemperatureCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (done)
            {
                MessageBox.Show("Vous ne pouvez pas effectuer deux choix au meme temps");
                this.TemperatureCheckbox.IsChecked = false;
            }
            else
            {
                done = true;
                //On recipere les donnes des deux Wilaya 
                ChartValues<double> Values = new ChartValues<double> { 3, 4, 6, 8 };
                DataGraphClass Wilaya1 = new DataGraphClass("Température", Values);
                DataGraphClass Wilaya2 = new DataGraphClass("Température", Values);
                ///On affecte les valeurs
                X1.Values = Wilaya1.Values;
                X2.Values = Wilaya2.Values;

                Y.LabelFormatter = Wilaya1.Formatter;
                //On affecte les titres
                X1.Title = GetWilaya1();
                X2.Title = GetWilaya2();
                Y.Title = "Température (°)";
                titre.Text = GetTitle();
            }
        }

        private void HumiditeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (done)
            {
                MessageBox.Show("Vous ne pouvez pas effectuer deux choix au meme temps");
                this.HumiditeCheckbox.IsChecked = false;
            }
            else
            {
                done = true;
                //On recipere les donnes des deux Wilaya 
                ChartValues<double> Values = new ChartValues<double> { 3, 4, 6, 8 };
                DataGraphClass Wilaya1 = new DataGraphClass("Humidité", Values);
                DataGraphClass Wilaya2 = new DataGraphClass("Humidité", Values);
                ///On affecte les valeurs
                X1.Values = Wilaya1.Values;
                X2.Values = Wilaya2.Values;

                //On affecte les titres
                X1.Title = GetWilaya1();
                X2.Title = GetWilaya2();
                Y.Title = "Humidité (%)";
                titre.Text = GetTitle();
            }
        }
            
        private void DirectionVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (done)
            {
                MessageBox.Show("Vous ne pouvez pas effectuer deux choix au meme temps");
                this.DirectionVentCheckbox.IsChecked = false;
            }
            else
            {
                done = true;
                //On recipere les donnes des deux Wilaya 
                ChartValues<double> Values = new ChartValues<double> { 3, 4, 6, 8 };
                DataGraphClass Wilaya1 = new DataGraphClass("Direction du vent", Values);
                DataGraphClass Wilaya2 = new DataGraphClass("Direction du vent", Values);
                ///On affecte les valeurs
                X1.Values = Wilaya1.Values;
                X2.Values = Wilaya2.Values;

                //On affecte les titres
                X1.Title = GetWilaya1();
                X2.Title = GetWilaya2();
                Y.Title = "Direction du vent (°)";
                titre.Text = GetTitle();
            }
        }

        private void VitesseVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (done)
            {
                MessageBox.Show("Vous ne pouvez pas effectuer deux choix au meme temps");
                this.VitesseVentCheckbox.IsChecked = false;
            }
            else
            {
                done = true;
                //On recipere les donnes des deux Wilaya 
                ChartValues<double> Values = new ChartValues<double> { 3, 4, 6, 8 };
                DataGraphClass Wilaya1 = new DataGraphClass("Vitesse du vent", Values);
                DataGraphClass Wilaya2 = new DataGraphClass("Vitesse du vent", Values);
                ///On affecte les valeurs
                X1.Values = Wilaya1.Values;
                X2.Values = Wilaya2.Values;

                //On affecte les titres
                X1.Title = GetWilaya1();
                X2.Title = GetWilaya2();
                Y.Title = "Vitesse du vent (km/h)";
                titre.Text = GetTitle();
            }
        }

        private void PrecipitationCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (done)
            {
                MessageBox.Show("Vous ne pouvez pas effectuer deux choix au meme temps");
                this.PrecipitationCheckbox.IsChecked = false;
            }
            else
            {
                done = true;
                //On recipere les donnes des deux Wilaya 
                ChartValues<double> Values = new ChartValues<double> { 3, 4, 6, 8 };
                DataGraphClass Wilaya1 = new DataGraphClass("Précipitation", Values);
                DataGraphClass Wilaya2 = new DataGraphClass("Précipitation", Values);
                ///On affecte les valeurs
                X1.Values = Wilaya1.Values;
                X2.Values = Wilaya2.Values;

                //On affecte les titres
                X1.Title = GetWilaya1();
                X2.Title = GetWilaya2();
                Y.Title = "Précipitation (mm)";
                titre.Text = GetTitle();
            }
        }
        

        /*******************    UnChecked    *******************/
        private void TemperatureCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked();
        }
        
        private void HumiditeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked();
        }
                
        private void DirectionVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked();
        }

        private void VitesseVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked();
        }

        private void PrecipitationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheked();
        }

        /***********       Changed   ********/

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
            DateTime DateChoisie = DateClass.DateRecover(date2.SelectedDate.ToString());
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

        private void Wilaya1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked = PrecipitationCheckbox.IsChecked = false;
            titre.Text = "";
            //On change les donnés
        }

        private void Wilaya2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TemperatureCheckbox.IsChecked = HumiditeCheckbox.IsChecked = VitesseVentCheckbox.IsChecked = DirectionVentCheckbox.IsChecked = PrecipitationCheckbox.IsChecked = false;
            titre.Text = "";
            //On change les donnés
        }
    }

}
