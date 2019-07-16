using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Input;
using WeatherLab.PredictionSystem.Common;
using WeatherLab.Synthése;
using WeatherLab.Data;
using System.Linq;
using System.Windows.Media.Animation;
using WeatherLab.ConfigUtils;
using System.Threading;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common.WilayaClasses;
using System.IO;
using WeatherLab.Synthèse;
using System.Security.Cryptography;

namespace WeatherLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public int btnSelec;
        public bool isLoggedIn = false;
        private Authentification authentifiction;
        private ChangePassword changePassword;
        private string passwords;
        //MapPage mapPage = new MapPage();

        public object NavigationService { get; private set; }

        public MainWindow()
        {

            InitializeComponent();
            this.Icon = (ImageSource)new ImageSourceConverter().ConvertFrom(@"assets\\icons\\icon.ico");
            this.Loaded += MainWindow_Loaded;
            passwords = "weatherlab";
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnSelec = 3;
            main.Content = new MapPage();
            btn3.Background = (Brush)App.Current.Resources["DarkBlue"];
            btn3.Content = App.Current.Resources["WhiteMap"];
        }

        /// <summary>
        /// Deselectionne une bouton selon son numero qui est btnselect
        /// </summary>
        /// <param name="btnselect"></param>
        public void Deselect_Btn(int btnselect)
        {
            if (main.Content != null && main.Content.GetType().Equals(typeof(MapPage)))
            {
                (main.Content as MapPage).Prediction_Synthese.Close();
            }
            switch (btnselect)
            {
                case 1:
                    btn1.Background = Brushes.White;
                    //btn1.Content = App.Current.Resources["BlackCloud"];
                    cloudImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"assets\icons\cloud.png");
                    break;
                case 2:
                    btn2.Background = Brushes.White;
                    btn2.Content = App.Current.Resources["BlackBarChart"];
                    break;
                case 3:
                    btn3.Background = Brushes.White;
                    btn3.Content = App.Current.Resources["BlackMap"];
                    break;
                case 4:
                    btn4.Background = Brushes.White;
                    btn4.Content = App.Current.Resources["BlackFileText"];
                    break;
                case 5:
                    btn5.Background = Brushes.White;
                    btn5.Content = App.Current.Resources["BlackLock"];
                    break;
                default:
                    break;
            }
        }
        private string getPassHash(string path_file)
        {
            string[] lines = File.ReadAllLines(path_file);
            return lines[0];
        }

        /// <summary>
        /// l'evenement de click du bouton d'authentification ou bien changer le mot de passe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            
            if (!isLoggedIn)
            {
                string hashedPass;
                try
                {
                    hashedPass = getPassHash(ChangePassword.password_path_file);
                }
                catch (Exception ){
                    hashedPass = ChangePassword.hashPassword("weatherlab", new SHA256CryptoServiceProvider());
                }
                 
                fermerFlyout();
                btn5.Background = (Brush)App.Current.Resources["DarkBlue"];
                btn5.Content = App.Current.Resources["WhiteLock"];
                Deselect_Btn(btnSelec);
                btnSelec = 0;
                authentifiction = new Authentification();
                authentifiction.VerticalAlignment = VerticalAlignment.Center;
                authentifiction.HorizontalAlignment = HorizontalAlignment.Center;
                authentifiction.ShowDialog();
                Boolean cond = (hashedPass == null);
                boucle: while (authentifiction.Visibility == Visibility.Visible && !authentifiction.isClosed)
                {
                }
                if (authentifiction.isClosed)
                {
                    btn5.Background = Brushes.White;
                    btn5.Content = App.Current.Resources["BlackLock"];
                }
                else if (hashedPass.Equals(ChangePassword.hashPassword(authentifiction.password, new SHA256CryptoServiceProvider())))
                {
                    authentifiction.Close();
                    authentifiction.isClosed = true;
                    isLoggedIn = true;
                    (new MessageBoxWindow("Mot de passe correct", true)).ShowDialog();
                    btn5.Background = (Brush)App.Current.Resources["DarkBlue"];
                    btn5.Content = App.Current.Resources["WhiteLock"];
                    //btn5.BorderBrush = (Brush)App.Current.Resources["LockedBack"];
                    btn4.Content = App.Current.Resources["BlackFileText"];
                    btn4.Background = Brushes.White;
                    Deselect_Btn(5);
                }
                else
                {
                    (new MessageBoxWindow("Mot de passe incorrect", false)).ShowDialog();
                    authentifiction.Close();
                    authentifiction = new Authentification();
                    authentifiction.ShowDialog();
                    goto boucle;
                }


            } else
            {
                fermerFlyout();
                btn5.Background = (Brush)App.Current.Resources["DarkBlue"];
                btn5.Content = App.Current.Resources["WhiteLock"];
                Deselect_Btn(btnSelec);
                btnSelec = 0;
                changePassword = new ChangePassword();
                changePassword.VerticalAlignment = VerticalAlignment.Center;
                changePassword.HorizontalAlignment = HorizontalAlignment.Center;
                changePassword.ShowDialog();

                boucle2: while (changePassword.IsActive)
                {
                }
                Deselect_Btn(5);
            }
        }

        /// <summary>
        /// L'evenement de click du bouton du dataset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            if (btnSelec != 4 && isLoggedIn)
            {
                fermerFlyout();
                btn4.Background = (Brush)App.Current.Resources["DarkBlue"];
                btn4.Content = App.Current.Resources["WhiteFileText"];
                Deselect_Btn(btnSelec);
                btnSelec = 4;
                DataSet ds = new DataSet();
                ds.Show();
            }
        }

        /// <summary>
        /// L'evenement de click du bouton de la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            if (!(btnSelec == 3))
            {
                fermerFlyout();
                main.Content = new MapPage();
                btn3.Background = (Brush)App.Current.Resources["DarkBlue"];
                btn3.Content = App.Current.Resources["WhiteMap"];
                Deselect_Btn(btnSelec);
                btnSelec = 3;
            }
        }

        /// <summary>
        /// L'evenement de click du bouton de la synthese
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Btn2_Click(object sender, RoutedEventArgs e)
        {
            if (!(btnSelec == 2))
            {
                fermerFlyout();
                btn2.Background = (Brush)App.Current.Resources["DarkBlue"];
                btn2.Content = App.Current.Resources["WhiteBarChart"];
                Deselect_Btn(btnSelec);
                btnSelec = 2;
                main.Content = new Synthese();
                (new ChangeWilaya()).Show();
            }
        }

        /// <summary>
        /// L'evenement de click du bouton de la prediction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (!(btnSelec == 1))
            {
                ouvrirFlyout();
                btn1.Background = (Brush)App.Current.Resources["DarkBlue"];
                cloudImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"assets\icons\white_cloud.png");
                Deselect_Btn(btnSelec);
                btnSelec = 1;
            }
            else
            {
                fermerFlyout();
                Deselect_Btn(btnSelec);
                btnSelec = 0;
            }
        }




        private void Main_Navigated(object sender, NavigationEventArgs e)
        {
            while (main.NavigationService.RemoveBackEntry() != null) ;
        }

        /// <summary>
        /// L'evenement de flotter au dessus de tout les boutons de la barre 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!(((sender as Button).Equals(btn4) & !isLoggedIn) || ((sender as Button).Equals(btn5) & isLoggedIn)) && (btnSelec != 1 || !(sender as Button).Equals(btn1)))
            {
                ContainerVisual child = new ContainerVisual();
                ScaleTransform scale = new ScaleTransform();
                child = VisualTreeHelper.GetChild(vw, 0) as ContainerVisual;
                scale = child.Transform as ScaleTransform;
                popup1Template.Height = scale.ScaleX * (sender as Button).ActualWidth;
                popup1Template.CornerRadius = new CornerRadius(popup1Template.Height / 2);
                popup1holl.Center = new Point(22.5, 22.5);
                popup1holl.RadiusX = popup1Template.Height / 2;
                popup1holl.RadiusY = popup1Template.Height / 2;
                popup1.PlacementTarget = sender as Button;
                switch ((sender as Button).Name)
                {
                    case "btn1":
                        popup1Content.Content = "Prévoire";
                        break;
                    case "btn2":
                        popup1Content.Content = "Synthèse";
                        break;
                    case "btn3":
                        popup1Content.Content = "Carte";
                        break;
                    case "btn4":
                        popup1Content.Content = "Dataset";
                        break;
                    case "btn5":
                        popup1Content.Content = "Admin";
                        break;
                    default:
                        break;
                }
                popup1.IsOpen = true;
            }
        }

        /// <summary>
        /// L'evenement de l'arret du flottement au dessus de tout les boutons du dataset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            popup1.IsOpen = false;
        }

        private void IncreaseRepeatButton_Click(object sender, RoutedEventArgs e)
        {

        }

       
        private void buttonValider_Click(object sender, RoutedEventArgs e)
        {
            if (getWilaya() == "")
            {
                (new MessageBoxWindow("Veuillez d'abord selectioner une wilaya", false)).ShowDialog();
                return;
            }
            if (!(sliderHumid.IsEnabled || sliderNuage.IsEnabled
              || sliderPrecip.IsEnabled || sliderTemp.IsEnabled
              || sliderVent.IsEnabled))
            {
                (new MessageBoxWindow("Veuillez selectioner au moins un attribut", false)).ShowDialog();
                return;
            }
            List<Input> inputs = getInputs();
            PredictionPage p = new PredictionPage();
            ConfigParser config = App.Config;
            config.import("config.json");
            try
            {
                p.SetInputs(inputs);
                p.SetConfig(config);
                p.StartPrediction(0.1);
                p.ShowPrediction();
                main.Content = p;
                fermerFlyout();
                Canvas.SetZIndex(transparent, 0);
                btn1.Background = (Brush)App.Current.Resources["DarkBlue"];
                Deselect_Btn(btnSelec);
                btnSelec = 1;
            }
            catch (FileNotFoundException error)
            {
                MessageBoxWindow mb = new MessageBoxWindow("Le chemin de la wilaya " + getWilaya() + "\n n'existe pas ", false);
                mb.messageErreur.Visibility = Visibility.Hidden;
                mb.casSpecial.Content = "Ajouter Dataset";
                mb.casSpecial.Visibility = Visibility.Visible;
                mb.casSpecial.Click += (object Sender, RoutedEventArgs a) =>
                {
                    MainWindow mw = App.Current.MainWindow as MainWindow;
                    if (mw.isLoggedIn)
                    {
                        DataSet d = new DataSet();
                        d.ComboWilaya.Text = getWilaya();
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
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void buttonLireDunFichier_Click(object sender, RoutedEventArgs e)
        {

        }
        private void switchTemp_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderTemp.IsEnabled = false;
            textblockTemp.Foreground = (Brush)Application.Current.Resources["Disabled"];
            path1.Fill = (Brush)Application.Current.Resources["Disabled"];
            Temp.Foreground = (Brush)Application.Current.Resources["Disabled"];
        }

        private void switchTemp_Checked(object sender, RoutedEventArgs e)
        {
            sliderTemp.IsEnabled = true;
            textblockTemp.Foreground = new SolidColorBrush(Colors.White);
            path1.Fill = new SolidColorBrush(Colors.White);
            Temp.Foreground= new SolidColorBrush(Colors.White);
        }

        
        private void switchVent_Checked(object sender, RoutedEventArgs e)
        {
            sliderVent.IsEnabled = true;
            textblockVent.Foreground = new SolidColorBrush(Colors.White);
            path2.Fill = new SolidColorBrush(Colors.White);
            Vent.Foreground = new SolidColorBrush(Colors.White);

        }

        private void switchVent_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderVent.IsEnabled = false;
            textblockVent.Foreground= (Brush)Application.Current.Resources["Disabled"];
            path2.Fill = (Brush)Application.Current.Resources["Disabled"];
            Vent.Foreground = (Brush)Application.Current.Resources["Disabled"];
        }

        private void switchPrecip_Checked(object sender, RoutedEventArgs e)
        {
            sliderPrecip.IsEnabled = true;
            textblockPrecip.Foreground = new SolidColorBrush(Colors.White);
            path3.Fill = new SolidColorBrush(Colors.White);
            Precip.Foreground = new SolidColorBrush(Colors.White);
        }

        private void switchPrecip_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderPrecip.IsEnabled = false;
            textblockPrecip.Foreground = (Brush)Application.Current.Resources["Disabled"];
            path3.Fill = (Brush)Application.Current.Resources["Disabled"];
            Precip.Foreground = (Brush)Application.Current.Resources["Disabled"];
        }

        private void switchNuage_Checked(object sender, RoutedEventArgs e)
        {
            sliderNuage.IsEnabled = true;
            textblockNuage.Foreground = new SolidColorBrush(Colors.White);
            path4.Fill = new SolidColorBrush(Colors.White);
            Nuage.Foreground = new SolidColorBrush(Colors.White);
        }

        private void switchNuage_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderNuage.IsEnabled = false;
            textblockNuage.Foreground = (Brush)Application.Current.Resources["Disabled"];
            path4.Fill = (Brush)Application.Current.Resources["Disabled"];
            Nuage.Foreground = (Brush)Application.Current.Resources["Disabled"];
        }

        private void switchHumid_Checked(object sender, RoutedEventArgs e)
        {
            sliderHumid.IsEnabled = true;
            textblockHumid.Foreground = new SolidColorBrush(Colors.White);
            goutte1.Stroke = new SolidColorBrush(Colors.White);
            goutte2.Fill = new SolidColorBrush(Colors.White);
            Humid.Foreground = new SolidColorBrush(Colors.White);
        }

        private void switchHumid_Unchecked(object sender, RoutedEventArgs e)
        {
            sliderHumid.IsEnabled = false;
            textblockHumid.Foreground = (Brush)Application.Current.Resources["Disabled"];
            goutte1.Stroke = (Brush)Application.Current.Resources["Disabled"];
            goutte2.Fill = (Brush)Application.Current.Resources["Disabled"];
            Humid.Foreground = (Brush)Application.Current.Resources["Disabled"];
        }
        private void SliderDelai_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                textblockDelai.Text = System.Math.Truncate(sliderDelai.Value).ToString() + " j";
            }
            catch (Exception)
            {
                return;
            }
        }
        private void SliderTemp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textblockTemp.Text = System.Math.Round(sliderTemp.Value, 1).ToString() + " °C";
        }

        private void SliderVent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textblockVent.Text = System.Math.Round(sliderVent.Value, 1).ToString() + " m/s";
        }

        private void SliderPrecip_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textblockPrecip.Text = System.Math.Round(sliderPrecip.Value, 1).ToString() + " mm";
        }

        private void SliderNuage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textblockNuage.Text = System.Math.Round(sliderNuage.Value, 1).ToString() + " %";
        }

        private void SliderHumid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textblockHumid.Text = System.Math.Round(sliderHumid.Value, 1).ToString() + " %";
        }

        
        // getters
        public String getWilaya()
        {
            if (!(string.IsNullOrEmpty(comboWilaya.Text)))
            {
                return comboWilaya.Text;
            }
            else
            {
                return "";
            }
        }

        double getTemp()
        {
            return sliderTemp.Value;
        }

        double getVent()
        {
            return sliderVent.Value;
        }

        double getPrecip()
        {
            return sliderPrecip.Value;
        }

        double getNuage()
        {
            return sliderNuage.Value;
        }

        double getHumid()
        {
            return sliderHumid.Value;
        }

        int getDelai()
        {
            return (int)Math.Truncate(sliderDelai.Value);
        }

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
            this.comboWilaya.ItemsSource = Liste;
        }

        private class Wilaya
        {
            public string wilaya { get; set; }

            public Wilaya(string wilaya)
            {
                this.wilaya = wilaya;
            }

        }

        List<Input> getInputs()
        {
            List<Input> l = new List<Input>
            {
                new PredictionSystem.Common.Wilaya(getWilaya()),
                new PredictionSystem.Common.Duration(getDelai()),
            };
            if (sliderTemp.IsEnabled) l.Add(new Parameter(InputKeys.TEMPERATURE, getTemp()));
            if (sliderVent.IsEnabled) l.Add(new Parameter(InputKeys.WIND_SPEED, getVent()));
            if (sliderPrecip.IsEnabled) l.Add(new Parameter(InputKeys.PLUVIOMETRIE, getPrecip()));
            if (sliderNuage.IsEnabled) l.Add(new Parameter(InputKeys.NUAGES, getNuage()));
            if (sliderHumid.IsEnabled) l.Add(new Parameter(InputKeys.HUMIDITY, getHumid()));
            return l;
        }

           
        

        public void fermerFlyout()
        {
            Canvas.SetZIndex(transparent, 0);
            DoubleAnimation flyoutAnimationOut = new DoubleAnimation();
            flyoutAnimationOut.From = flyoutTranslate.X;
            flyoutAnimationOut.To = -((myGrid.RenderSize.Width) + (BarGrid.RenderSize.Width));
            flyoutAnimationOut.Duration = TimeSpan.FromSeconds(.1);
            flyoutTranslate.BeginAnimation(TranslateTransform.XProperty, flyoutAnimationOut, HandoffBehavior.SnapshotAndReplace);
        }
        
        public void ouvrirFlyout()
        {
            Canvas.SetZIndex(transparent, 2);
            myGrid.Visibility = Visibility.Visible;
            DoubleAnimation flyoutAnimationIn = new DoubleAnimation();
            flyoutAnimationIn.From = -(myGrid.RenderSize.Width);
            flyoutAnimationIn.To = 0;
            flyoutAnimationIn.Duration = TimeSpan.FromSeconds(0.1);
            flyoutTranslate.BeginAnimation(TranslateTransform.XProperty, flyoutAnimationIn, HandoffBehavior.SnapshotAndReplace);
        }

        private void ButtonEnrichir_Click(object sender, RoutedEventArgs e)
        {

        }

    }

}