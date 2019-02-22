using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Exceptions
{
    class NullObservationException:Exception
    {
        public NullObservationException()
        {
        }

        public NullObservationException(string message)
            : base(message)
        {
        }

        public NullObservationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
