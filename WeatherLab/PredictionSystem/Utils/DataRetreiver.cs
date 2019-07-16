using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
using WeatherLab.Data;
using WeatherLab.ConfigUtils;
using Observation = WeatherLab.Data.Observation;


//Started : 21/02/2019
//By: Benmoussat Mouad
namespace WeatherLab.PredictionSystem.Utils
{
    
    class DataRetreiver
    {
        private string _path;

        private ConfigParser config;

        /// <summary>
        /// This is the class that will call the DataSet.
        ///  DataRetreiver will load and manipulate the DataSet and create from the correspendent dataset a
        /// list of prediction couples.
        /// </summary>
        
        
        private Query query;
        //contains the keys for each chosen parameter by the user.
            //and the chosen date and wilaya
        
        private ConfigUtils.Wilaya consideredWilaya;
            //To get the requested wilaya in the query and use the config tool to communicate with the dataset

        private Saison saison;
            //To get the requested season

        //The list of datas (observations) retrieved from the dataset in the same season
        private List<Observation> _donnees;
        private List<Observation> _donneesSaisonSuivante;
        
        //The list that will contain all of the observations of the same season
        //concatenated with those +x duration
        private List<PredictionCouple>  observationsTable;

        //The DataSet we want to load
        private ManipDS _datatSet;

        public DataRetreiver()
        {
        }

        public DataRetreiver(ConfigParser config)
        {
            this.config = config;
        }

        public void SetQuery(Query query)
        {
            this.query = query;
        }

        public void SetConfigParser(ConfigParser parser)
        {
            this.config = parser;
        }

        public void HandleQuery()
        {

            consideredWilaya = config.getWilaya(query.RequestedWilaya);
            _path = consideredWilaya.path;
            
            switch (query.Date.Month)
            {
                case 12:
                case 1:
                case 2:
                    saison =  Saison.hiver;
                    break;
                case 3:
                case 4:
                case 5:
                    saison =  Saison.pringtemps;
                    break;
                case 6:
                case 7:
                case 8:
                    saison =  Saison.été;
                    break;
                default:
                    saison =  Saison.automne;
                    break;
            }

            //***DEBUG ONLY**
            //Console.WriteLine("******HANDLE QUERY : WILAYA : " + consideredWilaya.code + " path : " + _path +
            //                  "SEASON: " + saison.ToString() + " Keys: " + query.ParameterKeys[0]);
            //Console.Read();
            //***************
        }
       

        public void SetObservationTable()
        {
            _datatSet = new ManipDS(_path);
            
            //Le partie du dataset contenant tous les observations de la meme saison
            _donnees = _datatSet.getSaison(saison);
            
            _donneesSaisonSuivante = _datatSet.getSaison(
                ((saison + 1) > Saison.automne) ? (Saison.hiver) : (saison + 1) //gestion d'une erreur 
                ); //et la saison suivante
            
            
            //************************************
            //_donnees = FilterToDays(_donnees);
            //_donneesSaisonSuivante = FilterToDays(_donneesSaisonsSuivante);
            //************************************
            
            // having the number of all observations, Initilizing the observation table.
            observationsTable = new List<PredictionCouple>(_donnees.Count); 
            
            int delay = query.Duration;
            int numberOfParameters = query.ParameterKeys.Count;
            
            //****By tha way, knowing that the attributes in the application are different from
            //those in the dataset, so we need a conversion. (Line: 115)
            
            
            

            foreach (Observation observation in _donnees)
            {
                //Allocating new space for the double table
                List<double> pastList = new List<double>(numberOfParameters);
                List<double> futureList = new List<double>(numberOfParameters);
                
                foreach (string key in query.ParameterKeys)
                {
                    //**DEBUG ONLY****
                    //Console.WriteLine("Key : " + key + " in ds : " + consideredWilaya.getAttr(key) + " value of the 1st obs : " + observation
                    //  .getDonnee(consideredWilaya.getAttr(key)) + " Datetime" + observation.GetDate().ToString());
                    //Console.Read();
                    //****************


                    //Filling the tables of doubles, 
                    //TODO: Expections: key might not exist in the Dictionnary
                    pastList.Add( observation.getDonnee(
                        consideredWilaya.getAttr(key)
                        ));

                    try
                    {
                        futureList.Add(
                            _donnees[_donnees.IndexOf(observation) + delay].getDonnee(
                                consideredWilaya.getAttr(key)
                            ));
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        //If we go beyond the first list of the current season we will get those in the next season
                        futureList.Add(
                            _donneesSaisonSuivante[_donnees.Count - _donnees.IndexOf(observation)].getDonnee(
                                consideredWilaya.getAttr(key)
                            ));
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                        throw;
                    }
                    
                }
                
                double[] past = new double[numberOfParameters];
                double[] future = new double[numberOfParameters];
                past = pastList.ToArray();
                future = futureList.ToArray();
                
                observationsTable.Add(new PredictionCouple(past, future));
                
                
            }
        }

        public List<PredictionCouple> GetObservations()
        {
            //Supposing that the observation table contains the observations by time, not by day.
            return observationsTable;
        }
        
        
    }
}
