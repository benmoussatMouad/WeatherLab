using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
namespace WeatherLab.PredictionSystem.Common
{
    class Observation
    {
        /// <summary>
        /// Represents the final Observation built by DailyMeteo to be used in Prediction
        /// </summary>
        private List<Parameter> parameters;
        private HashSet<string> keys;
        private DateTime observationDate;
        public Observation(List<Parameter> parameters)
        {
            this.keys = new HashSet<string>();
            this.observationDate = new DateTime();
            this.parameters = parameters;
            foreach(Parameter param in parameters)
            {
                keys.Add(param.ParamKey);
            }
        }
        public Observation() { this.parameters = new List<Parameter>(); this.keys = new HashSet<string>(); this.observationDate = new DateTime(); }
        public Observation(List<Parameter> parameters, DateTime datetime)
        {
            this.keys = new HashSet<string>();
            this.observationDate = datetime;
            this.parameters = parameters;
            foreach (Parameter param in parameters)
            {
                keys.Add(param.ParamKey);
            }
        }

        public bool AddParameter(Parameter parameter)
        {
            if( parameter == null)
            {
                return false;
            }
            if (!keys.Contains(parameter.ParamKey))
            {
                parameters.Add(parameter);
                keys.Add(parameter.ParamKey);
                return true;
            }
            return false;
        }

        public List<Parameter> Parameters
        {
            get
            {
                return parameters;
            }
        }
        public DateTime Date
        {
            get
            {
                return observationDate;
            }
        }

        /// <summary>
        /// Method used to do the query 
        /// </summary>
        /// <returns>the month of the Daily Observation</returns>
        public int GetMonth()
        {
            return observationDate.Month;
        }

        public Vector<double> ToVector()
        {
            var doubleList = from x in parameters
                             select x.Value;
            Vector<double> vector = Vector.Build.DenseOfArray(doubleList.ToArray());
            return vector;
        }

    }
}
