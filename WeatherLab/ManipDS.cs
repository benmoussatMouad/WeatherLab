using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    public class ManipDS
    {
        StructDS dataset;

        public ManipDS(string path)
        {
            dataset = new StructDS(path);
            dataset.Load();
        }

        // Cette fonction retourne une listes de toutes les données d'une saison donnée de toutes les années
        public List<Donnee> getSaison(Saison a)
        {
            return dataset.Donnees.Where(x => x.GetSaison() == a).ToList();
        }

        // Cette fonction retourne une listes de toutes les données d'un mois donné de toutes les années
        public List<Donnee> getMonth(int i)
        {
            return dataset.Donnees.Where(x => x.GetMonth() == i).ToList();
        }

        // Cette fonction retourne une liste des données dans un intervalles de jour de toutes les années
        public List<Donnee> requetteAIntervalle(int day1, int month1, int day2, int month2)
        {
            if (month1 < month2)
            {
                Console.WriteLine("hnaaaaa");
                return dataset.Donnees.Where(x => ((x.GetMonth() > month1 && x.GetMonth() < month2) || (x.GetMonth() == month1 && x.GetDay() >= day1)
                                          || (x.GetMonth() == month2 && x.GetDay() <= day2))).ToList();
            }
            else if (month1 == month2)
            {
                if (day1 > day2)
                {
                    return dataset.Donnees.Where(x => !(x.GetMonth() == month1 && x.GetDay() > day2 && x.GetDay() < day1)).ToList();
                }
                else
                {

                    return dataset.Donnees.Where(x => (x.GetMonth() == month1 && x.GetDay() >= day1 && x.GetDay() <= day2)).ToList();
                }
            }
            else return dataset.Donnees.Where(x => !((x.GetMonth() > month2 && x.GetMonth() < month1) || (x.GetMonth() == month2 && x.GetDay() >= day2)
                                          || (x.GetMonth() == month1 && x.GetDay() <= day1))).ToList();
        }

        // Cette fonction retourne une liste des données entre deux dates données
        public List<Donnee> requetteAIntervalle(int day1, int month1, int year1, int day2, int month2, int year2)
        {
            DateTime date1 = new DateTime(year1, month1, day1);
            DateTime date2 = new DateTime(year2, month2, day2,23,59,59);
            return dataset.Donnees.Where(x => x.GetDate().CompareTo(date1) >= 0 && x.GetDate().CompareTo(date2) <= 0 ).ToList();
        }

        // Cette fonction retourne le nombre d'années qu'on a dans le dataset
        public int getNbYear()
        { 
            return dataset.Donnees.Select(x => x.GetYear()).ToList().Distinct().Count();
        }

        // Cette fonction retourne une liste des années qu'on a dans le dataset
        public List<int> getYears()

        {
            return dataset.Donnees.Select(x => x.GetYear()).Distinct().ToList();
        }

        // Cette fonction retourne une liste des données d'une année donnée
        public List<Donnee> getYear(int annee)
        {
            return dataset.Donnees.Where(x => x.GetYear() == annee).ToList();
        }

        // Cette fonction retourne une liste des données d'un jour donné de toutes les années
        public List<Donnee> getDay(int day, int month)
        {
            return dataset.Donnees.Where(x => x.GetDay() == day && x.GetMonth() == month).ToList();
        }

        // Cette fonction retourne une liste des données d'un jour précis 
        //(ps: ca retournera qu'une seule donnée si on modifie le dataset de tel sorte qu'on pose la moyenne des attributs pour chaque jour
        public List<Donnee> getDay(int day, int month, int year)
        {
            return dataset.Donnees.Where(x => x.GetDay() == day && x.GetMonth() == month && x.GetYear() == year).ToList();
        }

       
    }
}
