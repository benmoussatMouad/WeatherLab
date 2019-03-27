using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    class AttributFormatException : Exception
    {

        public AttributFormatException()
        {

        }

        public AttributFormatException(string msg) : base(msg)
        {
        }
    }
}
