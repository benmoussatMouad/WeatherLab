﻿
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLab.Data;
using System.Windows.Controls;

namespace LiveChart
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
        private String defaultpath = @"C:\Users\acer\Desktop\2CP\Final DataSets";
        private DateTime date1;
        private DateTime date2;
        private string path;
        private ManipDS dataset;
        private List<Observation> periode = new List<Observation>();


        public InputSynthese(DateTime date1, DateTime date2, string path)
        {
            this.date1 = date1;
            this.date2 = date2;
            this.dataset = new ManipDS(path);
            this.periode = dataset.requetteAIntervalle(date1, date2);
        }

        public InputSynthese()
        {
            this.date1 = this.date2 = DateTime.Today;
            path = "";
        }

        public void SetPath (String nomWilaya)
        {
            path = this.defaultpath + @"\" + nomWilaya + ".csv";
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
        private List<double> GetAttribut(string attr, IEnumerable<Observation> periode)
        {
            List<double> listAttr = new List<double>();
            foreach (var x in periode)
            {
                listAttr.Add(x.getDonnee(attr));
            }
            return listAttr;
        }
        //Retourne une liste qui contient la valeur d'un attribut choisi de chaque jour de la periode
        public List<double> GetAttribut(string attr)
        {
            List<double> listAttr = new List<double>();
            foreach (var x in periode)
            {
                listAttr.Add(x.getDonnee(attr));
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
                listAttr.Add(GetAttribut(attr, this.periode.Where(x => x.GetMonth() == w.mois && x.GetYear() == w.annee)).Average());
            }
            return listAttr;
        }
    }
}

