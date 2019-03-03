using PredictionFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common
{
    class Result
    {
        
        private List<ParamPrediction> predictions;
        private double probability;
        public Result(List<ParamPrediction> predictions,double probability)
        {
            
            this.predictions = predictions;
            this.probability = probability;
        }

        public List<ParamPrediction> Predictions
        {
            get
            {
                return predictions;
            }
        }
        public double Probability
        {
            get
            {
                return probability;
            }
        }
    }
}
