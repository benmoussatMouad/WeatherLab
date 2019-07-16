using MathNet.Numerics.Statistics;
using System.Collections.Generic;

namespace WeatherLab.Synthése
{
    public static class Static
    {
        public static double Moyenne(List<double> Tab)
        {
            return Statistics.Mean(Tab);
        }
        public static double EcartType(List<double> Tab)
        {
            return Statistics.StandardDeviation(Tab);
        }
        public static double Minimum(List<double> Tab)
        {
            return Statistics.Minimum(Tab);
        }
        public static double Maximum(List<double> Tab)
        {
            return Statistics.Maximum(Tab);
        }
        public static double Mediane(List<double> Tab)
        {
            return Statistics.Median(Tab);
        }
        public static double Variance(List<double> Tab)
        {
            return Statistics.Variance(Tab);
        }
    }
}