using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherLab.Data
{
    public class DataSet
    {
        #region attributs

        // représente toutes les attributs que le dataset doit contenir 
        private string[] attributs;

        // la liste des donnees de ce dataset
        public List<Observation> observations;

        #endregion

        #region Constructeurs

        public DataSet()
        {
            attributs = new string[10];
            this.observations = new List<Observation>();
        }

        public DataSet(string[] attrs)
        {
            this.attributs = attrs;
            this.observations = new List<Observation>();
        }

        #endregion

        #region MethodesDonnee

        /// <summary>
        /// ajouter une observation à la liste
        /// </summary>
        /// <Error>
        ///     <Nom>AttibutFormatException</Nom>
        ///     <Detail>si les attributs dans l'observation ne correspond pas au attributs dans le dataset</Detail>
        /// </Error>
        /// <Error>
        ///     <Nom>ArgumentNullException</Nom>
        ///     <Detail>si la liste des attributs n'est pas initialisée</Detail>
        /// </Error>
        /// <param name="obser">l'observation à ajouter</param>
        public void ajouterObservation(Observation obser)
        {
            if (obser.nbDonnees() != attributs.Length)
                throw new AttributFormatException();

            Donnee[] d = obser.getDonnees();
            for (int i = 0; i < d.Length; i++)
                if (!isValidAttribut(d[i].getAttribut()))
                    throw new AttributFormatException();

            observations.Add(obser);
        }

        public void ajouterObservation(DateTime d, string[] attr, float[] valeur)
        {
            ajouterObservation(new Observation(d, attr, valeur));
        }

        /// <summary>
        /// supprimer l'observation dans la position id
        /// </summary>
        /// <Error>
        ///     <Nom>ArrayOutOfBoundException</Nom>
        ///     <Detail>si l'indice est en dehors de la taille de la liste</Detail>
        /// </Error>
        /// <param name="id">l'indice de suppression</param>
        public void supprimerObservation(int id)
        {
            observations.RemoveAt(id);
        }

        /// <summary>
        /// modifier l'observation dans la position id
        /// </summary>
        /// <Error>
        ///     <Nom>ArrayOutOfBoundException</Nom>
        ///     <Detail>si l'indice est en dehors de la taille de la liste</Detail>
        /// </Error>
        /// <param name="id">l'indice de modification</param>
        public void modifierObservation(int id, Observation obser)
        {
            if (obser.nbDonnees() != attributs.Length)
                throw new AttributFormatException();

            Donnee[] d = obser.getDonnees();
            for (int i = 0; i < d.Length; i++)
                if (isValidAttribut(d[i].getAttribut()))
                    throw new AttributFormatException();

            observations[id] = obser;
        }

        public void modifierObservation(int id, DateTime d, string[] attr, float[] valeur)
        {
            modifierObservation(id, new Observation(d, attr, valeur));
        }

        #endregion

        #region MethodesAttributs

        public bool isValidAttribut(string attr)
        {
            return attributs.Contains(attr);
        }


        /// <summary>
        /// remplace le tableau des attributs avec 'attributs'
        /// </summary>
        /// <Error>
        ///     <Name>ArgumentException</Name>
        ///     <Detail>la longueure des attributs doit etre la meme</Detail>
        /// </Error>
        /// <param name="attributs">la liste des attributs contenant le dataset</param>
        public void modifierAttributs(string[] attributs)
        {
            if (this.attributs.Length != 10 || this.attributs.Length != attributs.Length)
                throw new ArgumentException();

            this.attributs = attributs;
        }

        public string[] getAttributs()
        {
            return attributs;
        }

        #endregion
    }
}
