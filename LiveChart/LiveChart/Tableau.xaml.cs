using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Logique d'interaction pour Tableau.xaml
    /// </summary>
    public partial class Tableau : Page
    {
        //private ManipDs Inputs
        DataTable dt = new DataTable();
        private Boolean done = false;

        public Tableau()
        {
            InitializeComponent();
            done = true;
            InitDataGrider();
            date1.SelectedDate = DateTime.Today;
            date2.SelectedDate = DateTime.Today;
        }

        /**********   Méthodes  **************/

        private void InitDataGrider ()      //Cette méthode initialise les colonnes du DataGrid
        {
            DataColumn param = new DataColumn("Paramétre",typeof(string));
            DataColumn min = new DataColumn("Minimum", typeof(double));
            DataColumn max = new DataColumn("Maximum", typeof(double));
            DataColumn moy = new DataColumn("Moyenne", typeof(double));
            DataColumn med = new DataColumn("Médiane", typeof(double));
            DataColumn var = new DataColumn("Variance", typeof(double));
            dt.Columns.Add(param);
            dt.Columns.Add(min);
            dt.Columns.Add(max);
            dt.Columns.Add(moy);
            dt.Columns.Add(med);
            dt.Columns.Add(var);
        }

        public string GetTitle()        //Retourne le titre 
        {
            string title = "Tableau contenant des données statistiques ";
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
                            else
                                return "";
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
            return (value.Content.ToString().Substring(3, value.Content.ToString().Length - 3));
        }

        /*************************/
        /******  EVENT   *********/
        /*************************/


        /*******************    Checked    *******************/

        private void TemperatureCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            List<double> Donnee = new List<double> { 2, 5, 8, 9 };
            // Donnee = this.InputScope.GetAttribute("Temperature");

            DataRow row = this.dt.NewRow();
            row[0] = "Température";
            row[1] = Static.Minimum(Donnee);
            row[2] = Static.Maximum(Donnee);
            row[3] = Static.Moyenne(Donnee);
            row[4] = Static.Mediane(Donnee);
            row[5] = Static.Variance(Donnee);
            

            this.dt.Rows.Add(row);
            this.dataGrid.ItemsSource = dt.DefaultView;
            this.titre.Text = GetTitle();
        }

        private void HumiditeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            List<double> Donnee = new List<double> { 2, 5, 8, 9 };
            // Donnee = this.InputScope.GetAttribute("humidité");

            DataRow row = this.dt.NewRow();
            row[0] = "Humidité";
            row[1] = Static.Minimum(Donnee);
            row[2] = Static.Maximum(Donnee);
            row[3] = Static.Moyenne(Donnee);
            row[4] = Static.Mediane(Donnee);
            row[5] = Static.Variance(Donnee);


            this.dt.Rows.Add(row);
            this.dataGrid.ItemsSource = dt.DefaultView;
            this.titre.Text = GetTitle();
        }

        private void VitesseVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            List<double> Donnee = new List<double> { 2, 5, 8, 9 };
            // Donnee = this.InputScope.GetAttribute("viesse du vent");

            DataRow row = this.dt.NewRow();
            row[0] = "Vitesse du vent";
            row[1] = Static.Minimum(Donnee);
            row[2] = Static.Maximum(Donnee);
            row[3] = Static.Moyenne(Donnee);
            row[4] = Static.Mediane(Donnee);
            row[5] = Static.Variance(Donnee);


            this.dt.Rows.Add(row);
            this.dataGrid.ItemsSource = dt.DefaultView;
            this.titre.Text = GetTitle();
        }

        private void DirectionVentCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            List<double> Donnee = new List<double> { 2, 5, 8, 9 };
            // Donnee = this.InputScope.GetAttribute("directio  du vent");

            DataRow row = this.dt.NewRow();
            row[0] = "Direction du vent";
            row[1] = Static.Minimum(Donnee);
            row[2] = Static.Maximum(Donnee);
            row[3] = Static.Moyenne(Donnee);
            row[4] = Static.Mediane(Donnee);
            row[5] = Static.Variance(Donnee);


            this.dt.Rows.Add(row);
            this.dataGrid.ItemsSource = dt.DefaultView;
            this.titre.Text = GetTitle();
        }

        private void PrecipitationCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            //On recuperer les donnes dans une liste 
            List<double> Donnee = new List<double> { 2, 5, 8, 9 };
            // Donnee = this.InputScope.GetAttribute("Precipitation");

            DataRow row = this.dt.NewRow();
            row[0] = "Précipitation";
            row[1] = Static.Minimum(Donnee);
            row[2] = Static.Maximum(Donnee);
            row[3] = Static.Moyenne(Donnee);
            row[4] = Static.Mediane(Donnee);
            row[5] = Static.Variance(Donnee);


            this.dt.Rows.Add(row);
            this.dataGrid.ItemsSource = dt.DefaultView;
            this.titre.Text = GetTitle();
        }

        /******************  Unchecked    *******************/

        private void TemperatureCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if ((String)item[0] == "Température")
                {
                    dt.Rows.Remove(item);
                    this.dataGrid.ItemsSource = dt.DefaultView;
                    this.titre.Text = GetTitle();
                    return;
                }
            }
        }

        private void HumiditeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if ((String)item[0] == "Humidité")
                {
                    dt.Rows.Remove(item);
                    this.dataGrid.ItemsSource = dt.DefaultView;
                    this.titre.Text = GetTitle();
                    return;
                }
            }
        }

        private void VitesseVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if ((String)item[0] == "Vitesse du vent")
                {
                    dt.Rows.Remove(item);
                    this.dataGrid.ItemsSource = dt.DefaultView;
                    this.titre.Text = GetTitle();
                    return;
                }
            }
        }

        private void DirectionVentCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if ((String)item[0] == "Direction du vent")
                {
                    dt.Rows.Remove(item);
                    this.dataGrid.ItemsSource = dt.DefaultView;
                    this.titre.Text = GetTitle();
                    return;
                }
            }
        }

        private void PrecipitationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow item in dt.Rows)
            {
                if ((String)item[0] == "Précipitation")
                {
                    dt.Rows.Remove(item);
                    this.dataGrid.ItemsSource = dt.DefaultView;
                    this.titre.Text = GetTitle();
                    return;
                }
            }
        }

        /*************   Changed   **************/

        private void Wilaya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (done)
            {
                this.TemperatureCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = false;
                titre.Text = "";
                //On recupere les autre donne => inputs.donne= ###
            }            
        }

        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (done)
            {
                this.TemperatureCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = false;
                titre.Text = "";
                //On recupere les autre donne => inputs.donne= ###
            }
        }

        private void Date2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (done)
            {
                this.TemperatureCheckbox.IsChecked = this.HumiditeCheckbox.IsChecked = this.VitesseVentCheckbox.IsChecked =
                                this.DirectionVentCheckbox.IsChecked = this.PrecipitationCheckbox.IsChecked = false;
                titre.Text = "";
                //On recupere les autre donne => inputs.donne= ###
            }
        }
    }
    
}
