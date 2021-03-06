﻿using System.Collections.Generic;
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
        private DecisionMaker decisionMaker;

        /// variables : 
        private int mode = 0;
        public readonly static int CEP = 1;
        public readonly static int SELECTION = 0;
        
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
        public int GetMode()
        {
            return mode;
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
        public int GetNbParameters()
        {
            return PredictionCouple.NUMBER_OF_PARAMETERS;
        }
        public Query GetQuery()
        {
            return queryManager.Query;
        }
        
        /// Public Methods Section 
       

        ///<summary>
        ///
        /// Entry Point of prediction ,requires all systems Initilized and requires DailyMeteoSystem to have a non null observation
        /// 
        /// </summary>
        public void StartPrediction()
        {
            if (dailyMeteoSystem.Observation != null)
            {
                predictionManager.DailyObservation = dailyMeteoSystem.Observation;


                queryManager.GenerateQuery(dailyMeteoSystem.Observation);
                PredictionCouple.NUMBER_OF_PARAMETERS = dailyMeteoSystem.Observation.Parameters.Count;
                
               
                /// Getting data from dataSet
                dataRetreiver.SetQuery(queryManager.Query);
                dataRetreiver.HandleQuery();
                dataRetreiver.SetObservationTable();
                predictionManager.PredictionCouples = dataRetreiver.GetObservations();
                ///
                if (predictionManager.PredictionCouples != null && mode ==CEP )
                {
                    
                    predictionManager.PredictByKNN();  
                    resultHandler.Predictions = predictionManager.Predictions; 
                    resultHandler.GenerateResults();
                    if(resultHandler.Results != null)
                    {
                        foreach(var prediction in resultHandler.Results)
                        {
                            decisionMaker.SetPredictions(prediction.Predictions);
                            prediction.Climate = decisionMaker.GetDecision();
                        }
                    }
                }else if(mode == SELECTION)
                {
                    resultHandler.SetResults(predictionManager.PredictBySelection());
                    if (resultHandler.Results != null)
                    {
                        foreach (var prediction in resultHandler.Results)
                        {
                            decisionMaker.SetPredictions(prediction.Predictions);
                            prediction.Climate = decisionMaker.GetDecision();
                        }
                    }

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
        public void SetPredictionPrecision(double pre = 0.1)
        {
            predictionManager.SetPredictionPrecision(pre); 
        }

        public void SetMode(int mode)
        {
            this.mode = mode;
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
            decisionMaker = new DecisionMaker();
        }

        private void SetDurationInDays(int days)
        {
            queryManager.SetDurationInDays(days);
        }
        public int GetDurationInDays()
        {
            return queryManager.GetDurationInDays();
        }
        public string GetRequestedWilaya()
        {
            return queryManager.Query.RequestedWilaya;
        }
        public void SetConfiguration(ConfigUtils.ConfigParser parser)
        {
            this.dataRetreiver.SetConfigParser(parser);
            
        }

    }
}
