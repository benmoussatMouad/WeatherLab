using MathNet.Numerics.Statistics;

namespace LiveChart
{
    class Static
    {
        public double Moyenne(double[] Tab)
        {
            return Statistics.Mean(Tab);
        }
        public double EcartType(double[] Tab)
        {
            return Statistics.StandardDeviation(Tab);
        }
        public double Minimum(double[] Tab)
        {
            return Statistics.Minimum(Tab);
        }
        public double Maximum(double[] Tab)
        {
            return Statistics.Maximum(Tab);
        }
        public double Mediane(double[] Tab)
        {
            return Statistics.Median(Tab);
        }
        public double Variance(double[] Tab)
        {
            return Statistics.Variance(Tab);
        }
    }
}
