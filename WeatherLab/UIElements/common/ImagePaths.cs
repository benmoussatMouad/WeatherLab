using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
using WeatherLab.PredictionSystem.Utils;

namespace WeatherLab.UIElements.common
{
    static class ImagePaths
    {
        //public static readonly string HIGH_TEMP_ICON_PATH = "pack://application:,,,/assets/icons/white/highTemp.png";
        //public static readonly string MID_TEMP_ICON_PATH = "pack://application:,,,/assets/icons/white/lowTemp.png";
        //public static readonly string WIND_SPEED_ICON_PATH = "pack://application:,,,/assets/icons/white/windWhite.png";
        //public static readonly string NA_ICON_PATH = "pack://application:,,,/assets/icons/white/NA.png";
        //public static readonly string LOW_PLUVIOMETRIE_ICON_PATH = "pack://application:,,,/assets/icons/white/rain.png";
        //public static readonly string NUAGES_ICON_PATH = "pack://application:,,,/assets/icons/white/cloud.png";
        //public static readonly string HIGH_PLUVIOMETRIE_ICON_PATH = "pack://application:,,,/assets/icons/white/rain.png";
        //public static readonly string WIND_DIRECTION_ICON_PATH = "pack://application:,,,/assets/icons/white/directionWhite.png";
        //public static readonly string GROUND_ICON_PATH = "pack://application:,,,/assets/icons/white/groundWhite.png";
        //public static readonly string HUMIDITY_ICON_PATH = "pack://application:,,,/assets/icons/white/drop.png";

        /// SVG icons 
        public static readonly string HIGH_TEMP_ICON_PATH = "./assets/icons/svg/Thermometer-75.svg";
        public static readonly string MID_TEMP_ICON_PATH = "./assets/icons/svg/Thermometer-25.svg";
        public static readonly string WIND_SPEED_ICON_PATH = "./assets/icons/svg/Wind.svg";
        public static readonly string NA_ICON_PATH = "./assets/icons/svg/NA.svg";
        public static readonly string LOW_PLUVIOMETRIE_ICON_PATH = "./assets/icons/svg/Cloud-Rain-Alt.svg";
        public static readonly string NUAGES_ICON_PATH = "./assets/icons/svg/Cloud.svg";
        public static readonly string HIGH_PLUVIOMETRIE_ICON_PATH = "./assets/icons/svg/Cloud-Rain.svg";
        public static readonly string WIND_DIRECTION_ICON_PATH = "./assets/icons/svg/Compass.svg";
        public static readonly string GROUND_ICON_PATH = "./assets/icons/svg/Cloud-Fog.svg";
        public static readonly string HUMIDITY_ICON_PATH = "./assets/icons/svg/Humidity.svg";
        public static readonly string AIR_PRESSURE_ICON_PATH = "./assets/icons/svg/air-pressure.svg";



        /// images 

        public static readonly string SUNNY_IMAGE = "./assets/icons/svg/Sun.svg";
        public static readonly string CLOUDY_IMAGE = "./assets/icons/svg/Cloud.svg";
        public static readonly string HEAVY_RAINY_IMAGE = "./assets/icons/svg/Cloud-Rain.svg";
        public static readonly string LOW_RAINY_IMAGE = "./assets/icons/svg/Cloud-Rain-Alt.svg";
        public static readonly string NA = "./assets/icons/svg/question-mark.svg";
        public static readonly string SUNNY_CLOUDS = "./assets/icons/svg/Cloud-Sun.svg";
        public static readonly string WINDY = "./assets/icons/svg/Cloud-Wind.svg";
        public static readonly string SNOWY = "./assets/icons/svg/Cloud-Snow.svg";
        /// 

        /// BACKGROUNDS 
        public static readonly string DEFAULT_BACKGROUND = "pack://application:,,,/assets/imgs/sunny.png";

        public static readonly string SUNNY_BACKGROUND = "pack://application:,,,/assets/imgs/sunny.png";

        public static readonly string CLOUDY_BACKGROUND = "pack://application:,,,/assets/imgs/cloudy2.png";

        public static readonly string HEAVYRAIN_BACKGROUND = "pack://application:,,,/assets/imgs/rain.png";


        public static readonly string LOWRAIN_BACKGROUND = "pack://application:,,,/assets/imgs/low_rain.png";

        public static readonly string NORAIN_BACKGROUND = "pack://application:,,,/assets/imgs/sea.jpg";

        public static readonly string CLOUDS_BACKGROUND = "pack://application:,,,/assets/imgs/cloudy.png";

        public static readonly string SNOWY_HIGH_BACKGROUND = "pack://application:,,,/assets/imgs/snowy2.jpg";

        public static readonly string SNOWY_LOW_BACKGROUND = "pack://application:,,,/assets/imgs/snowy1.jpg";

        public static readonly string WINDY_HIGH_BACKGROUND = "pack://application:,,,/assets/imgs/windy1.png";

        public static readonly string WINDY_LOW_BACKGROUND = "pack://application:,,,/assets/imgs/windy1.png";
        /// 


        public static string GetImagePathForParameter(ParamPrediction param)


        {
            if (param.ParamKey.Equals(InputKeys.TEMPERATURE))
            {
                if(param.PredictedValue >=33)
                {
                    return HIGH_TEMP_ICON_PATH;
                }else if (param.PredictedValue>= -19)
                {
                    return MID_TEMP_ICON_PATH;
                }
            }else if(param.ParamKey.Equals(InputKeys.WIND_SPEED))
            {
                return WIND_SPEED_ICON_PATH;
            }
            else if (param.ParamKey.Equals(InputKeys.WIND_DIRECTION))
            {
                return WIND_DIRECTION_ICON_PATH;
            }
            else if (param.ParamKey.Equals(InputKeys.GROUND_STATE))
            {
                return GROUND_ICON_PATH;
            }
            else if (param.ParamKey.Equals(InputKeys.PLUVIOMETRIE))
            {
                if (param.PredictedValue >= 50)
                {
                    return HIGH_PLUVIOMETRIE_ICON_PATH;
                }
                else if (param.PredictedValue >= 0)
                {
                    return LOW_PLUVIOMETRIE_ICON_PATH;
                }
                else
                {
                    return NA_ICON_PATH;
                }
            }
            else if (param.ParamKey.Equals(InputKeys.NUAGES))
            {
                return NUAGES_ICON_PATH;
            }
            else if (param.ParamKey.Equals(InputKeys.HUMIDITY))
            {
                return HUMIDITY_ICON_PATH;
            }
            else if (param.ParamKey.Equals(InputKeys.AIR_PRESSURE))
            {
                return AIR_PRESSURE_ICON_PATH;
            }
            return NA_ICON_PATH;
        }
        public static string GetOutlookImagePath(Result result)
        {
            string climate = result.Climate;
            if (climate.Equals(DecisionMaker.CLOUDS_ONLY))
            {
                return CLOUDY_IMAGE;
            }else if (climate.Equals(DecisionMaker.HEAVY_RAIN) )
            {
                return HEAVY_RAINY_IMAGE;
            }
            else if (climate.Equals(DecisionMaker.LOW_RAIN))
            {
                return LOW_RAINY_IMAGE;
            }
            else if(climate.Equals(DecisionMaker.SUNNY)){
                return SUNNY_IMAGE;
            }else if (climate.Equals(DecisionMaker.SUNNY_SMALL_CLOUDS))
            {
                return SUNNY_CLOUDS;
            }
            else if (climate.Equals(DecisionMaker.WIND_HIGH) || climate.Equals(DecisionMaker.WIND_LOW))
            {
                return WINDY;
            }
            else if (climate.Equals(DecisionMaker.SNOWY_HIGH) || climate.Equals(DecisionMaker.SNOWY_LOW))
            {
                return SNOWY;
            }
            return NA;
        }
    }
}
