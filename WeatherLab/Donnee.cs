using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Data
{
    public enum Saison { hiver, pringtemps, été, automne };
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
            int year, month, day, hr, min;

            string[] datas = data.Split(';');
            string[] Date = datas[0].Split(' ');
            year = int.Parse(Date[0].Split('.')[2]);
            month = int.Parse(Date[0].Split('.')[1]);
            day = int.Parse(Date[0].Split('.')[0]);
            hr = int.Parse(Date[1].Split(':')[0]);
            min = int.Parse(Date[1].Split(':')[1]);

            date = new DateTime(year, month, day, hr, min, 0);
            this.attrs = new List<Attribut>();
            
            for(int i=0;i<Attrs.Length;i++)
            {
                this.attrs.Add(new Attribut(Attrs[i], float.Parse(datas[i+1])));
                // problem is the frequent passage from the try block to the catch ( cost 2~8ms ) 
                // multiplied by Attrs.Count will be a big deal -_-
                /*
                try
                {
                    this.attrs.Add(Attrs[i], double.Parse(datas[i]));
                } catch(FormatException e)
                {
                    this.attrs.Add(Attrs[i], 0);
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message+e.StackTrace);
                }//*/
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

        public float GetAttr(string attr)
        {
            if (Attribut.attrExists(attr))
            {
                foreach (Attribut i in attrs)
                {
                    if (i.getAttr() == attr)
                        return i.getValeur();
                }
                return 0;
            }
            else
                throw new ArgumentException();
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
                    if (i.getAttr() == key)
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
            foreach (Attribut i in attrs)
            {
                Console.WriteLine("\t{0} : {1},", i.getAttr(), i.getValeur());
            }
            Console.WriteLine("}");
        }

        public Saison GetSaison()
        {
            int i = (int)this.GetMonth();
            switch (i)
            {
                case 1:
                case 2:
                case 3:
                    return (Saison)0;
                case 4:
                case 5:
                case 6:
                    return (Saison)1;
                case 7:
                case 8:
                case 9:
                    return (Saison)2;
                default:
                    return (Saison)3;
            }
        }

    }
}
