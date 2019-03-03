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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace WeatherLab.Data
{
    public class StructDS
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        private struct Index
        {
            public int debut;
            public int fin;
        }

        private string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        private string _index;
        public string index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        private List<string> Attrs;
        public List<Donnee> Donnees { get; }

        public StructDS(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            this.Path = path;
            Donnees = new List<Donnee>();
            Attrs = new List<string>();

            Load();
        }

        /// <summary>
        /// donner les données du mois dans path
        /// </summary>
        /// <PS>
        /// si l'index n'existe pas Exception IndexNotFoundException
        /// si le mois par dans l'intervalle 1..12 Exception ArgumentException
        /// si le path n'existe pas Exception FileNotFoundException
        /// </PS>
        /// <param name="mois"> mois entre 1 et 12 </param>
        public StructDS(string path, string index, int nb_wilaya, int mois)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            if (!File.Exists(path))
                throw new IndexNotFoundException();
            if (mois < 1 || mois > 12)
                throw new ArgumentException("mois doit être entre 1 et 12.");

            this.Path = path;
            this.index = index;

            Load(mois, nb_wilaya);
        }

        /// <summary>
        /// la méthode permettant de récuperer le fichier et ses attributs
        /// 
        ///  qlq améliorations :
        ///     - traitement des fichiers de différents formats( .csv, .xls, json...)
        /// </summary>
        /// <PS>
        /// si les attributs ne sont pas dans Attribut.dict Exception AttributFormatException
        /// </PS>
        private void Load()
        {
            int i = 0;
            string[] lines;

            /* reading all lines from file */
            using (StreamReader f = File.OpenText(Path))

            {
                lines = f.ReadToEnd().Split('\n');
            }

            foreach (string line in lines)
                if (line.Length > 0)
                {
                    if (line[0] != '#')
                        continue;
                    if (++i == 1)
                    {
                        // check if attrs are in Attribut.dict

                        Attrs = line.Split(';').ToList();
                        Attrs.RemoveAt(0);

                        foreach (string attr in Attrs)
                        {
                            if (!Attribut.attrExists(attr))
                                throw new AttributFormatException();
                        }
                    }
                    else
                        Donnees.Add(new Donnee(line, Attrs.ToArray()));
                
                }
            
        }
        
        //By Mouad Benmoussat :***************************************
        //A la fin de Load(), Donnes et Attrs seront plein c'est ça ?
        //*********************************************************

        /// <summary>
        /// rétourne les données du mois dans Donnees
        /// </summary>
        /// <PS>
        /// si la wilaya n'existe pas dans l'index Exception IndexNotCompleteException
        /// si les Attributs ne sont pas dans Attribut.dict Exception AttributFormatException
        /// </PS>
        /// <param name="mois">le mois qu'on veut récuperer</param>
        private void Load(int mois, int nb_wilaya)
        {
            List<long> wilayas = new List<long>();
            List<Index> id = new List<Index>();
            var writer = new BinaryFormatter();

            /* récuperant les champs nécessaires */
            using (var f = File.OpenRead(index))
            {
                f.Seek(0, SeekOrigin.Begin);
                wilayas = (List<long>)writer.Deserialize(f);
                if (wilayas[nb_wilaya] == 0)
                    throw new IndexNotCompleteException("le nombre "+nb_wilaya+" n'existe pas dans l'index");
                f.Seek(wilayas[nb_wilaya], SeekOrigin.Begin);
                id = (List < Index >) writer.Deserialize(f);
            }

            int count = id[mois].fin - id[mois].debut;
            byte[] chars = new byte[count];

            /* reading month data only */
            using (var f = File.OpenRead(Path))
            {
                f.Seek(id[mois].debut, SeekOrigin.Begin);
                f.Read(chars, 0, count);
            }

            Encoding enc = Encoding.UTF8;
            string data = enc.GetString(chars);
            string[] lines = data.Split('\n');

            int i = 0;
            foreach(string line in lines)
            {
                if (line.Length > 0)
                {
                    if (line[0] == '#')
                        continue;
                    if (i++ == 0)
                    {
                        Attrs = line.Split(';').ToList();
                        Attrs.RemoveAt(0);
                        foreach (string attr in Attrs)
                        {
                            if (!Attribut.attrExists(attr))
                                throw new AttributFormatException("l'attribut " + attr + " (" + i + ") n'existe pas dans Attribut");
                        }
                    }
                    else
                        Donnees.Add(new Donnee(line, Attrs.ToArray()));
                }
            }
        }

        /**
         *  la méthode de tri du fichier en utilisant Linq pour fournir un multi-tri rapide
         *  
         *  qlq améliorations :
         *      - trier selon des attributs donnés comme arguments
         *  
         *  PS : pour améliorer le fonctionnement "override" cette méthode dans une autre classe
         **/
        protected virtual void Trier()
        {
            Donnees.OrderBy(l => l.GetMonth()).ThenBy(l => l.GetYear()).ThenBy(l => l.GetDay());
        }

        /// <summary>
        /// cette fonction sauvegarde les données et le fichier d'index
        /// </summary>
        public virtual void sauvegarder(string nom_index, int nb_wilaya)
        {
            Trier();
            using (var ou = File.Create(Path + ".tmp.csv"))
            {
                List<long> wilaya = new List<long>();
                List<Index> id = new List<Index>();

                var writer = new BinaryFormatter();

                string str = "Date;" + String.Join(";", Attrs.ToArray());
                writer.Serialize(ou, str);

                // si le fichier d'index n'existe pas on crée ce fichier avec une entête de 48 wilaya
                if (!File.Exists(nom_index))
                {
                    using (var f = File.Create(nom_index))
                    {
                        /* initialisation et écriture de l'entête */
                        for (int j = 0; j < 48; j++)
                        {
                            wilaya[j] = 0;
                        }
                        /*
                        for (int j = 0; j < 12; j++)
                        {
                            i.Add(new Index { debut = 0, fin = 0 });
                        }//*/

                        writer.Serialize(f, wilaya);
                        wilaya[nb_wilaya] = f.Position;

                        /* remplissage du fichier et de la table d'index */
                        int month = 0, debut = 0, fin = 0;
                        for (int j = 0; j < Donnees.Count; j++)
                        {
                            if (Donnees[j].GetMonth() != month)
                            {
                                if(month != 0)
                                {
                                    fin = (int)ou.Position - 1;
                                    id[Donnees[j].GetMonth()] = new Index { debut = debut, fin = fin };
                                }
                                debut = (int)ou.Position;
                            }
                            writer.Serialize(ou, Donnees[j].toRaw("csv"));
                        }
                        
                        /* écriture du fichier d'index avec tout les informations */
                        writer.Serialize(f, id);
                        f.Seek(0, SeekOrigin.Begin);
                        writer.Serialize(f, wilaya);
                    }
                }
                else
                {
                    /* récuperant l'entête de l'index */
                    using (var f = File.OpenRead(nom_index))
                    {
                        f.Seek(0, SeekOrigin.Begin);
                        wilaya = (List<long>)writer.Deserialize(f);
                    }
                    using (var f = File.OpenWrite(nom_index))
                    {
                        if (wilaya[nb_wilaya] == 0)
                        {
                            f.Seek(0, SeekOrigin.End);
                            wilaya[nb_wilaya] = f.Position;
                        }
                        else
                        {
                            f.Seek(wilaya[nb_wilaya], SeekOrigin.Begin);
                        }

                        /* remplissage du fichier et de la table d'index */
                        int month = 0, debut = 0, fin = 0;
                        for (int j = 0; j < Donnees.Count; j++)
                        {
                            if (Donnees[j].GetMonth() != month)
                            {
                                if (month != 0)
                                {
                                    fin = (int)ou.Position - 1;
                                    id[Donnees[j].GetMonth()] = new Index { debut = debut, fin = fin };
                                }
                                debut = (int)ou.Position;
                            }
                            writer.Serialize(ou, Donnees[j].toRaw("csv"));
                        }

                        writer.Serialize(f, id);
                        f.Seek(0, SeekOrigin.Begin);
                        writer.Serialize(f, wilaya);
                    }
                }
            }
        }

        public static void sauvegarder(string[] paths, string nom_index, int[] nb_wilaya)
        {
            if (paths.Length != nb_wilaya.Length)
                throw new ArgumentException("la longueure des deux tableaux ne sont pas les mêmes.");
            for(int i=0;i<paths.Length;i++)
            {
                StructDS ds = new StructDS(paths[i]);
                ds.Trier();
                ds.sauvegarder(nom_index, nb_wilaya[i]);
            }
        }



        /// <summary>
        /// this hfunction is only to use in te console
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

        /// <summary>
        /// ajouter des données dans le fichier
        /// </summary>
        /// <param name="d"></param>
        public void ajouterDonnee(Donnee d)
        {
            Donnees.Add(d);
            using (var f = File.OpenWrite(Path))
            {
                new BinaryFormatter().Serialize(f, d.toRaw("csv"));
            }
        }

        public void ajouterDonnee(Donnee[] ds)
        {
            using (var f = File.OpenWrite(Path))
            {
                var writer = new BinaryFormatter();
                foreach (Donnee d in ds)
                {
                    Donnees.Add(d);
                    writer.Serialize(f, d.toRaw("csv"));
                }
            }
        }

        /// <summary>
        /// renommer attr in file
        /// </summary>
        /// <param name="ancien"></param>
        /// <param name="nouveau"></param>
        public void renommerAttr(string ancien, string nouveau)
        {
            string text = string.Empty;
            using(var f = new StreamReader(Path))
            {
                string line = f.ReadLine();
                while(line[0] == '#')
                {
                    text += line + "\n";
                    line = f.ReadLine();
                }
                line = Regex.Replace(line, ancien, nouveau);
                text += line + "\n";
                text += f.ReadToEnd();
            }
            using(var f = File.CreateText(Path + ".tmp"))
            {
                f.Write(text);
            }
            File.Delete(Path);
            File.Move(Path + ".tmp", Path);
        }

    }
}
