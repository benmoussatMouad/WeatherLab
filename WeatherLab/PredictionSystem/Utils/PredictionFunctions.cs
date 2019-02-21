using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;

namespace PredictionFeature
{
    /// <summary>
    /// Static Class Containing static methods used for making predictions via PredictionManager
    /// <remarks> Exceptions missing </remarks>
    /// </summary>
    static class PredictionFunctions
    {
        /// <summary>
        /// Calculates the Standard Deviation of an array of observations
        /// </summary>
        /// <param name="observations"></param>
        /// <param name="daily"></param>
        /// <returns></returns>
        static double DistanceStandardDeviation(IEnumerable<PredictionCouple> predictionCouples, Vector<double> daily)
        {
            int i = 0;
            var distances = new double[predictionCouples.Count()];
            foreach (var item in predictionCouples)
            {
                distances[i] = Distance.Euclidean(item.Past, daily.ToArray());
                i++;
            }
            return Statistics.PopulationStandardDeviation(distances);
        }

        /// <summary>
        /// Uses the Gaussian Kernel to calculate a similarity Value between 0 and 1 for the two vectors vector1 and vector2
        /// the more the similarity tends to 1 the more the two vectors are similar and the more it tends to zero the more they are dissimilar
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="std">Standard deviation of the all distances in the data population used to regulate the function to give meeningfull results</param>
        /// <returns>a similarity double mesure between 0 and 1 </returns>
        static double RadialBasisFunction(Vector<double> vector1, Vector<double> vector2, double std)
        {
            if (vector1.Count() == vector2.Count())
            {

                var dist = Distance.Euclidean(vector1, vector2);
                
                var sim = Math.Exp(-0.5 * Math.Pow(dist / std, 2));
                return sim;

            }
            return 0;

        }
        public static void SimilarityFunction(IEnumerable<PredictionCouple> predictionCouples, Vector<double> daily)
        {

            var std = DistanceStandardDeviation(predictionCouples, daily);
            
            foreach (var item in predictionCouples)
            {
                item.Similarity = RadialBasisFunction(Vector.Build.DenseOfArray(item.Past), daily, std);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks> Modify parameters such that no need for index => search based on param key </remarks>
        /// <param name="cluster">The cluster that the function will predict Parameters for</param>
        /// <param name="paramKey">The parameter Key reprenting the parameter as a string </param>
        /// <param name="index">index of the parameter in the array(Future) of each element in the cluster</param>
        /// <returns>A parameter Prediction</returns>
         static ParamPrediction PredictParamsCluster(Cluster cluster, String paramKey, int index)
        {
            List<double> inputs = new List<double>();
            foreach (var e in cluster.Elements)
            {
                inputs.Add(e.Future.ElementAt(index));
            }

            ParamPrediction paramPrediction = new ParamPrediction(paramKey, Statistics.Mean(inputs), Statistics.PopulationStandardDeviation(inputs));

            return paramPrediction;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusters">a list of ordered clusters by probability</param>
        /// <returns>a Dictionary of Pairs of Cluster and the list of Parameter Predictions</returns>
        public static Dictionary<Cluster, List<ParamPrediction>> Predict(List<Cluster> clusters)
        {
            Dictionary<Cluster, List<ParamPrediction>> map = new Dictionary<Cluster, List<ParamPrediction>>();
            foreach (Cluster cluster in clusters)
            {
                if (cluster.GetProbability() != 0)
                {
                    List<ParamPrediction> clusterList = new List<ParamPrediction>();
                    for (int i = 0; i < 4; i++)
                    {
                        clusterList.Add(PredictParamsCluster(cluster, "Key", i));
                    }
                    map.Add(cluster, clusterList);
                }

            }
            return map;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusters">List of unordered clusters</param>
        /// <returns>ordered list of Clusters by probability in decreasing order</returns>
        public static List<Cluster> OrderByProbabiltyDescending(List<Cluster> clusters)
        {
            if (clusters != null)
            {

                List<Cluster> sortedClusters = clusters.OrderByDescending(cluster => cluster.GetProbability()).ToList();
                return sortedClusters;
            }

            return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cluster">the cluster that we want to calculate the probability</param>
        /// <param name="sumSimilarites">the sum of all similarity mesures of the population</param>
        /// <returns>the probability Of the cluster</returns>
        public static double ProbabiltyOfCluster(Cluster cluster, double sumSimilarites)
        {
            if (cluster != null && sumSimilarites > 0)
            {
                double sum = 0;
                foreach (var item in cluster.Elements)
                {
                    sum += item.Similarity;
                }
                return sum / sumSimilarites;
            }
            return 0;/// probabilty of null cluster is 0
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawCouples">unclustered array of PredictionCouples having each a similarity assigned to it</param>
        /// <param name="clusterWidth"> the width of the cluster grouping this PredictionCouples ex: 0.1 </param>
        /// <returns></returns>
        public static List<Cluster> Cluster(IEnumerable<PredictionCouple> rawCouples, double clusterWidth)
        {
            /// TODO : optimize Function
            List<Cluster> finalList = new List<Cluster>();
            double startingBound = 0;
            var sortedData = from item in rawCouples
                             orderby item.Similarity descending
                             select item;
            if (clusterWidth > 1)
            {
                clusterWidth = 1;
            }

            while (startingBound <= 1 - clusterWidth)
            {
                Cluster cluster = new Cluster(startingBound, startingBound + clusterWidth);
                ///TODO : optimize this piece of code to not traverse all sortedData everyTime
                foreach (PredictionCouple item in sortedData)
                {
                    /// add element if he had similarity in this cluster
                    cluster.AddElement(item);
                }

                finalList.Add(cluster);

                startingBound += clusterWidth;
            }


            return finalList;

        }

    }
}
