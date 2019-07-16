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

        /// <summary>
        /// le contructeur par défault
        /// </summary>
        /// <Error>
        ///    FileNotFoundException
        /// </Error>
        /// <param name="path"></param>
        public DataParser(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            this.path = path;
        }

        #endregion

        #region MethodesAbstraites

        /// <summary>
        /// retourne la liste des attributs existant dans le fichier
        /// </summary>
        /// <Error>
        /// FormatException
        /// </Error>
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
        /// <Error>
        ///     <Name>ArgumentException</Name>
        ///     <Detail>si les valeurs ne correspond pas au attributs</Detail>
        /// </Error>
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
