using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionFeature
{
    class Cluster
    {

        /// <summary>
        /// This class represents a groupe/cluster/set of PredictionCouples .
        /// used to groupe Couples by similarity and then calculate their probabilty.
        /// @startingEdge is lower bound of similarity that elements must have greater than .
        /// @endingEdge is higher bound of similarity , elements must have this lower or equal to to be in this cluster
        /// 
        /// </summary>
        private List<PredictionCouple> elements;
        private double probability;
        private double startingEdge;
        private double endingEdge;
        public Cluster(double startingEdge, double endingEdge)
        {
            elements = new List<PredictionCouple>();
            this.startingEdge = startingEdge;
            this.endingEdge = endingEdge;
            this.probability = 1;
        }
        public List<PredictionCouple> Elements
        {
            get
            {
                return elements;
            }
            set
            {
                elements = value;
            }
        }
        public double Start
        {
            get
            {
                return startingEdge;
            }
        }
        public double End
        {
            get
            {
                return endingEdge;
            }
        }
        public void SetProbability(double proba)
        {
            this.probability = proba;
        }
        public double GetProbability()
        {
            return probability;
        }
        public bool AddElement(PredictionCouple couple)
        {
            if(couple.Past !=null && couple.Future != null)
            {   if(couple.Similarity > startingEdge && couple.Similarity <= endingEdge)
                {
                    elements.Add(couple);
                    return true;
                }
                return false;
            }
            return false;
        }


        

    }
}
