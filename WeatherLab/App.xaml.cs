using System;
using System.Windows;
using WeatherLab.ConfigUtils;

namespace WeatherLab
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private static ConfigParser config = new ConfigParser();

        public static ConfigParser Config
        {
            get { return config; }
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception exc = e.Exception;
            MessageBox.Show(exc.Message+"\n"+exc.StackTrace);
        }
    }
}