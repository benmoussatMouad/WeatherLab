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
    /// This is the class that will call the DataSet.
    ///  DataRetreiver will load and manipulate the DataSet and create from the correspendent dataset a
    /// list of prediction couples.
    /// </summary>
    class DataRetreiver
    {
        //contains the keys for each chosen parameter by the user.
        //and the chosen date and wilaya
        private Query query;

        private string _path;

        private Saison saison;

        //The list of datas (observations) retrieved from the dataset in the same season
        private List<Donnee> _donnees;
        private List<Donnee> _donneesSaisonSuivante;
        
        //The list that will contain all of the observations of the same season
        //concatenated with those +x duration
        private List<PredictionCouple>  observationsTable;

        //The DataSet we want to load
        private ManipDS _datatSet;

        public void SetQuery(Query query)
        {
            this.query = query;
        }


        public void HandleQuery()
        {

            //TODO: this might generate some expections, take care of it
             //_path = getWilayaPath(query.RequestedWilaya);
            
            switch (query.Date.Month)
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

        public void GatherData()
        {
            _datatSet = new ManipDS(_path);
            
            //Le partie du dataset contenant tous les observations de la meme saison
            _donnees = _datatSet.getSaison(saison);
            _donneesSaisonSuivante = _datatSet.getSaison(saison + 1); //et la saison suivante
            
            // having the number of all observations, Initilizing the observation table.
            observationsTable = new List<PredictionCouple>(_donnees.Count); 
            
            int delay = query.Duration;
            int numberOfParameters = query.ParameterKeys.Count;
            
            
            

            //TODO: Fill the observationsTable with observation according to parameters keys
            //::::::::::::::
            foreach (Donnee donnee in _donnees)
            {
                //Allocating new space for the double table
                double[] past = new double[numberOfParameters];
                double[] future = new double[numberOfParameters];
                
                foreach (string key in query.ParameterKeys)
                {
                    //Filling the tables of doubles, 
                    //TODO: Expections: key might not exist in the Dictionnary
                    past.Append(donnee.GetAttr(key));
                    
                    //TODO: An error might occur, going out of the list, add from the following dataset
                    future.Append(_donnees[_donnees.IndexOf(donnee) + delay].GetAttr(key));
                    
                }
                
                
                observationsTable.Add(new PredictionCouple(past, future));
                
            }
        }
    }
}
