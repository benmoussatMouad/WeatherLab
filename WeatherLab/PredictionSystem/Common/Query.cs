using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common
{
    class Query
    {
        private List<string> parameterKeys;
        private string requestedWilaya;
        private DateTime date;
        private int duration;

        public Query()
        {

        }
        public List<string> ParameterKeys
        {
            get
            {
                return parameterKeys;
            }
        }
        public string RequestedWilaya
        {
            get
            {
                return requestedWilaya;
            }
            set
            {
                requestedWilaya = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
    }
}
