using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeatherLab.PredictionSystem.Common;
namespace WeatherLab.PredictionSystem.Utils
{
    class ResultHandler
    {
        private Dictionary<Cluster, List<ParamPrediction>> rawPredictions;
        private List<Result> results;
        public ResultHandler()
        {
            results = new List<Result>();
        }

        public Dictionary<Cluster,List<ParamPrediction>> Predictions
        {
            get { return rawPredictions; }
            set
            {
                rawPredictions = value;
            }
        }
        public List<Result> Results
        {
            get { return results; }
        }


        public void GenerateResults()
        {
            if(results != null)
            {
                results.Clear();
            }
           if(rawPredictions != null)
            {
               var temp = rawPredictions.OrderByDescending(cluster => cluster.Key.GetProbability());
                foreach(var item in temp)
                {
                    Result result = new Result(item.Value, item.Key.GetProbability());
                    results.Add(result);
                }
            }
            
        }
        public void SetResults(List<Result> results)
        {
            this.results = results;
        } 
    }
}
