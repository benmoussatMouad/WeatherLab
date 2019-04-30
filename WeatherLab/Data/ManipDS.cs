using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    public class ManipDS
    {

        #region Attributs

        StructDS sd;

        #endregion

        #region Constructeur

        public ManipDS(string path)
        {
            sd = new StructDS(path);
        }

        #endregion

        #region Methodes

        // Cette fonction retourne une listes de toutes les données d'une saison donnée de toutes les années
        public List<Observation> getSaison(Saison a)
        {
            return sd.GetObservations().Where(x => x.GetSaison() == a).OrderBy(x=> x.GetDate()).ToList();
        }

        // Cette fonction retourne une listes de toutes les données d'un mois donné de toutes les années
        public List<Observation> getMonth(int i)
        {
            return sd.GetObservations().Where(x => x.GetMonth() == i).OrderBy(x => x.GetDate()).ToList();
        }

        // Cette fonction retourne une liste des données dans un intervalles de jour de toutes les années
        public List<Observation> requetteAIntervalle(int day1, int month1, int day2, int month2)
        {
            if (month1 < month2)
            {
                return sd.GetObservations().Where(x => ((x.GetMonth() > month1 && x.GetMonth() < month2) || (x.GetMonth() == month1 && x.GetDay() >= day1)
                                          || (x.GetMonth() == month2 && x.GetDay() <= day2))).OrderBy(x => x.GetDate()).ToList();
            }
            else if (month1 == month2)
            {
                if (day1 > day2)
                {
                    return sd.GetObservations().Where(x => !(x.GetMonth() == month1 && x.GetDay() > day2 && x.GetDay() < day1))
                        .OrderBy(x => x.GetDate()).ToList();
                }
                else
                {

                    return sd.GetObservations().Where(x => (x.GetMonth() == month1 && x.GetDay() >= day1 && x.GetDay() <= day2))
                        .OrderBy(x => x.GetDate()).ToList();
                }
            }
            else return sd.GetObservations().Where(x => !((x.GetMonth() > month2 && x.GetMonth() < month1) || (x.GetMonth() == month2 && x.GetDay() >= day2)
                                          || (x.GetMonth() == month1 && x.GetDay() <= day1))).OrderBy(x => x.GetDate()).ToList();
        }

        // Cette fonction retourne une liste des données entre deux dates données
        public List<Observation> requetteAIntervalle(int day1, int month1, int year1, int day2, int month2, int year2)
        {
            DateTime date1 = new DateTime(year1, month1, day1);
            DateTime date2 = new DateTime(year2, month2, day2,23,59,59);
            return sd.GetObservations().Where(x => x.GetDate().CompareTo(date1) >= 0 && x.GetDate().CompareTo(date2) <= 0 )
                .OrderBy(x => x.GetDate()).ToList();
        }

        // Cette fonction retourne une liste des données entre deux dates données
        public List<Observation> requetteAIntervalle(DateTime date1,DateTime date2)
        {
            return sd.GetObservations().Where(x => x.GetDate().CompareTo(date1) >= 0 && x.GetDate().CompareTo(date2) <= 0 )
                .OrderBy(x => x.GetDate()).ToList();
        }

        // Cette fonction retourne le nombre d'années qu'on a dans le dataset
        public int getNbYear()
        { 
            return sd.GetObservations().Select(x => x.GetYear()).ToList().Distinct().Count();
        }

        // Cette fonction retourne une liste des années qu'on a dans le dataset
        public List<int> getYears()

        {
            return sd.GetObservations().Select(x => x.GetYear()).Distinct().ToList();
        }

        // Cette fonction retourne une liste des données d'une année donnée
        public List<Observation> getYear(int annee)
        {
            return sd.GetObservations().Where(x => x.GetYear() == annee).ToList();
        }

        // Cette fonction retourne une liste des données d'un jour donné de toutes les années
        public List<Observation> getDay(int day, int month)
        {
            return sd.GetObservations().Where(x => x.GetDay() == day && x.GetMonth() == month).OrderBy(x => x.GetDate()).ToList();
        }

        // Cette fonction retourne une liste des données d'un jour précis 
        //(ps: ca retournera qu'une seule donnée si on modifie le dataset de tel sorte qu'on pose la moyenne des attributs pour chaque jour
        public List<Observation> getDay(int day, int month, int year)
        {
            return sd.GetObservations().Where(x => x.GetDay() == day && x.GetMonth() == month && x.GetYear() == year).ToList();
        }

        #endregion
    }
}
