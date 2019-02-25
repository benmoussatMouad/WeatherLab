using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PredictionFeature;
namespace WeatherLab.PredictionSystem.Utils
{
    class ResultHandler
    {
        private Dictionary<Cluster, List<ParamPrediction>> rawPredictions;
        private IOrderedEnumerable<KeyValuePair<Cluster, List<ParamPrediction>>> results;
        public ResultHandler()
        {

        }

        public Dictionary<Cluster,List<ParamPrediction>> Predictions
        {
            get { return rawPredictions; }
            set
            {
                rawPredictions = value;
            }
        }
        public IOrderedEnumerable<KeyValuePair<Cluster, List<ParamPrediction>>> Results
        {
            get { return results; }
        }


        public void GenerateResults()
        {
           if(rawPredictions != null)
            {
                results = rawPredictions.OrderByDescending(cluster => cluster.Key.GetProbability());
            }
            
        }
    }
}
