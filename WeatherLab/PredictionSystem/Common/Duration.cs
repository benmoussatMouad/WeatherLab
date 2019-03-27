using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common
{
    class Duration:Input
    {
        public Duration(int duration) : base(InputKeys.DURATION, duration) { }

    }
}
