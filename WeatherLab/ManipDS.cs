using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    class ManipDS
    {
        StructDS dataset;
        public ManipDS(string path)
        {
            dataset = new StructDS(path);
            dataset.Load();
        }

        public List<Donnee> getSaison(Saison a)
        {
            return dataset.Donnees.Where(x => x.GetSaison() == a).ToList();
        }

        public Dictionary<DateTime,Attribut> getSaison(Saison a, Attribut attr)
        {
              var result = from x in dataset.Donnees
                           where (x.GetSaison() == a)
                           select new
                           {
                               date = new DateTime(x.GetYear(), x.GetMonth(), x.GetDay()),
                               attribute = attr
                           };
        }

        public List<Donnee> getMonth(int i)
        {
            return dataset.Donnees.Where(x => x.GetMonth() == i).ToList();
        }

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

        public int getNbYear()
        {
            return dataset.Donnees.Select(x => x.GetYear()).ToList().Distinct().Count();
        }

        public List<int> getYears()

        {
            return dataset.Donnees.Select(x => x.GetYear()).Distinct().ToList();
        }

        public List<Donnee> getAnnees(int annee)
        {
            return dataset.Donnees.Where(x => x.GetYear() == annee).ToList();
        }

        public List<Donnee> getDay(int day, int month)
        {
            return dataset.Donnees.Where(x => x.GetDay() == day && x.GetMonth() == month).ToList();
        }

        public List<Donnee> getDay(int day, int month, int year)
        {
            return dataset.Donnees.Where(x => x.GetDay() == day && x.GetMonth() == month && x.GetYear() == year).ToList();
        }

        static void Main(string[] args)
        {
            ManipDS haya = new ManipDS(@"C:\Etudes\Projet2CP\DataSets_modifiés\ALGER.csv");
           
           
        } 
    }
}
