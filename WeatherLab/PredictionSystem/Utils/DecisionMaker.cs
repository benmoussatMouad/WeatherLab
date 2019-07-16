using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
namespace WeatherLab.PredictionSystem.Utils
{
    class DecisionMaker
    {
        /// <summary>
        /// This class represents a decision maker that decides weather outlook based on parameters predictions 
        /// </summary>

        /// - DECISION CONSTANTS :

        public static readonly string SUNNY = "Ensoleillé";
        public static readonly string CLOUDS_ONLY = "Présence de nuages";
        public static readonly string SUNNY_SMALL_CLOUDS = "Ensoleillé avec présence de nuages";
        public static readonly string LOW_RAIN = "Pluie faible";
        public static readonly string HEAVY_RAIN = "Pluie forte";
        public static readonly string NO_RAIN = "Pas de pluie";
        public static readonly string CLIMATE_NA = "Climat non disponible";

        public static readonly string SNOWY_LOW = "Neige faible";

        public static readonly string SNOWY_HIGH = "Neige forte";

        public static readonly string WIND_LOW = "Vent faible";

        public static readonly string WIND_HIGH = "Vent Fort";
        /// 

        /// VALUES CONSTANTS :

        private static readonly double CLOUDS_CONSTANT = 25;
        private static readonly double RAIN_CONSTANT = 50;
        private static readonly double TEMP_CONSTANT = 2;
        private static readonly double WIND_CONSTANT = 55;
        ///

        private List<ParamPrediction> predictions;

        public DecisionMaker()
        {

        }



        public void SetPredictions(List<ParamPrediction> predictions)
        {
            this.predictions = predictions;
        }

        public string GetDecision()
        {
            if(predictions != null)
            {
                var nuages = predictions.Find(x => x.ParamKey.Equals(InputKeys.NUAGES));
                /// si nuages existe dans la liste 
                if (nuages != null)
                {
                    if(nuages.PredictedValue >=CLOUDS_CONSTANT)
                    {
                        var rain = predictions.Find(x => x.ParamKey.Equals(InputKeys.PLUVIOMETRIE));
                        if (rain != null)
                        {
                            var temp = predictions.Find(x => x.ParamKey.Equals(InputKeys.TEMPERATURE));
                            if (temp != null)
                            {
                                if (temp.PredictedValue > TEMP_CONSTANT)
                                {
                                    if (rain.PredictedValue >= RAIN_CONSTANT)
                                    {
                                        return HEAVY_RAIN;
                                    }
                                    else if (rain.PredictedValue > 5)
                                    {
                                        return LOW_RAIN;
                                    }
                                    else
                                    {
                                        return SUNNY_SMALL_CLOUDS;
                                    }
                                }
                                else
                                {
                                    if (rain.PredictedValue >= RAIN_CONSTANT)
                                    {
                                        return SNOWY_HIGH;
                                    }
                                    else if (rain.PredictedValue > 5)
                                    {
                                        return SNOWY_LOW;
                                    }
                                    else
                                    {
                                        return SUNNY_SMALL_CLOUDS;
                                    }
                                }
                            }

                        }else
                        {
                            return CLOUDS_ONLY;
                        }
                    } else if(nuages.PredictedValue != 0)
                    {
                        var wind = predictions.Find(x => x.ParamKey.Equals(InputKeys.WIND_SPEED));
                        if( wind != null)
                        {
                            if( wind.PredictedValue >=WIND_CONSTANT )
                            {
                                return WIND_HIGH;
                            }else if (wind.PredictedValue> 10)
                            {
                                return WIND_LOW;
                            }
                        }
                        return SUNNY_SMALL_CLOUDS;
                    }else
                    {
                        return SUNNY;
                    }
                } else
                {
                    var rain = predictions.Find(x => x.ParamKey.Equals(InputKeys.PLUVIOMETRIE));
                    if (rain != null)
                    {
                        var temp = predictions.Find(x => x.ParamKey.Equals(InputKeys.TEMPERATURE));
                        if( temp != null)
                        {
                            if( temp.PredictedValue > TEMP_CONSTANT)
                            {
                                if (rain.PredictedValue >= RAIN_CONSTANT)
                                {
                                    return HEAVY_RAIN;
                                }
                                else if (rain.PredictedValue > 5)
                                {
                                    return LOW_RAIN;
                                }
                                else
                                {
                                    return SUNNY;
                                }
                            } else
                            {
                                if (rain.PredictedValue >= RAIN_CONSTANT)
                                {
                                    return SNOWY_HIGH;
                                }
                                else if (rain.PredictedValue > 5)
                                {
                                    return SNOWY_LOW;
                                }
                                else
                                {
                                    return SUNNY;
                                }
                            }
                        }
                        
                    }
                }
            }
            var wind2 = predictions.Find(x => x.ParamKey.Equals(InputKeys.WIND_SPEED));
            if (wind2 != null)
            {
                if (wind2.PredictedValue >= WIND_CONSTANT)
                {
                    return WIND_HIGH;
                }
                else if (wind2.PredictedValue > 10)
                {
                    return WIND_LOW;
                }
            }
            return CLIMATE_NA;
        }


    }
}
