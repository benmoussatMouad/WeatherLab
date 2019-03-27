using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    public enum Saison
    {
        hiver, pringtemps, été, automne
        
    }
    
    public class Donnee
    {
        private DateTime date;
        private List<Attribut> attrs;

        public Donnee()
        {
            date = DateTime.Today;
            attrs = new List<Attribut>();
        }

        public Donnee(string data, string[] Attrs)
        {
            // utilisant le format csv par défault
            fromRaw(data, Attrs, "csv");
   
        }

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
                    this.attrs = new List<Attribut>();

                    for (int i = 0; i < Attrs.Length; i++)
                    {
                        float t;
                        if (float.TryParse(datas[i + 1], out t))
                            this.attrs.Add(new Attribut(Attrs[i], t));
                        else if (float.TryParse(datas[i + 1].Replace('.', ','), out t))
                        {
                            this.attrs.Add(new Attribut(Attrs[i], t));
                        }
                        else if(datas[i + 1] == "")
                        {
                            this.attrs.Add(new Attribut(Attrs[i], 0f));
                        }
                        else
                        {
                            //Console.WriteLine(Attrs[i]);
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
                    foreach(Attribut i in attrs)
                    {
                        outp += ";" + i.getValeur();
                    }
                    return outp;
                default:
                    return "";
            }
        }

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

        public float GetAttr(string attr)
        {
            if (Attribut.attrExists(attr))
            {
                foreach (Attribut i in attrs)
                {
                    if (i.getKey() == attr)
                        return i.getValeur();
                }
                return 0;
            }
            else
                throw new ArgumentException();
        }

        public Attribut[] getAttrs()
        {
            return attrs.ToArray();
        }

        public void SetDate(int year, int month, int day)
        {
            date = new DateTime(year, month, day);
        }

        public void SetAttr(string key, float value)
        {
            if (Attribut.attrExists(key))
                foreach (Attribut i in attrs)
                {
                    if (i.getKey() == key)
                    {
                        i.setValeur(value);
                        break;
                    }
                }
            else
                throw new ArgumentException();
        }

        public void afficher()
        {
            Console.WriteLine("Donnees {");
            Console.WriteLine("\tdate : {0}.{1}.{2}", date.Day, date.Month, date.Year);
            foreach (Attribut i in attrs)
            {
                Console.WriteLine("\t{0} : {1},", i.getKey(), i.getValeur());
            }
            Console.WriteLine("}");
        }

        public void renameAttr(string Old, string New)
        {
            for(int i=0;i<attrs.Count;i++)
            {
                if (attrs[i].getKey() == Old)
                {
                    attrs[i].setKey(New);
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

    }
}
