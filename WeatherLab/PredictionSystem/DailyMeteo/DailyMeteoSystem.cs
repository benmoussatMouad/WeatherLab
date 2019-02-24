using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
using WeatherLab.PredictionSystem.Exceptions;
namespace WeatherLab.PredictionSystem.DailyMeteo
{
    /// <summary>
    /// A subsystem of the General PredictionSystem that is charged to gather Daily observation Inputs and serve them to Prediction System
    /// </summary>
    class DailyMeteoSystem
    {
        private Observation observation;
        private List<Input> inputs;
        public DailyMeteoSystem(List<Parameter> inputs)
        {
            BuildObservation();
        }
        public DailyMeteoSystem() { }
        public Observation Observation
        {
            get
            {
                if (inputs != null)
                {
                    return observation;
                }
                return null;
            }
        }

        public List<Input> Inputs
        {
            get
            {
                return inputs;
            }
        }

        public Observation NewObservation(List<Input> inputs)
        {
            if (inputs != null)
            {
                this.inputs = inputs;
                BuildObservation();
                return observation;
            }
            else
            {
                throw new NullReferenceException("Inputs are null => cannot create Observation");
            }

        }
        /// <summary>
        /// Method used to create an observation based on a list of inputs (Class Input) coming from ui each having A String Key and a value which is an object(depends on the type of the input)
        /// </summary>
        private void BuildObservation()
        {

            observation = new Observation();
            foreach (var input in inputs)
            {
                if (input.InputKey.Equals(InputKeys.WILAYA))
                {
                    observation.Wilaya = (string) input.Value;
                }else if(input is Parameter) {
                    observation.AddParameter((Parameter) input);
                }else
                {
                    /// TODO : implement other keys Code 
                }

            }
        }
    }
}