using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChart
{
    class DataGraphClass
    {
        public ChartValues<double> Values { get; set; }
        public Func<double, string> Formatter { get; set; }
        public DataGraphClass DataContext { get; set; }

        public DataGraphClass()
        { }
        public DataGraphClass(string parametre, ChartValues<double> Valeurs)
        {
            if (parametre == "Température")
            {
                Values = Valeurs;
                Formatter = Values => Values + "°";
                DataContext = this;
            }
            else
            {
                if (parametre == "Humidité")
                {
                    Values = Valeurs;
                    Formatter = Values => Values + "%";
                    DataContext = this;
                }
                else
                {
                    if (parametre == "Vitesse du vent")
                    {
                        Values = Valeurs;
                        Formatter = Values => Values + "km/h";
                        DataContext = this;
                    }
                    else
                    {
                        if (parametre == "Précipitation")
                        {
                            Values = Valeurs;
                            Formatter = Values => Values + "mm";
                            DataContext = this;
                        }
                        else
                        {
                            if (parametre == "Direction du vent")
                            {
                                Values = Valeurs;
                                Formatter = Values => Values + "°";
                                DataContext = this;
                            }
                            else
                            {
                                if (parametre == "Nuage")
                                {
                                    Values = Valeurs;
                                    Formatter = Values => Values + "%";
                                    DataContext = this;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
