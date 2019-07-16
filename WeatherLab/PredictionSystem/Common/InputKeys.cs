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
        public static readonly string TEMPERATURE = "Température";
        public static readonly string WIND_SPEED = "Vitesse du vent";
        public static readonly string NUAGES = "Nuage %";
        public static readonly string WIND_DIRECTION = "Durection du vent";
        public static readonly string HUMIDITY = "Humidité";
        public static readonly string PLUVIOMETRIE = "Précipitation";
        public static readonly string GROUND_STATE = "Etat du sol";
        public static readonly string AIR_PRESSURE = "Pression";

    }
}
