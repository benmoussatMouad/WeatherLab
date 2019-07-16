using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.Synthése
{
    class ImportData
    {
        public List<double> Data { get; set; }
        public string Parametre { get; set; }

        public ImportData(List<double> Data, string Parametre)
        {
            this.Data = Data;
            this.Parametre = Parametre;
        }

        public ImportData()
        {
            this.Data = new List<double>();
        }
    }
}
