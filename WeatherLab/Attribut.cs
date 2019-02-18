using System;
using System.Collections.Generic;

namespace WeatherLab.Data
{
    class Attribut
    {
        public const short NUMERIC = 0;
        public const short ENUM = 1;
        public const short BOOL = 2;

        private static Dictionary<string, short> attrs = new Dictionary<string, short>();
        private string nom;
        private float val;
        private short type;

        // should generate errors if the name doesn't exist
        public Attribut(string nom, float valeur)
        {
            val = valeur;
            type = attrs[nom];
        }

        // should generate errors if the attr already exist
        public static void addAttr(string nom, short type)
        {
            attrs.Add(nom, type);
        }

        public static bool attrExists(string nom)
        {
            return attrs.ContainsKey(nom);
        }

        public bool isNumeric()
        {
            return type == NUMERIC;
        }

        public bool isEnum()
        {
            return type == ENUM;
        }

        public bool isBool()
        {
            return type == BOOL;
        }

        public string getAttr()
        {
            return nom;
        }

        public float getValeur()
        {
            return val;
        }

        public void setValeur(float val)
        {
            this.val = val;
        }
    }
}
