using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.UIElements.common;
namespace WeatherLab.PredictionSystem.Common
{
    /// <summary>
    /// Class representing a prediction Parameter that can be used in distances and similarity mesures 
    /// derives from Input class because it will be retreived from UI / FILE 
    /// </summary>
    class Parameter:Input
    {
        private string used_unit;

        public Parameter(string paramKey , double value):base(paramKey,value)
        {
            used_unit = StringFormater.GetUnit(paramKey);
        }
        public Parameter(string paramKey) : this(paramKey, 0) { used_unit = StringFormater.GetUnit(paramKey); }
        public Parameter(string paramKey, double value , string unit) : base(paramKey, value)
        {
            used_unit = unit;
            StringFormater.SetUnit(paramKey, unit);
        }

        public string ParamKey
        {
            get { return inputKey; }
           

        }

        public new double Value
        {
            get { return (double ) value; }
            set { this.value = value;}
        }

    }
}
