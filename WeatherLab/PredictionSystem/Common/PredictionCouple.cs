using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionFeature
{
    class PredictionCouple
    {
        /// <summary>
        /// This class represents a couple in the prediction table witch will be used to make future predictions
        /// @past represents a historical observation which is an array of double each index represents a parameterValue
        /// @future is posterior outcome given based on a period of time and is retreived from the historical data
        /// @similarity is a real between 0 and 1 representing how similar this past observation from a given daily observation 
        /// </summary>
        public static int NUMBER_OF_PARAMETERS = 4;
        private double[] past;
        private double[] future;
        private double similirity;
        public PredictionCouple(double[] past, double[] future)
        {
            this.past = past;
            this.future = future;
        }
        public double[] Past
        {
            get
            {
                return past;
            }
        }
        public double[] Future
        {
            get
            {
                return future;
            }
        }
        public double Similarity
        {
            get
            {
                return similirity;
            }
            set
            {
                similirity = value;
            }
        }
    }

}
