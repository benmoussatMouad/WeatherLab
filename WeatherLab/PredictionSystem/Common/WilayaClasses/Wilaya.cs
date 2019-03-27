using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common
{
    /// <summary>
    /// Class represents a Wilaya in the application and is a subclass of the Abstract Input class 
    /// </summary>
    class Wilaya : Input
    {
        private int wilayaNumber;
        public Wilaya(string wilayaName) : base(InputKeys.WILAYA, wilayaName) {
            /// set wilaya number
        }
        public Wilaya(int wilayaNumber) : base(InputKeys.WILAYA) {
            this.wilayaNumber = wilayaNumber;
            /// set wilaya name
        }

    }
}
