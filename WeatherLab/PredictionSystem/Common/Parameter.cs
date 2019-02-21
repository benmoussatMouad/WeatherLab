using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common
{
    class Parameter
    {
        private string paramKey;
        private double value;

        public Parameter(string paramKey , double value)
        {
            this.paramKey = paramKey;
            this.value = value;
        }
        public Parameter(string paramKey) : this(paramKey, 0) { }

        public string ParamKey
        {
            get { return paramKey; }
           

        }

        public double Value
        {
            get { return value; }
            set { this.value = value;}
        }

    }
}
