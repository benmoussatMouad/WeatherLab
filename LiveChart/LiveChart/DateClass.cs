using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChart
{
    public static class DateClass
    {
        public static DateTime DateRecover(string s)       //Retourne une date a partir d'une date en string
        {
            string[] ListeDate = new string[4];

            ListeDate = s.Split('/');
            ListeDate[2] = ListeDate[2].Substring(0, 4);

            DateTime ADate = new DateTime(Convert.ToInt32(ListeDate.ElementAt(2)), Convert.ToInt32(ListeDate.ElementAt(1)), Convert.ToInt32(ListeDate.ElementAt(0)));
            return ADate;
        }

        public static Boolean DateOK(DateTime date1, DateTime date2)     //Retourne vrai si la date est correcte , faux sinon
        {
            if (date2.Year >= date1.Year)
            {
                if (date2.Month >= date1.Month)
                {
                    if (date2.Day >= date1.Day)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
