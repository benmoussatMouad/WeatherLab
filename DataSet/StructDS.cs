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
using System.Threading;

namespace WeatherLab.Data
{
    public class StructDS
    {
        #region Attributs

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

        private DataSet dataset;

        #endregion

        #region Constructeur

        /// <summary>
        /// constructeur par défault
        /// </summary>
        /// <Error>
        ///     <Name>FileNotFoundException</Name>
        /// </Error>
        /// <param name="path"></param>
        public StructDS(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            this.Path = path;

            dataset = new DataSet();

            Load("csv");  // par défault
        }

        /// <summary>
        /// donner les données du mois dans path
        /// </summary>
        /// <Error>
        ///     <Name>IndexNotFoundException</Name>
        ///     <Detail>si l'index n'existe pas</Detail>
        /// </Error>
        /// <Error>
        ///     <Name>ArgumentException</Name>
        ///     <Detail>si le mois pas dans [[1, 12]]</Detail>
        /// </Error>
        /// <Error>
        ///     <Name>FileNotFoundException</Name>
        /// </Error>
        /// <param name="mois"> mois entre 1 et 12 </param>
        public StructDS(string path, string index, int nb_wilaya, int mois)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            if (!File.Exists(index))
                throw new IndexNotFoundException();
            if (mois < 1 || mois > 12)
                throw new ArgumentException("mois doit être entre 1 et 12.");

            this.Path = path;
            this.index = index;

            dataset = new DataSet();

            Load(mois, nb_wilaya, "csv");
        }

        #endregion

        #region Methodes

        /// <summary>
        /// récuperer le fichier et ses attributs
        /// </summary>
        /// <Error>
        ///     <Name>NotSupportedException</Name>
        ///     <Detail>la seule format qu'on a maintenant est csv</Detail>
        /// </Error>
        /// <PS>
        /// qlq améliorations :
        ///     - traitement des fichiers de différents formats( .csv, .xls, json...)
        /// </PS>
        private void Load(string format)
        {
            CsvParser parser;
            if (format == "csv" || format == "CSV")
            {
                parser = new CsvParser(Path);
            } else
            {
                throw new NotSupportedException("cette format n'est pas implementé.");
            }

            // get attrs from config here ...
            dataset.modifierAttributs(parser.getAttributs());

            try
            {
                while (true)
                    dataset.ajouterObservation(parser.getObservation());
            } catch(EndOfStreamException e)
            {}

            parser.Dispose();
        }


        /// <summary>
        /// récuperer seulement les données d'une wilaya
        /// </summary>
        /// <Error>
        ///     <Name>IndexNotCompleteException</Name>
        ///     <Detail>si le fichier d'index n'est pas sauvegarder avec la wilaya voulu</Detail>
        /// </Error>
        /// <param name="mois"></param>
        /// <param name="nb_wilaya"></param>
        /// <param name="format"></param>
        private void Load(int mois, int nb_wilaya, string format)
        {
            long[] wilayas;
            Index[] id;
            var writer = new BinaryFormatter();

            /* récuperant les champs nécessaires */
            using (var f = File.OpenRead(index))
            {
                f.Seek(0, SeekOrigin.Begin);
                wilayas = (long[])writer.Deserialize(f);
                if (wilayas[nb_wilaya] == 0)
                    throw new IndexNotCompleteException("le nombre " + nb_wilaya + " n'existe pas dans l'index");
                f.Seek(wilayas[nb_wilaya], SeekOrigin.Begin);
                id = (Index[])writer.Deserialize(f);
            }

            DataParser parser;
            if (format == "csv" || format == "CSV")
            {
                parser = new CsvParser(Path, id[mois - 1].debut, id[mois - 1].fin);
            }
            else
            {
                throw new NotSupportedException("cette format n'est pas implementé.");
            }

            // get attrs from config here ...
            dataset.modifierAttributs(parser.getAttributs());

            try
            {
                while (true)
                    dataset.ajouterObservation(parser.getObservation());
            }
            catch (EndOfStreamException e)
            { }
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
            dataset.observations = dataset.observations.OrderBy(l => l.GetMonth()).ThenBy(l => l.GetYear()).ThenBy(l => l.GetDay()).ToList();
        }

        /// <summary>
        /// cette fonction sauvegarde les données et le fichier d'index
        /// </summary>
        public virtual void sauvegarder(string nom_index, int nb_wilaya)
        {
            Trier();
            using (var ou = File.Create(Path + ".tmp.csv"))
            {
                long[] wilaya = new long[48];
                Index[] id = new Index[12];

                int nblignes = 0;

                var writer = new BinaryFormatter();
                var output = new StreamWriter(ou);
                output.AutoFlush = true;

                string str = "Date;" + String.Join(";", dataset.getAttributs());
                output.WriteLine(str);
                nblignes++;

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

                        writer.Serialize(f, wilaya);
                        wilaya[nb_wilaya] = f.Position;

                        /* remplissage du fichier et de la table d'index */
                        int month = 0, deb = nblignes, fi = 0;
                        for (int j = 0; j < dataset.observations.Count; j++)
                        {
                            if (dataset.observations[j].GetMonth()-1 != month)
                            {
                                fi = nblignes - 1;
                                id[month] = new Index { debut = deb, fin = fi };
                                deb = nblignes;
                                month = dataset.observations[j].GetMonth()-1;
                            }

                            output.WriteLine(dataset.observations[j].toRaw("csv"));
                            nblignes++;
                        }
                        fi = nblignes - 1;
                        id[month] = new Index { debut = deb, fin = fi };

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
                        wilaya = (long[])writer.Deserialize(f);
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
                        int month = 0, deb = nblignes, fi = 0;
                        for (int j = 0; j < dataset.observations.Count; j++)
                        {
                            if (dataset.observations[j].GetMonth() - 1 != month)
                            {
                                fi = nblignes-1;
                                id[month] = new Index { debut = deb, fin = fi };
                                month = dataset.observations[j].GetMonth() - 1;
                                deb = nblignes;
                            }
                            output.WriteLine(dataset.observations[j].toRaw("csv"));
                            nblignes++;
                        }
                        fi = nblignes - 1;
                        id[month] = new Index { debut = deb, fin = fi };

                        writer.Serialize(f, id);
                        f.Seek(0, SeekOrigin.Begin);
                        writer.Serialize(f, wilaya);
                    }
                }
                output.Close();
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
        /// this function is only to use in the console
        /// </summary>
        /// <param name="nb"> le nommbre d'element a afficher </param>
        public void afficherObservations(int nb)
        {
            for(int i=0;i<nb;i++)
            {
                dataset.observations[i].afficher();
            }
        }

        public List<Observation> GetObservations()
        {
            return dataset.observations;
        }

        /// <summary>
        /// ajouter des données dans le fichier
        /// </summary>
        /// <param name="d"></param>
        public void ajouterObservation(Observation d)
        {
            dataset.ajouterObservation(d);
            CsvParser csv = new CsvParser(Path);
            csv.addObservation(d);
        }

        public void ajouterObservations(Observation[] ds)
        {
            CsvParser csv = new CsvParser(Path);
            foreach (Observation d in ds)
            {
                dataset.ajouterObservation(d);
                csv.addObservation(d);
            }
        }

        /// <summary>
        /// renommer attr in file
        /// </summary>
        public void renommerAttr(string ancien, string nouveau)
        {
            CsvParser csv = new CsvParser(Path);
            csv.renommerAttribut(ancien, nouveau);

            string[] attrs = dataset.getAttributs();
            for(int i = 0; i < attrs.Length; i++)
            {
                if(attrs[i] == ancien)
                {
                    attrs[i] = nouveau;
                    break;
                }
            }

            dataset.modifierAttributs(attrs);
        }

        #endregion
    }
}
