using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Utils
{
    public class QueryManager       //Cette classe contient une liste d'inputs selon le choix du User + Le delait de prediction + La date du jour 
    {
        public List<ParamValue> Liste = new List<ParamValue>();
        public double delai { get; set; }
        public DateTime date = new DateTime();

        public void add(ParamValue newParamValue)       //CEtte méthode ajoute un nouveau tuplet ParamValue a la liste
        {
            this.Liste.Add(newParamValue);
        }

        public int length()         //REtourne la taille de la liste
        {
            return Liste.Count;
        }

    }

    public class ParamValue        //Cette Classe va contenir Le tuplet <Param,Value> 
    {
        public Param Parametre { get; set; }
        public int Value { get; set; }
    }

    public enum Param       //Ce type contient Les parametres qui se trouve dans le Dataset et que l'utilisateur pourra Choisir Par la suite
    {
        Temp,
        Pression,
        Humidite,
        DirectionVent,
        VitesseVent,
        PourcentageNuage,
        DistanceVisibilite,
        EtatSol
    }
}
