using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;

namespace WeatherLab.PredictionSystem.Utils
{
    static class SaveManager
    {
        public static String getCsvHeader( Query query)
        {
            StringBuilder str = new StringBuilder();
            String separator = ";";
            if (query != null)
            {
                str.Append("Date").Append(separator).Append("Wilaya").Append(separator).Append("Date predit").Append(separator);
                
                var param = string.Join(separator, query.ParameterKeys.ToArray());

                str.Append(param);
                str.Append(separator).Append("Prabability");

                Console.WriteLine(str); // debug
            }
            return str.ToString();
        }
        public static String getResultAsCsv(DateTime writeDate,DateTime predictionDate,String wilaya,Result result)
        {
            StringBuilder str = new StringBuilder();
            String separator = ";";
            if(result != null)
            {
                str.Append(writeDate.ToLocalTime().ToString()).Append(separator).Append(wilaya).Append(separator).Append(predictionDate.ToLocalTime().ToString());
                foreach(ParamPrediction p in result.Predictions)
                {
                    str.Append(separator);
                    str.Append(p.PredictedValue.ToString());
                }
                str.Append(separator).Append(result.Probability.ToString());
            }
            return str.ToString();
        }
    }
}
