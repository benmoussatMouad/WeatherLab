using System;
using System.Collections.Generic;

namespace WeatherLab.Data
{
    public class Donnee
    {

        #region Attributs

        private string attribut;
        private float valeur;

        #endregion

        #region Constructeurs

        public Donnee(string nom, float valeur)
        {
            this.attribut = nom;
            this.valeur = valeur;
        }

        #endregion

        #region SettersGetters

        public string getAttribut()
        {
            return attribut;
        }

        public void setAttribut(string nom)
        {
            this.attribut = nom;
        }

        public float getValeur()
        {
            return valeur;
        }

        public void setValeur(float val)
        {
            this.valeur = val;
        }

        #endregion
    }
}
