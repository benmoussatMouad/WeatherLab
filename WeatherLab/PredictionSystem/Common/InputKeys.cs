using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common
{
    /// <summary>
    /// Contains all possible Keys for inputs in the application 
    /// Add more if needed 
    /// </summary>
    static class InputKeys
    {
        public static string NONE = "NONE";

        /// Date and time related Keys 

        public static string DATE = "DATE";
        public static string DURATION = "DURATION";
        public static string WILAYA = "WILAYA";

        /// Parameters related Keys 
        public static string TEMPERATURE = "TEMP";
        public static string WIND_SPEED = "WINDS";
        public static string WIND_DIRECTION = "WINDD";
        public static string HUMIDITY = "HUMIDITY";
        public static string PLUVIOMETRIE = "PLUVIOMETRIE";
        public static string GROUND_STATE = "GROUNDSTATE";
        
    }
}
