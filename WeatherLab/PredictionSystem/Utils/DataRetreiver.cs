using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
using WeatherLab.Data;


//Started : 21/02/2019
//Expected end date: 23/02/2019
namespace WeatherLab.PredictionSystem.Utils
{
    /// <summary>
    /// This is the class that will call the DataSet, it will be accessed from diffrent parts of the program
    /// Once the path entered by the user, DataRetreiver will load and manipulate the DataSet
    /// </summary>
    class DataRetreiver
    {
        private string path;

        //contains the keys for each chosen parameter by the user.
        //and the chosen date and wilaya
        private Query query;

        private Saison saison;

        //The list of datas (observations) retrieved from the dataset in the same season
        private List<Donnee> _donnees;
        
        //The list that will contain all of the observations of the same season
        //concatenated with those +x duration
        private List<PredictionCouple>  observationsTable;

        private ManipDS dataSet;


        public DataRetreiver(string path)
        {
            this.path = path;
            dataSet = new ManipDS(path);
        }

        public void handleQuery(Query _query)
        {
            query = _query;
            
            
            switch (_query.Date.Month)
            {
                case 1:
                case 2:
                case 3:
                    saison =  Saison.hiver;
                    break;
                case 4:
                case 5:
                case 6:
                    saison =  Saison.pringtemps;
                    break;
                case 7:
                case 8:
                case 9:
                    saison =  Saison.été;
                    break;
                default:
                    saison =  Saison.automne;
                    break;
            }
            
        }
        

        //public PredictionCouple retreiveData();

        public void gatherData()
        {
            _donnees = dataSet.getSaison(saison); //Le partie du dataset contenant tous les observations de la meme saison
            
            // having the number of all observations, Initilizing the observation table.
            observationsTable = new List<PredictionCouple>(_donnees.Count); 
            
            int delay = query.Duration;
            int numberOfParameters = query.ParameterKeys.Count;

            double[] past = new double[numberOfParameters];
            double[] future = new double[numberOfParameters];
            PredictionCouple.NUMBER_OF_PARAMETERS = numberOfParameters;
            
            Attribut.
            

            //TODO: Fill the observationsTable with observation according to parameters keys
            foreach (Donnee donnee in _donnees)
            {
                foreach (string key in query.ParameterKeys)
                {
                    //Filling the tables of doubles, 
                    past.Append(donnee.GetAttr(key));
                    
                    //TODO: An error might occur, going out of the list
                    future.Append(_donnees[_donnees.IndexOf(donnee) + delay].GetAttr(key));
                    
                }
                observationsTable[_donnees.IndexOf(donnee)] = new PredictionCouple(past, future);
            }
        }
        


    }
}
