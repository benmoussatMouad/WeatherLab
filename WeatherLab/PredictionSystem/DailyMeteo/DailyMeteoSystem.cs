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
                if(inputs != null)
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
            if(inputs != null)
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

        private void BuildObservation()
        {
            
            observation = new Observation();
            foreach (var input in inputs)
            {
                if (!input.InputKey.Equals(InputKeys.DATE) && !input.InputKey.Equals(InputKeys.DURATION))
                {
                    observation.AddParameter((Parameter)input);
                } else
                {
                    throw new IllegalKeyException("Cannot include DATE or DURATION in Observation");
                }
            }
        }
    }
}
