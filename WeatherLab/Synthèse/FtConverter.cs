using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherLab.Synthèse
{
    public class FtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float val = float.Parse(value.ToString());

            return val / 4;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
