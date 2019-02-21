using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
using PredictionFeature;
namespace WeatherLab.PredictionSystem.Utils
{
    /// <summary>
    /// This class represents the The entity responsible of performing the predictions on the historical data
    /// @predictionCouples is a list of observations coupled with  their future outcome and having each a similarity field
    /// @dailyObservation is a daily input given by user to use in the prediction
    /// @clusters is a list of groupements used to make predictions for each parameter
    /// @predictions is the final result of predictions it corresponds each Cluster with the outcome predicted
    /// 
    /// // THIS CLASS IS NOT TESTED YET : reason waiting for DataSet and DataRetreiver Classes
    /// </summary>
    class PredictionManager
    {
        private List<PredictionCouple> predictionCouples;
        private Observation dailyObservation;
        private Dictionary<Cluster, List<ParamPrediction>> predictions;
        private double sumSimilarities;
        private List<Cluster> clusters;
        private List<Cluster> orderedClustersByProba;
        private const double CLUSTER_WIDTH = 0.1; 
        public PredictionManager()
        {

        }
        

        public List<PredictionCouple> PredictionCouples
        {
            get
            {
                return predictionCouples;
            }
            set
            {
                predictionCouples = value;
            }
        }
        public Observation DailyObservation
        {
            get
            {
                return dailyObservation;
            }
            set
            {
                dailyObservation = value;
            }

        }
        public Dictionary<Cluster,List<ParamPrediction>> Predictions
        {
            get
            {
                return predictions;
            }
        }
        public void Predict()
        {
            if(predictionCouples!=null && dailyObservation != null)
            {
                PredictionFunctions.SimilarityFunction(predictionCouples, dailyObservation.ToVector());

                sumSimilarities = CalculateSumOfSimilarities();

                clusters=PredictionFunctions.Cluster(predictionCouples, CLUSTER_WIDTH);

                CalculateProbabilities();

                orderedClustersByProba = PredictionFunctions.OrderByProbabiltyDescending(clusters);

                predictions = PredictionFunctions.Predict(orderedClustersByProba);

                SetParameterKeys();
            }
        }

        private double CalculateSumOfSimilarities()
        {
            double sum = 0;
            foreach(var item in predictionCouples)
            {
                sum = sum + item.Similarity;
            }
            return sum;
        }

        private void CalculateProbabilities()
        {
            if(clusters != null)
            {
                foreach(Cluster cluster in clusters)
                {
                    cluster.SetProbability(PredictionFunctions.ProbabiltyOfCluster(cluster,sumSimilarities));
                }
            }
        }

        /// <summary>
        /// Sets the parameter Key found in dailyObservation for each parameter in the list of predicted params for each list in the dictionnary
        /// used in order to make sure every parameter has his key to identify him for ulterior usage
        /// </summary>
        private void SetParameterKeys()
        {
            foreach(var list in predictions.Values)
            {
                for(int i = 0; i< list.Count; i++)
                {
                    list.ElementAt(i).ParamKey = dailyObservation.Parameters.ElementAt(i).ParamKey;
                }
            }

        }
    }
}
