using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
using WeatherLab.PredictionSystem.Utils;

namespace WeatherLab.UIElements.common
{
    static class StringFormater
    {
        /// <summary>
        /// Class responsible for formating strings 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        /// strings :

        public static readonly string TEMPERATURE = "Température moyenne";
        public static readonly string HUMIDITY = "Humidité moyenne";
        public static readonly string PLUVIOMETRIE = "Précipitation moyenne";
        public static readonly string NUAGES = "Quantité de nuages moyenne ";
        public static readonly string WIND_SPEED = "Vitesse de vent moyenne ";
        public static readonly string WIND_DIRECTION = "Direction du vent";
        public static readonly string PRESSION = "Pression d'air moyenne";
        public static readonly string NA = "Non disponible";
        public static readonly string SI = " SI";

        /// 
        public static Dictionary<string, string> units = new Dictionary<string, string>
        {
            {InputKeys.TEMPERATURE,"°C" },
            {InputKeys.WIND_SPEED,"m/s" },
            {InputKeys.PLUVIOMETRIE,"%" },
            {InputKeys.WIND_DIRECTION,"°" },
            {InputKeys.NUAGES ,"%" },
            {InputKeys.AIR_PRESSURE,"Bar" },
            {InputKeys.HUMIDITY,"%" },
            {InputKeys.GROUND_STATE,SI }
        };

        public static String GetParameterValue(ParamPrediction param)
        {
            
            return (Math.Round(param.PredictedValue)).ToString("00") + " "+units[param.ParamKey];
        }
        public static String GetProbabiltyText(Result result)
        {
            
            return   Math.Round((result.Probability * 100), 2) + "%";
        }
        public static String GetClimate(Result result)
        {
            return result.Climate ;
        }
        public static string GetParameterFromKey(string paramKey)
        {
            if (paramKey.Equals(InputKeys.TEMPERATURE))
            {
                return TEMPERATURE;
            }else if (paramKey.Equals(InputKeys.HUMIDITY))
            {
                return HUMIDITY;
            }else if (paramKey.Equals(InputKeys.PLUVIOMETRIE))
            {
                return PLUVIOMETRIE;
            } else if (paramKey.Equals(InputKeys.NUAGES))
            {
                return NUAGES;
            }
            else if (paramKey.Equals(InputKeys.WIND_SPEED))
            {
                return WIND_SPEED;
            }
            else if (paramKey.Equals(InputKeys.WIND_DIRECTION))
            {
                return WIND_DIRECTION;
            }
            else if (paramKey.Equals(InputKeys.AIR_PRESSURE))
            {
                return PRESSION;
            }
            else
            {
                return NA;
            }
        }
        public static void SetUnit(string param, string unit)
        {
            units[param] = unit;
        }
        public static string GetUnit(string param)
        {
            return units[param];
        }
    }

   
}
