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
using MahApps.Metro;
using MahApps.Metro.Controls;
using MaterialDesignThemes;
using Microsoft.Win32;


namespace UIcontrol
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

       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            flyoutDePrediction.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            flyoutDePrediction.IsOpen = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            flyoutDePrediction.IsOpen = true;
          
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Switch1_Checked(object sender, RoutedEventArgs e)
        {
            sliderTemp.IsEnabled = true;
            
        }

        private void IncreaseRepeatButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            flyoutDePrediction.IsOpen = false;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SliderTemp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void ButtonLireDunFichier_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
        }

        private void SwitchTemp_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderTemp.IsEnabled = false;
            
        }

        private void switchVent_Checked(object sender, RoutedEventArgs e)
        {
            sliderVent.IsEnabled = true;
        }

        private void SwitchVent_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderVent.IsEnabled = false;
        }

        private void switchPrecip_Checked(object sender, RoutedEventArgs e)
        {
            sliderPrecip.IsEnabled = true;
        }

        private void SwitchPrecip_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderPrecip.IsEnabled = false;
        }

        private void switchNuage_Checked(object sender, RoutedEventArgs e)
        {
            sliderNuage.IsEnabled = true;
        }

        private void SwitchNuage_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderNuage.IsEnabled = false;
        }

        private void switchHumid_Checked(object sender, RoutedEventArgs e)
        {
            sliderHumid.IsEnabled = true;
        }

        private void SwitchHumid_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderHumid.IsEnabled = false;
        }
    }
}
