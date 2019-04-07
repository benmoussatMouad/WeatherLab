using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    public abstract class DataParser
    {

        #region attributs

        private string path;

        #endregion

        #region Constructeur

        public DataParser(string path)
        {
            this.path = path;
        }

        #endregion

        #region MethodesAbstraites

        /// <summary>
        /// retourne la liste des attributs existant dans le fichier
        /// </summary>
        abstract public string[] getAttributs();

        /// <summary>
        /// retourne une observation du fichier
        /// </summary>
        /// <param name="attributs">la liste des attributs du fichier</param>
        /// <param name="valeurs">les valeurs correspondant au attributs</param>
        /// <Error>
        ///     <Name>EndOfStreamException</Name>
        ///     <Detail>la fin du fichier a été atteint</Detail>
        /// </Error>
        /// <returns>le nombre d'elements dans attributs et valeurs</returns>
        abstract public int getObservation(out string[] attributs, out string[] valeurs);

        abstract public Observation getObservation();

        /// <summary>
        /// ajoute une observation au fichier
        /// </summary>
        /// <param name="attributs"></param>
        /// <param name="valeurs"></param>
        /// <returns></returns>
        abstract public void addObservation(DateTime d, float[] valeurs);

        abstract public void addObservation(Observation observation);

        /// <summary>
        /// modifier le nom d'un attribut
        /// </summary>
        abstract public void renommerAttribut(string ancien, string nouveau);

        abstract public Observation[] getObservations();

        #endregion

        #region SettersGetters

        public void setPath(string path) { this.path = path; }

        public string getPath() { return path; }

        #endregion
    }
}
