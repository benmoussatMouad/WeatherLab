using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.DailyMeteo;
using WeatherLab.PredictionSystem.Utils;
using WeatherLab.PredictionSystem.Common;
using WeatherLab.PredictionSystem.Exceptions;
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
        public DailyMeteoSystem DailyMeteoSystem {
            get
            {
                return dailyMeteoSystem;
            }
        }
        public ResultHandler Result
        {
            get
            {
                return resultHandler;
            }
        }

        /// <summary>
        /// Public Methods Section 
        /// </summary>

        public void StartPrediction()
        {
            if(dailyMeteoSystem.Observation != null)
            {
                predictionManager.DailyObservation = dailyMeteoSystem.Observation;
                /// QueryManager.GenerateQuery(dailyMeteoSystem.Observation);
                /// DataRetreiver.SetQuery(QueryManager.Query);
                /// DataRetreiver.GatherData();
                ///predictionManager.PredictionCouples = DataRetreiver.RetreiveData();
                if(predictionManager.PredictionCouples != null)
                {
                    predictionManager.Predict();
                    /// resultHandler.Predictions = predictionManager.Predictions; 
                    /// resultHandler.GenerateResults();
                }
            }
            else
            {
                throw new NullObservationException("DailyMeteoSystem Has not yet acquired Observation");
            }
            
        }
        
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
