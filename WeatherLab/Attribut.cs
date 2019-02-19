using System;
using System.Collections.Generic;

namespace WeatherLab.Data
{
    class Attribut
    {
        public const byte NUMERIC = 0;
        public const byte ENUM = 1;
        public const byte BOOL = 2;

        private static Dictionary<string, byte> attrs = new Dictionary<string, byte>();
        private string nom;
        private float val;
        private byte type;

        // should generate errors if the name doesn't exist
        public Attribut(string nom, float valeur)
        {
            this.nom = nom;
            val = valeur;
            type = attrs[nom];
        }

        // should generate errors if the attr already exist
        public static void addAttr(string nom, byte type)
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
