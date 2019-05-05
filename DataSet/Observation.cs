using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    public enum Saison { hiver, pringtemps, été, automne };
    public class Observation
    {
        #region attributs

        private DateTime date;
        private List<Donnee> donnees;

        #endregion

        #region Constructeur

        public Observation()
        {
            date = DateTime.Today;
            donnees = new List<Donnee>();
        }

        public Observation(DateTime date)
        {
            this.date = date;
            donnees = new List<Donnee>();
        }

        public Observation(DateTime date, string[] attributs, float[] valeurs)
        {
            if (attributs.Length != valeurs.Length)
                throw new ArgumentException("Observation Constr : les deux tableau ne sont pas de même longueure.");

            this.date = date;
            this.donnees = new List<Donnee>();

            for (int i = 0; i < attributs.Length; i++)
            {
                donnees.Add(new Donnee(attributs[i], valeurs[i]));
            }
        }

        public Observation(DateTime date, string[] attributs, string[] valeurs)
        {
            if (attributs.Length != valeurs.Length)
                throw new ArgumentException("Observation Constr : les deux tableau ne sont pas de même longueure.");

            this.date = date;
            this.donnees = new List<Donnee>();

            for (int i = 0; i < attributs.Length; i++)
            {
                float f;
                if (float.TryParse(valeurs[i], out f))
                    donnees.Add(new Donnee(attributs[i], f));
                else if (float.TryParse(valeurs[i].Replace('.', ','), out f))
                    donnees.Add(new Donnee(attributs[i], f));
                else
                    throw new FormatException(string.Format("une valeur de l'attribut {0} n'est pas valide.", attributs[i]));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// load data with the specified format
        /// </summary>
        /// <param name="format">can be csv, xml, json... PS : we only support csv for now</param>
        public void fromRaw(string data, string[] Attrs, string format)
        {
            switch (format)
            {
                case "csv":
                case "CSV":
                    int year, month, day;

                    string[] datas = data.Split(';');
                    string[] Date = datas[0].Split(' ');

                    year = int.Parse(Date[0].Split('.')[2]);
                    month = int.Parse(Date[0].Split('.')[1]);
                    day = int.Parse(Date[0].Split('.')[0]);

                    date = new DateTime(year, month, day, 0, 0, 0);
                    this.donnees = new List<Donnee>();

                    for (int i = 0; i < Attrs.Length; i++)
                    {
                        float t;
                        if (float.TryParse(datas[i + 1], out t))
                            this.donnees.Add(new Donnee(Attrs[i], t));
                        else if (float.TryParse(datas[i + 1].Replace('.', ','), out t))
                        {
                            this.donnees.Add(new Donnee(Attrs[i], t));
                        }
                        else if(datas[i + 1] == "")
                        {
                            this.donnees.Add(new Donnee(Attrs[i], 0f));
                        }
                        else
                        {
                            throw new FormatException("Type not a float: "+datas[i + 1]);
                        }
                    }
                    break;
            }
        }

        public string toRaw(string format)
        {
            switch(format)
            {
                case "csv":
                case "CSV":
                    string outp = "";
                    outp += date.Day+"."+date.Month+"."+date.Year;
                    //Console.WriteLine(attrs.Count);
                    foreach(Donnee i in donnees)
                    {
                        outp += ";" + i.getValeur();
                    }
                    return outp;
                default:
                    return "";
            }
        }

        public void afficher()
        {
            Console.WriteLine("Donnees {");
            Console.WriteLine("\tdate : {0}.{1}.{2}", date.Day, date.Month, date.Year);
            foreach (Donnee i in donnees)
            {
                Console.WriteLine("\t{0} : {1},", i.getAttribut(), i.getValeur());
            }
            Console.WriteLine("}");
        }

        #endregion

        #region Setters_Getters

        public int GetYear()
        {
            return date.Year;
        }

        public int GetMonth()
        {
            return date.Month;
        }

        public int GetDay()
        {
            return date.Day;
        }

        public DateTime GetDate()
        {
            return date;
        }

        public void SetDate(int year, int month, int day)
        {
            date = new DateTime(year, month, day);
        }

        public float getDonnee(string attr)
        {
            foreach (Donnee i in donnees)
            {
                if (i.getAttribut() == attr)
                    return i.getValeur();
            }
            throw new ArgumentException(String.Format("L'attribut {0} n'existe pas dans l'observation.", attr));
        }

        public int nbDonnees()
        {
            return donnees.Count;
        }

        public Donnee[] getDonnees()
        {
            return donnees.ToArray();
        }

        public void SetDonnee(string key, float value)
        {
            foreach (Donnee i in donnees)
            {
                if (i.getAttribut() == key)
                {
                    i.setValeur(value);
                    break;
                }
            }
            throw new ArgumentException(String.Format("L'attribut {0} n'existe pas dans l'observation.", key));
        }

        public void renameAttr(string Old, string New)
        {
            for (int i = 0; i < donnees.Count; i++)
            {
                if (donnees[i].getAttribut() == Old)
                {
                    donnees[i].setAttribut(New);
                    break;
                }
            }
        }

        public Saison GetSaison()
        {
            int i = this.GetMonth();
            switch (i)
            {
                case 1:
                case 2:
                case 3:
                    return Saison.hiver;
                case 4:
                case 5:
                case 6:
                    return Saison.pringtemps;
                case 7:
                case 8:
                case 9:
                    return Saison.été;
                default:
                    return Saison.automne;
            }
        }

        #endregion

    }
}
