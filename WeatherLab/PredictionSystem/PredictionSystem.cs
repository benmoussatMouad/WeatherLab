using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.DailyMeteo;
using WeatherLab.PredictionSystem.Utils;

namespace WeatherLab.PredictionSystem
{
    class PredictionSystem
    {
        /// <summary>
        /// this class represents the system in charge of making forcasts (predictions) of weather based on
        /// historical data retreived from dataset
        /// </summary>
        
        // singelton instance in the application
        public static PredictionSystem predictionSystem= null;

        /// private instances of subsystems ;
        
        private DailyMeteoSystem dailyMeteoSystem;
        private PredictionManager predictionManager;
        private QueryManager queryManager;
        private DataRetreiver dataRetreiver;
        private ResultHandler resultHandler;

        private PredictionSystem()
        {
            initSystems();
        }

        // Getter for singleton instance 

        public static PredictionSystem Instance
        {
            get
            {
                if (predictionSystem == null)
                {
                    predictionSystem = new PredictionSystem();
                }
                return predictionSystem;
            }
        }


        /// <summary>
        /// Public Methods Section 
        /// </summary>

        
        /// <summary>
        /// Private Methods Section 
        /// </summary>

        ///Init method used to initialize subsystems to prevent having null objects
        private void initSystems()
        {
            dailyMeteoSystem = new DailyMeteoSystem();
            predictionManager = new PredictionManager();
            queryManager = new QueryManager();
            dataRetreiver = new DataRetreiver();
            resultHandler = new ResultHandler();
        }
    }
}
