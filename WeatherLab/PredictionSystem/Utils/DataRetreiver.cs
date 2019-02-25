using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
//Started : 21/02/2019
//Expected end date: 23/02/2019
namespace WeatherLab.PredictionSystem.Utils
{
    
    //Ret
    class DataRetreiver
    {
        private Query query; 
        //contains the keys for each chosen parameter by the user.
        //and the chosen date and the chosen wilaya
        
        //The list that will contain all of the observations of the same season
        //concatenated with those +x duration
        private List<PredictionCouple>  observationsTable;

        
        
        public static void setQuery(Query _query)
        {
            query = _query;
        }

        //public PredictionCouple retreiveData();

        //public void gatherData();


    }
}
