using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.PredictionSystem.Common;
namespace WeatherLab.PredictionSystem.Utils
{   /// <summary>
    ///  This class represents the entity in charge of building a query object to send it to DataRetreiver object in order to fetch for data in Dataset based on constraints like : date , selected parameters
    ///@ query is a contract for Data retreiver to use in his search for data;
    ///@durationInDays is set from exterior and is built in query ;
    /// </summary>
    class QueryManager      
    {

        private Query query;
        private int durationInDays;
        public QueryManager()
        {

        }

        public Query Query
        {
            get
            {
                return query;
            }
        }
        public void SetDurationInDays(int days)
        {
            this.durationInDays = days;
        }
        public void GenerateQuery(Observation observation)
        {   /// creating a new query object
            query = new Query();
            /// adding all parameter keys found in observation in the same order as they appear in it
            foreach(Parameter param in observation.Parameters)
            {
                query.ParameterKeys.Add(param.ParamKey);
            }
            query.Date = observation.Date;
            query.Duration = durationInDays;
            query.RequestedWilaya = (string) observation.Wilaya.Value;

        }

    }
}
