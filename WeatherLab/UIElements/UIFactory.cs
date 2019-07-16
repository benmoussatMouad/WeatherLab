using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using WeatherLab.UIElements.common;
using WeatherLab.PredictionSystem.Common;
using System.IO;
using Svg2Xaml;
using System.Windows.Media;

namespace WeatherLab.UIElements
{
    static class UIFactory
    {
        private static PredictionSystem.PredictionSystem predictionSystem = PredictionSystem.PredictionSystem.Instance;
        /// <summary>
        /// This class has some utility functions that build some UI Elements of the prédiction page
        /// </summary>

        public static DropShadowEffect shadowEffect = new DropShadowEffect()
        {
            Opacity = 20,
            BlurRadius = 7,
            Direction = 300,
            ShadowDepth = 3
        };
        public static int textSize = 16;
        public static WrapPanel CreatePredictionWrapPanel(Image img, TextBlock text, Orientation orientation)
        {
            WrapPanel panel = new WrapPanel()
            {
                Margin = new System.Windows.Thickness(30),
                Orientation = orientation,
            };
            panel.Children.Add(img);
            panel.Children.Add(text);
            return panel;
        }
        public static Image CreatePredictionIcon(String relativePath)
        {
            Image img1 = new Image
            {
                Source = SvgIcon(relativePath)
                ,
                
                Width = 40,
                Height = 40,
               
                Margin = new System.Windows.Thickness(0,5,0,5),
                Effect = shadowEffect,
                
            };
            return img1;
        }
        public static TextBlock CreatePredictionText(string text)
        {

            TextBlock text1 = new TextBlock
            {
                Text = text,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                FontSize = 20,
                Effect = shadowEffect

            };
            return text1;
        }
        public static WrapPanel GeneratePredictionWrapPanel(int mode ,String iconPath, String parameterKey, String Value, String error, String errorRatio)
        {

            WrapPanel panel = new WrapPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new System.Windows.Thickness(6),
                
            };
            Image icon = new Image()
            {
                Source = SvgIcon(iconPath),
                Width = 40,
                Height = 40,
                Margin = new System.Windows.Thickness(10),
                ToolTip = StringFormater.GetParameterFromKey(parameterKey),
                Effect = shadowEffect,
            };
            panel.Children.Add(icon);

            TextBlock value = new TextBlock()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new System.Windows.Thickness(2),
                Text = Value,
                FontSize = textSize,
                Effect = shadowEffect
                
            };
            panel.Children.Add(value);
            if (mode == PredictionSystem.PredictionSystem.CEP)
            {
                TextBlock err = new TextBlock()
                {

                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Margin = new System.Windows.Thickness(2),
                    Text = " \u00B1" + error,
                    ToolTip = "Varie de : " + errorRatio,
                    FontSize = textSize,
                    Effect = shadowEffect


                };

                panel.Children.Add(err);
            }
           
            else
            {
                value.Margin = new System.Windows.Thickness(10,0,10,0);
            }
            
            
            
            return panel;
        }
        public static TextBlock Probabilityblock(Result result,int mode)
        {
            String tooltip = "degré de confiance sur la prédiction" ;
            
            return new TextBlock()
            {
                Text = StringFormater.GetProbabiltyText(result),

                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Margin = new System.Windows.Thickness(0, 0, 0, 0),
                FontSize = 18,
                ToolTip = tooltip,
                Effect = shadowEffect



            };
        }
        public static Image PredictionOutlookImage(Result result)
        {
            return new Image()
            {
                Source = SvgIcon(ImagePaths.GetOutlookImagePath(result)),

                MaxHeight = 50,
                MaxWidth = 50,
                
                Margin = new System.Windows.Thickness(20, 0, 20, 0)
                   ,
                ToolTip = StringFormater.GetClimate(result),
                Effect = shadowEffect
            };
        }
        public static StackPanel ParametersPanel()
        {
            return new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new System.Windows.Thickness(20, 0, 20, 0),
                Width = double.NaN,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
            };
        }
        public static StackPanel Prediction(Result item,int mode)
        {
            /// a container for the parameters that stacks them horizontaly 
            StackPanel parametres = UIFactory.ParametersPanel();
            foreach (var param in item.Predictions)
            {
                ///TODO :  change this to use resources 
                parametres.Children.Add(UIFactory.GeneratePredictionWrapPanel(mode,ImagePaths.GetImagePathForParameter(param), param.ParamKey, StringFormater.GetParameterValue(param), (Math.Round(param.StandardDeviation, 2)).ToString("00"), (Math.Abs(Math.Round(param.StandardDeviation, 2))).ToString()));
            }
            /// a container for the image of the outlook
            StackPanel outlookPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,

            };
            /// TODO : change this to use resources 
            Image outlook = UIFactory.PredictionOutlookImage(item);

            ///------------------
            outlookPanel.Children.Add(outlook);

            /// This is the probability text 
            TextBlock probability = UIFactory.Probabilityblock(item,mode);

            /// a container for the whole prediction element  
            StackPanel prediction = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Width = double.NaN
                ,
                Margin = new System.Windows.Thickness(0, 2, 0, 2)
            };
            /// creating the prediction
            prediction.Children.Add(outlookPanel);
            prediction.Children.Add(parametres);
            prediction.Children.Add(probability);
            return prediction;
        }
        public static DrawingImage SvgIcon(String path)
        {
            string filePath = System.IO.Path.GetFullPath(path);
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {


                DrawingImage image = SvgReader.Load(stream);
                return image;

            }
        }
    }
}
