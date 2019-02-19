/******************************************************************
 *    Ecole national Supérieure d'Informatique Alger (ESI)
 *  
 *    Projet 2CPI année 2018/2019
 *    
 *    Projet No 4
 *    Groupe 28
 *    
 ******************************************************************    
 * 
 * classe StructDS ( Structurer DataSet )
 * 
 * fonctionnalités principales :
 *      - Récuperer le DataSet et ses attributs
 *      - trier le DataSet selon un ou plusieurs attributs
 *      - sauvegarder le DataSet
 *      - créer et sauvegarder un index pour fournir un accée directe aux informations
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace WeatherLab.Data
{
    public class StructDS
    {
        private string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (!File.Exists(value))
                    throw new FileNotFoundException();
                else
                    _path = value;
            }
        }
        private List<string> Attrs;
        public List<Donnee> Donnees { get; }

        public StructDS(string path)
        {
            this.Path = path;
            Donnees = new List<Donnee>();
            Attrs = new List<string>();
        }

        /*
         *  la méthode permettant de récuperer le fichier et ses attributs
         *  
         *  qlq améliorations :
         *      - traitement des fichiers de différents formats ( .csv, .xls, json...)
         * 
         */
        public virtual void Load()
        {
            int i = 0;
            string[] lines;
            using (StreamReader f = File.OpenText(Path))

            {
                lines = f.ReadToEnd().Split('\n');
            }
            foreach (string line in lines)
                if(line.Length > 0)
                    if (line[0] != '#')
                    {
                        if (++i == 1)
                        {
                            Attrs = line.Split(';').ToList();
                            Attrs.RemoveAt(0);
                        }
                        else
                            Donnees.Add(new Donnee(line, Attrs.ToArray()));
                    }
                    else
                        Console.WriteLine(line);
            
        }

        /**
         *  la méthode de tri du fichier en utilisant Linq pour fournir un multi-tri rapide
         *  
         *  qlq améliorations :
         *      - trier selon des attributs donnés comme arguments
         *  
         *  PS : pour améliorer le fonctionnement "override" cette méthode dans une autre classe
         **/
        public virtual void Trier()
        {
            Donnees.OrderBy(l => l.GetMonth()).ThenBy(l => l.GetYear()).ThenBy(l => l.GetDay());
        }

        /// <summary>
        /// cette fonction sauvegarde les données dans le fichier et l'index dans
        /// </summary>
        public virtual void sauvegarder(string nom_wilaya, string nom_index)
        {

        }

        /// <summary>
        /// this function is only to use in the console
        /// </summary>
        /// <param name="nb"> le nommbre d'element a afficher </param>
        public void afficherDonnees(int nb)
        {
            for(int i=0;i<nb;i++)
            {
                Donnees[i].afficher();
            }
        }

        public List<string> GetAttrs()
        {
            return Attrs;
        }

        public List<Donnee> GetDonnees()
        {
            return Donnees;
        }

        public void ajouterDonnee(Donnee d)
        {
            Donnees.Add(d);
        }

        public void ajouterDonnee(Donnee[] ds)
        {
            foreach(Donnee d in ds)
            {
                Donnees.Add(d);
            }
        }

        public void renommerAttr(string ancien, string nouveau)
        {
            if (Attribut.attrExists(ancien))
                Attribut.renameAttr(ancien, nouveau);

            for(int i = 0; i < Donnees.Count; i++)
            {
                Donnees[i].renameAttr(ancien, nouveau);
            }
        }

    }
}
