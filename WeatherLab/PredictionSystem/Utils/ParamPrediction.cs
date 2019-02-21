using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionFeature
{
    class ParamPrediction
    {
        /// <summary>
        /// This class represent a predected parameter like : temprature ,wind speed ...
        /// @paramKey is a string used to identify the predicted parameter
        /// @standardDeviation is used to calculate how much the real value is away from the predictedValue if small then prediction is quite good
        /// @Variance is used to calculate how much the predictedValue varies from realValue 
        /// @coeffVariation is used to mesure the ratio of the std over the mean it represents how valid is our prediction the more it is smalll
        /// </summary>
        private string paramKey;
        private double predictedValue;
        private double standardDeviation;
        private double variance;
        private double coeffVariation;
        public ParamPrediction(string paramKey) {
            this.paramKey = paramKey;
        }
        public ParamPrediction(string paramKey, double predictedValue, double std)
        {
            this.paramKey = paramKey;
            this.predictedValue = predictedValue;
            this.standardDeviation = std;
            this.variance = Math.Sqrt(std);
            this.coeffVariation = std / predictedValue;
        }
        public double PredictedValue
        {
            get
            {
                return predictedValue;
            }

        }

        public double StandardDeviation
        {
            get
            {
                return standardDeviation;
            }

        }
        public double Variance
        {
            get
            {
                return variance;
            }
        }

        public string ParamKey
        {
            get
            {
                return paramKey;
            }

        }
        public double CoeffVariation
        {
            get
            {
                return coeffVariation;
            }
        }
    }
}
