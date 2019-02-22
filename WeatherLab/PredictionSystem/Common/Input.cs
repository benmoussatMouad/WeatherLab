using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common
{
    /// <summary>
    /// A class representing any type of Input that the user can enter or import example : Date , duration ...
    /// @inputKey is a unique Key defined in InputKeys Class that identifies each input object
    /// @value is any kind of Value this inputKey can represent ex : Temp => double , Date => DateTime
    /// </summary>
    abstract class  Input
    {
        protected string inputKey;
        protected Object value;

        public Input() { this.inputKey = InputKeys.NONE; this.value = null; }
        public Input(string inputKey, Object value)
        {
            this.inputKey = inputKey;
            this.value = value;
        }
        public Input(string inputKey) : this(inputKey, null) { }

        public string InputKey
        {
            get { return inputKey; }
            

        }

        public Object Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
