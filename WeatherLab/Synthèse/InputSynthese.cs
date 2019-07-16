using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WeatherLab.Data;

namespace WeatherLab.Synthése
{
    public struct moisAnnee
    {
        public int mois { get; set; }
        public int annee { get; set; }

        public moisAnnee(int mois, int annee)
        {
            this.mois = mois;
            this.annee = annee;
        }
    }

    class InputSynthese
    {
        protected String defaultpath = @"C:\Etudes\Final DataSets";
        protected DateTime date1;
        protected DateTime date2;
        private string path;
        protected ManipDS dataset ;
        public List<Observation> periode = new List<Observation>();


       /* public InputSynthese(DateTime date1, DateTime date2, string wilaya)
        {
            this.date1 = date1;
            this.date2 = date2;
            this.path=defaultpath + @"\" + wilaya + ".csv";
            this.dataset = new ManipDS(path);
            this.periode = dataset.requetteAIntervalle(date1, date2);
        }*/

        public InputSynthese(DateTime date1, DateTime date2, string path)
        {
            this.date1 = date1;
            this.date2 = date2;
            this.path = path;
            this.dataset = new ManipDS(path);
            this.periode = dataset.requetteAIntervalle(date1, date2);
        }

        /*public InputSynthese()
        {
            date1 =  date2 = DateTime.Today;
            path = defaultpath+ @"\ADRAR.csv";
            this.dataset = new ManipDS(path);
            this.periode = dataset.requetteAIntervalle(date1, date2);
        }*/

        public void SetPath(String path)
        {
            this.path = path;
            this.dataset = new ManipDS(path);
            this.periode = dataset.requetteAIntervalle(date1, date2);
        }

        public void SetDate1(DatePicker date)
        {
            date1 = date.DisplayDate;
            this.periode = dataset.requetteAIntervalle(date1, date2);
        }

        public void SetDate2(DatePicker date)
        {
            date2 = date.DisplayDate;
            this.periode = dataset.requetteAIntervalle(date1, date2);
        }

        //Retourne vrai si on doit tracer le graphe par mois sinon par date
        public bool ByMonth()
        {
            return periode.Count > 60;
        }

        //Elle sera utilisée pour avoir la liste des moyenne c'est pour cela qu'elle est private
        protected List<double> GetAttribut(string attr, IEnumerable<Observation> periode)
        {
            List<double> listAttr = new List<double>();
            foreach (var x in periode)
            {
                listAttr.Add(Math.Round(x.getDonnee(attr),2));
            }
            return listAttr;
        }
        //Retourne une liste qui contient la valeur d'un attribut choisi de chaque jour de la periode
        public List<double> GetAttribut(string attr)
        {
            List<double> listAttr = new List<double>();
            foreach (var x in periode)
            {
                listAttr.Add(Math.Round(x.getDonnee(attr),2));
            }
            return listAttr;
        }

        //Retourne une liste des mois de la periode choisie
        public List<moisAnnee> GetMoisAnnee()
        {
            var moisAnnees = from x in periode
                             select new moisAnnee(x.GetMonth(), x.GetYear());

            return moisAnnees.Distinct().ToList();
        }

        // retourne une liste qui contient la moyenne de l'attribut de chaque mois  
        public List<double> GetMoyAttribut(string attr)
        {
            List<double> listAttr = new List<double>();
            foreach (var w in GetMoisAnnee())
            {
                listAttr.Add(Math.Round(GetAttribut(attr, this.periode.Where(x => x.GetMonth() == w.mois && x.GetYear() == w.annee)).Average(),2));
            }
            return listAttr;
        }

        // retourne une map de chaques années avec leurs attributs
        public Dictionary<int,List<double>> GetMap(string attr)
        {
            Dictionary<int, List<double>> map = new Dictionary<int, List<double>>();
            for (int i = date1.Year; i < date2.Year+1; i++)
            {
                map.Add(i, dataset.getYear(i).Select(x =>(double) (x.getDonnee(attr))).ToList());
            }
            return map;
        }
    }
}