using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLab.PredictionSystem.Common.WilayaClasses
{
    static class Wilayas
    {

        public static readonly string ADRAR = "ADRAR";
        public static readonly string CHLEF = "CHLEF";
        public static readonly string LAGHOUAT = "LAGHOUAT";
        public static readonly string OUM_EL_BOUAGHI = "OUM EL BOUAGHI";
        public static readonly string BATNA = "BATNA";
        public static readonly string BEJAIA = "BEJAIA";
        public static readonly string BISKRA = "BISKRA";
        public static readonly string BECHAR = "BECHAR";
        public static readonly string BLIDA = "BLIDA";
        public static readonly string BOUIRA = "BOUIRA";
        public static readonly string TAMANRASSAT = "TAMANRASSAT";
        public static readonly string TEBESSA = "TEBESSA";
        public static readonly string TLEMCEN = "TLEMCEN";
        public static readonly string TIARET = "TIARET";
        public static readonly string TIZI_OUZOU = "TIZI OUZOU";
        public static readonly string ALGER = "ALGER";
        public static readonly string DJELFA = "DJELFA";
        public static readonly string JIJEL = "JIJEL";
        public static readonly string SETIF = "SETIF";
        public static readonly string SAIDA = "SAIDA";
        public static readonly string SKIKDA = "SKIKDA";
        public static readonly string SIDI_BEL_ABBES = "SIDI BEL ABBES";
        public static readonly string ANNABA = "ANNABA";
        public static readonly string GUELMA = "GUELMA";
        public static readonly string CONSTANTINE = "CONSTANTINE";
        public static readonly string MEDEA = "MEDEA";
        public static readonly string MOSTAGANEM = "MOSTAGANEM";
        public static readonly string MSILA = "M'SILA";

        public static readonly string MASCARA = "MASCARA";
        public static readonly string OUARGLA = "OUARGLA";
        public static readonly string ORAN = "ORAN";
        public static readonly string EL_BAYADTH = "EL BAYADTH";
        public static readonly string ILLIZI = "ILLIZI";
        public static readonly string BORDJ_BOU_ARRERIDJ = "BORDJ BOU ARRERIDJ";
        public static readonly string BOUMERDES = "BOUMERDES";
        public static readonly string EL_TAREF = "EL TAREF";
        public static readonly string TINDOUF = "TINDOUF";
        public static readonly string TISSEMSILT = "TISSEMSILT";
        public static readonly string EL_OUED = "EL_OUED";
        public static readonly string KHENCHELA = "KHENCHELA";
        public static readonly string SOUK_AHRAS = "SOUK AHRAS";
        public static readonly string TIPAZA = "TIPAZA";
        public static readonly string MILA = "MILA";
        public static readonly string AIN_DEFLA = "AIN DEFLA";
        public static readonly string NAAMA = "NAAMA";
        public static readonly string AIN_TEMOUCHENT = "AIN TEMOUCHENT";
        public static readonly string GHERDAIA = "GHERDAIA";
        public static readonly string RELIZANE = "RELIZANE";

        private static readonly Dictionary<int, string> code_wilaya = new Dictionary<int, string>
        {
            { 1, ADRAR },
            { 2, CHLEF },
            { 3, LAGHOUAT },
            { 4, OUM_EL_BOUAGHI },
            { 5, BATNA },
            { 6, BEJAIA },
            { 7, BISKRA },
            { 8, BECHAR },
            { 9, BLIDA },
            { 10, BOUIRA },
            { 11, TAMANRASSAT },
            { 12, TEBESSA },
            { 13, TLEMCEN },
            { 14, TIARET },
            { 15, TIZI_OUZOU },
            { 16, ALGER },
            { 17, DJELFA },
            { 18, JIJEL },
            { 19, SETIF },
            { 20, SAIDA },
            { 21, SKIKDA },
            { 22, SIDI_BEL_ABBES },
            { 23, ANNABA },
            { 24, GUELMA },
            { 25, CONSTANTINE },
            { 26, MEDEA },
            { 27, MOSTAGANEM },
            { 28, MSILA },
            { 29, MASCARA },
            { 30,OUARGLA },
            { 31, ORAN},
            { 32, EL_BAYADTH },
            { 33,ILLIZI },
            { 34, BORDJ_BOU_ARRERIDJ},
            { 35, BOUMERDES},
            { 36, EL_TAREF },
            { 37,TINDOUF },
            { 38, TISSEMSILT},
            { 39, EL_OUED },
            { 40,KHENCHELA },
            { 41, SOUK_AHRAS},
            { 42,TIPAZA },
            { 43, MILA},
            { 44,AIN_DEFLA },
            { 45, NAAMA},
            { 46, AIN_TEMOUCHENT},
            { 47,GHERDAIA },
            { 48, RELIZANE},


    };

        public static string GetWilayaByCode(int code)
        {
            String wilaya = code_wilaya[code];
            return wilaya;
        }
        public static int GetCodeWilaya(string wilaya)
        {
            int code = code_wilaya.FirstOrDefault(x => x.Value == wilaya).Key;
            return code;
        }

    }
}
