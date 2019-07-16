using WeatherLab.PredictionSystem;
using WeatherLab.PredictionSystem.Common;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;
using WeatherLab.UIElements;
using WeatherLab.UIElements.common;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Threading;
using System.Windows;
using System.IO;
using WeatherLab.PredictionSystem.Utils;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace WeatherLab
{
    /// <summary>
    /// Interaction logic for PredictionPage.xaml
    /// </summary>
    public partial class PredictionPage : Page
    {
        private PredictionSystem.PredictionSystem predictionSystem = PredictionSystem.PredictionSystem.Instance;
        
        private static double ANIMATION_STARTING_SECONDS = 1.0;
        private readonly string INDISPONIBLE = "Indisponible";
        private readonly string DEFAULT_DAYS_TEXT = "Dans ? Jours";
        private readonly string DEFAULT_WILAYA_TEXT = "Wilaya ?";
        private readonly string DEFEULT_TOOLTIP = "Clicker sur le button de prédiction";
        private readonly String DEFAULT_OUTLOOK_IMAGE = ImagePaths.NA;
        private readonly DoubleAnimation animation;
        private double animationTime = ANIMATION_STARTING_SECONDS;
        private Boolean predictionComplete = false;
        private Image backgroundImage;

        /// Ints and floats :
        private double predictionPrecision = 0.1;
        private int numberOfPredictionsToShow = 1;
        private int lastElementShown = 0;    
        /// 

        public PredictionPage()
        {
            InitializeComponent();
            /// initializing page elements 
            animation = new DoubleAnimation(0, 1, new System.Windows.Duration(TimeSpan.FromSeconds(ANIMATION_STARTING_SECONDS)));
            InitPredictionPage();

        }


        /// <summary>
        /// Generates the first label text based on requested prediction duration
        /// </summary>
        /// <returns>string text</returns>
        private string GenerateDaysText()
        {

            return "Dans " + predictionSystem.GetDurationInDays() + " Jours";

        }
        /// <summary>
        /// Generates the label of the requested wilaya of prediction
        /// </summary>
        /// <returns></returns>
        private string GenerateWilayaText()
        {

            return predictionSystem.GetRequestedWilaya().ToUpper();

        }

        /// <summary>
        /// this method generates the most probable Prediction's UI elements like the Outlook Image and the first parameter predictions 
        /// </summary>
        private void GenerateFirstPrediction()
        {

            GenerateFirstOutcastImage();

            GenerateFirstPredictionParameters();


        }


        /// <summary>
        /// this button is used to return from this page to map page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DisposeUI();
            ///TODO : Add return from page logic here :

            ///
            startButton.IsEnabled = true;
            morePrediction.IsEnabled = false;
            clear.IsEnabled = false;
            noPredictionPanel.Visibility = System.Windows.Visibility.Visible;

            InitPredictionPage();


        }
        /// <summary>
        /// This method generate the most probable prediction Image with appropriate width and height and effect
        /// </summary>
        private void GenerateFirstOutcastImage()
        {
            Result result = predictionSystem.Result.Results[0];
            ///TODO : Change this to use Resources :
            //Uri imagePath = new Uri(@"" + ImagePaths.GetOutlookImagePath(result), UriKind.Absolute);
            outcastImage.Source = UIFactory.SvgIcon(ImagePaths.GetOutlookImagePath(result));
            outcastImage.VerticalAlignment = VerticalAlignment.Stretch;outcastImage.HorizontalAlignment = HorizontalAlignment.Stretch;
            
            ///------------------------------
            outcastImage.ToolTip = result.Climate;
            
            animation.Duration = new System.Windows.Duration(TimeSpan.FromSeconds(animationTime));
            outcastImage.BeginAnimation(OpacityProperty, animation);
            outcastImage.BeginAnimation(OpacityProperty, animation);
        }
        /// <summary>
        /// Generates the most probable prédiction first parameters 
        /// </summary>
        private void GenerateFirstPredictionParameters()
        {
            /// Determining the number of parameters to draw 
            int nbParam = (int)Math.Round(predictionSystem.GetNbParameters() * 0.5) + 1;

            int i = 0;
            foreach (var item in predictionSystem.Result.Results[0].Predictions)
            {
                /// TODO : Change this to use resources 
                Image img = UIFactory.CreatePredictionIcon(ImagePaths.GetImagePathForParameter(item));
                ///
                TextBlock text = UIFactory.CreatePredictionText(StringFormater.GetParameterValue(item));
                WrapPanel panel = UIFactory.CreatePredictionWrapPanel(img, text, Orientation.Vertical);
                firstPrediction.Children.Add(panel);
                panel.BeginAnimation(OpacityProperty, animation);
                i++;
                if (i >= nbParam)
                {
                    break;
                }
            }

        }
        /// <summary>
        /// This method builds the prédictions table dynamicly based on the results taken from prédictionSystem
        /// Has TODO task 
        /// </summary>
        private async void BuildPredictions()
        {
            /// setting the background of the page based on the weather
            
            SetOutlookBackground();
            
            int i = 0;
            animation.Duration = new System.Windows.Duration(TimeSpan.FromSeconds(animationTime));
            await Task.Delay(2000);
            foreach (var item in predictionSystem.Result.Results)
            {
               
                //Separator separator = new Separator();

                /// creating the prediction
                StackPanel prediction = UIFactory.Prediction(item,predictionSystem.GetMode());
                /// Adding to the UI Element that is displayed 
                //panelExpanders.Children.Add(separator);
                panelExpanders.Children.Add(prediction);
                /// animating 
                //animation.Duration = new System.Windows.Duration(TimeSpan.FromSeconds(animationTime));
                // separator.BeginAnimation(OpacityProperty, animation);
                
                await animate(prediction, 2);
                i++;
                if (i > 2)
                {
                    
                    break;
                }
                      
            }
            lastElementShown = i - 1;
            clear.IsEnabled = true;

        }

        /// <summary>
        /// Used to cleanup the page from objects after finishing 
        /// </summary>
        private void DisposeUI()
        {
            firstPrediction.Children.Clear();
            panelExpanders.Children.Clear();
            outcastImage.Source = null;
            wilayaLabel.Text = null;
            daysLabel.Text = null;

        }

        /// <summary>
        /// Performs the prédiction based on Inputs taken from the prédiction bar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ShowPrediction();

        }
        public async void ShowPrediction()
        {
            /// initializing the prédiction page with data
            /// Implement : Open Prediction Bar and wait for input 
            this.daysLabel.Text = GenerateDaysText() + " : " + GetDateOfPrediction();
            daysLabel.BeginAnimation(OpacityProperty, animation);
            this.wilayaLabel.Text = GenerateWilayaText();

            
            animation.Duration = new System.Windows.Duration(TimeSpan.FromSeconds(animationTime));

            wilayaLabel.BeginAnimation(OpacityProperty, animation);
            

            predictionComplete = true;
            //noPredictionPanel.Visibility = System.Windows.Visibility.Hidden;
            Random rand = new Random();
            int sec = rand.Next(2,5);
            await Task.Delay(sec*1000);
           
            noPredictionPanel.Visibility = System.Windows.Visibility.Hidden;
            GenerateFirstPrediction();
            if (predictionComplete)
            {
                BuildPredictions();
                saveButton.IsEnabled = true;
                morePrediction.IsEnabled = true;
            }

            startButton.IsEnabled = false;
        }
        private async void MoreButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            int count = predictionSystem.Result.Results.Count;
            if (lastElementShown <count)
            {
                //Separator sep = new Separator();
                StackPanel prediction = UIFactory.Prediction(predictionSystem.Result.Results[lastElementShown + 1],predictionSystem.GetMode());

                lastElementShown = lastElementShown + 1;
                //panelExpanders.Children.Add(sep);
                panelExpanders.Children.Add(prediction);
                await animate(prediction, 2);
                //sep.BeginAnimation(OpacityProperty, animation);
                //prediction.BeginAnimation(OpacityProperty, animation);
                if(lastElementShown == count - 1)
                {
                    morePrediction.IsEnabled = false;
                    
                }
                clear.IsEnabled = true;
            }
          
        }
        private void ClearButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            panelExpanders.Children.Clear();
            lastElementShown = -1;
            clear.IsEnabled = false;
            morePrediction.IsEnabled = true;

        }

        /// <summary>
        /// Sets the background of the prédiction page based on the prédicted outlook
        /// </summary>
        private void SetBackground(string path)
        {
            /// TODO : change this to use resources 
            Uri uri = new Uri(path, UriKind.Absolute);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = uri;
            image.EndInit();
            backgroundImage = new Image()
            {
                Source = image,
                Effect = new BlurEffect()
                {
                    Radius = 15,
                    KernelType = KernelType.Gaussian
                }
            };

            VisualBrush brush = new VisualBrush
            {
                Visual = backgroundImage,
                Viewbox = new System.Windows.Rect(0.05, 0.05, 0.9, 0.9) /// fixing black border

            };


            this.Background = brush;


        }

        private void InitPredictionPage()
        {
            outcastImage.Source =UIFactory.SvgIcon(DEFAULT_OUTLOOK_IMAGE);
            outcastImage.ToolTip = INDISPONIBLE;
            wilayaLabel.Text = DEFAULT_WILAYA_TEXT;
            daysLabel.Text = DEFAULT_DAYS_TEXT;
            noPredictionPanel.ToolTip = DEFEULT_TOOLTIP;
            animationTime = ANIMATION_STARTING_SECONDS;

            animation.Duration = new System.Windows.Duration(TimeSpan.FromSeconds(animationTime));
            SetBackground(@"" + ImagePaths.DEFAULT_BACKGROUND);
            predictionComplete = false;
            saveButton.IsEnabled = false;
            morePrediction.IsEnabled = false;
        }
        /// <summary>
        /// Sets the outlook background based on most probable predicted outlook
        /// </summary>
        private void SetOutlookBackground()
        {
            if (predictionSystem.Result.Results.Count != 0)
            {
                Result result = predictionSystem.Result.Results[0];
                /// TODO : change this to use resources 
                if (result.Climate.Equals(DecisionMaker.SUNNY))
                {
                    SetBackground(@"" + ImagePaths.SUNNY_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.SUNNY_SMALL_CLOUDS))
                {
                    SetBackground(@"" + ImagePaths.CLOUDY_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.HEAVY_RAIN))
                {
                    SetBackground(@"" + ImagePaths.HEAVYRAIN_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.LOW_RAIN))
                {
                    SetBackground(@"" + ImagePaths.LOWRAIN_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.CLOUDS_ONLY))
                {
                    SetBackground(@"" + ImagePaths.CLOUDS_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.NO_RAIN))
                {
                    SetBackground(@"" + ImagePaths.NORAIN_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.SNOWY_HIGH))
                {
                    SetBackground(@"" + ImagePaths.SNOWY_HIGH_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.SNOWY_LOW))
                {
                    SetBackground(@"" + ImagePaths.SNOWY_LOW_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.WIND_HIGH))
                {
                    SetBackground(@"" + ImagePaths.WINDY_HIGH_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.WIND_LOW))
                {
                    SetBackground(@"" + ImagePaths.WINDY_LOW_BACKGROUND);
                }
                else if (result.Climate.Equals(DecisionMaker.CLIMATE_NA))
                {
                    SetBackground(@"" + ImagePaths.DEFAULT_BACKGROUND);
                }
            }
            else
            {
                SetBackground(@"" + ImagePaths.DEFAULT_BACKGROUND);
            }

        }
        private String GetDateOfPrediction()
        {
            var culture = new System.Globalization.CultureInfo("fr-FR"); 
            DateTime date = predictionSystem.DailyMeteoSystem.Observation.Date;
            date = date.AddDays(predictionSystem.GetDurationInDays());
            var day = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
            return day+" "+date.ToLocalTime().ToShortDateString();
        }
        public void SetInputs(List<Input> inputs)
        {
            predictionSystem.SetInputs(inputs);
        }
        public void StartPrediction(double pre=0.1)
        {
            noPredictionPanel.Visibility = System.Windows.Visibility.Visible;
            progress.Visibility = System.Windows.Visibility.Visible; 
            predictionSystem.SetPredictionPrecision(pre);
            predictionSystem.StartPrediction();
            predictionPrecision =  pre;
            numberOfPredictionsToShow = (int ) Math.Floor(1/pre);

            
        }
         public void SetMode(int mode )
        {
            predictionSystem.SetMode(mode);
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            String header = SaveManager.getCsvHeader(predictionSystem.GetQuery());
            Console.WriteLine(header);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "Coma Separated Values (.csv)|*.csv";
            List<String> contents = new List<string>();
            DateTime today = predictionSystem.DailyMeteoSystem.Observation.Date;
            DateTime date = today.AddDays(predictionSystem.GetDurationInDays());
            string wilaya = predictionSystem.GetRequestedWilaya();
            contents.Add(header);
            foreach (Result r in predictionSystem.Result.Results)
            {
                contents.Add(SaveManager.getResultAsCsv(today,date,wilaya,r));
            }
            if (saveFileDialog.ShowDialog() == true)
            {
               
                File.AppendAllLines(saveFileDialog.FileName, contents);
            }
                
        }
        public void SetConfig(ConfigUtils.ConfigParser parser)
        {
            this.predictionSystem.SetConfiguration(parser);
        }
      
        private async Task animate(UIElement element , int sec)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1, new System.Windows.Duration(TimeSpan.FromSeconds(ANIMATION_STARTING_SECONDS)));
            element.BeginAnimation(OpacityProperty, animation);
            await Task.Delay(sec * 1000);
        }
    }
}
