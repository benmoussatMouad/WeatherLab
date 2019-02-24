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
        public static readonly string NONE = "NONE";

        /// Date and time related Keys 

        public static readonly string DATE = "DATE";
        public static readonly string DURATION = "DURATION";
        public static readonly string WILAYA = "WILAYA";

        /// Parameters related Keys 
        public static readonly string TEMPERATURE = "TEMP";
        public static readonly string WIND_SPEED = "WINDS";
        public static readonly string WIND_DIRECTION = "WINDD";
        public static readonly string HUMIDITY = "HUMIDITY";
        public static readonly string PLUVIOMETRIE = "PLUVIOMETRIE";
        public static readonly string GROUND_STATE = "GROUNDSTATE";
        
    }
}
