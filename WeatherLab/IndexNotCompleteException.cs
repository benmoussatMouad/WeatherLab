using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    class IndexNotCompleteException : Exception
    {
        public IndexNotCompleteException()
        {

        }

        public IndexNotCompleteException(string msg) : base(msg)
        {

        }

    }
}
