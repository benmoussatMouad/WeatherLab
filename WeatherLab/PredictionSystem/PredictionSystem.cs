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
        /// How To use the System : 
        /// 1- Set the inputs gathered from UI using SetInputs function (list of inputs )
        /// 2- use StartPrediction() to start the predictions
        /// 3- Access results from Result .
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
            InitSystems();
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

        
        /// Public Methods Section 
       

        ///<summary>
        ///
        /// Entry Point of prediction ,requires all systems Initilized and requires DailyMeteoSystem to have a non null observation
        /// 
        /// </summary>
        public void StartPrediction()
        {
            if(dailyMeteoSystem.Observation != null)
            {
                predictionManager.DailyObservation = dailyMeteoSystem.Observation;

              
                queryManager.GenerateQuery(dailyMeteoSystem.Observation);

                /// DataRetreiver.SetQuery(queryManager.query);
                /// DataRetreiver.GatherData();
                ///predictionManager.PredictionCouples = DataRetreiver.RetreiveData();
                if(predictionManager.PredictionCouples != null)
                {
                    predictionManager.Predict();
                    resultHandler.Predictions = predictionManager.Predictions; 
                    resultHandler.GenerateResults();
                }
            }
            else
            {
                throw new NullObservationException("DailyMeteoSystem Has not yet acquired Observation");
            }
            
        }

        public void SetInputs(List<Input> inputs)
        {
            if(inputs != null)
            {
                dailyMeteoSystem.NewObservation(inputs); /// creating a new Observation
                SetDurationInDays((int) inputs.Find(input => input.InputKey.Equals(InputKeys.DURATION)).Value); /// setting the duration of prediction in days
            }
           
        }


        
        /// <summary>
        /// Private Methods Section 
        /// </summary>

        ///Init method used to initialize subsystems to prevent having null objects
        private void InitSystems()
        {
            dailyMeteoSystem = new DailyMeteoSystem();
            predictionManager = new PredictionManager();
            queryManager = new QueryManager();
            dataRetreiver = new DataRetreiver();
            resultHandler = new ResultHandler();
        }

        private void SetDurationInDays(int days)
        {
            queryManager.SetDurationInDays(days);
        }

    }
}
