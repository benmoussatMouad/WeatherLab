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
        private List<Parameter> parameters;
        private HashSet<string> keys;
        public Observation(List<Parameter> parameters)
        {
            this.keys = new HashSet<string>();
            this.parameters = parameters;
            foreach(Parameter param in parameters)
            {
                keys.Add(param.ParamKey);
            }
        }
        public Observation() { this.parameters = new List<Parameter>(); this.keys = new HashSet<string>(); }


        public bool AddParameter(Parameter parameter)
        {
            if( parameter == null)
            {
                return false;
            }
            if (!keys.Contains(parameter.ParamKey))
            {
                parameters.Add(parameter);
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

        public Vector<double> ToVector()
        {
            ////TODO : implement how to convert observation to Vector in Rn 
            var doubleList = from x in parameters
                             select x.Value;
            Vector<double> vector = Vector.Build.DenseOfArray(doubleList.ToArray());
            return vector;
        }

    }
}
