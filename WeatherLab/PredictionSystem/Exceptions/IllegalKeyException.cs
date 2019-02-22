
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Exceptions
{
    class IllegalKeyException:Exception
    {
        public IllegalKeyException()
        {

        }
        public IllegalKeyException(string message)
            : base(message) { }
        public IllegalKeyException(string message,Exception inner)
            : base(message,inner) { }
    }
}
