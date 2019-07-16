using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeatherLab.Data;

namespace WeatherLab.Synthése
{
    class InputSynthesePrime : InputSynthese
    {
        private string path2;
        private ManipDS dataset2;
        public List<Observation> periode2 = new List<Observation>();

       /* public InputSynthesePrime () : base()
        {
            path2 = base.defaultpath + @"\ADRAR.csv";
            this.dataset2 = new ManipDS(path2);
            this.periode2 = dataset2.requetteAIntervalle(date1, date2);
        }*/

        public InputSynthesePrime(DateTime date1, DateTime date2 , string path1,string path2) : base(date1,date2,path1)
        {
            this.path2 =path2;
            this.dataset2 = new ManipDS(path2);
            this.periode2 = dataset2.requetteAIntervalle(date1, date2);
        }

        public void SetPath2(String path)
        {
            path2 = path;
            this.dataset2 = new ManipDS(path2);
            this.periode2 = dataset2.requetteAIntervalle(date1, date2);
        }
        

        public List<double> GetAttribut2(string attr)
        {
            List<double> listAttr = new List<double>();
            foreach (var x in periode2)
            {
                listAttr.Add(Math.Round(x.getDonnee(attr), 2));
            }
            return listAttr;
        }

        public void SetDate1(DatePicker date)
        {
            date1 = date.DisplayDate;
            periode = dataset.requetteAIntervalle(date1, date2);
            periode2 = dataset2.requetteAIntervalle(date1, date2);
        }

        public void SetDate2(DatePicker date)
        {
            date2 = date.DisplayDate;
            periode = dataset.requetteAIntervalle(date1, date2);
            periode2 = dataset2.requetteAIntervalle(date1, date2);
        }

        public List<double> GetMoyAttribut2(string attr)
        {
            List<double> listAttr = new List<double>();
            foreach (var w in GetMoisAnnee())
            {
                listAttr.Add(Math.Round(GetAttribut(attr, this.periode2.Where(x => x.GetMonth() == w.mois && x.GetYear() == w.annee)).Average(), 2));
            }
            return listAttr;
        }

    }
}
