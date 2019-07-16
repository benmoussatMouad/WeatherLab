using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WeatherLab.Data
{
    public class CsvParser : DataParser, IDisposable
    {

        #region Attributs

        private StreamReader reader;

        private string[] attrs;

        private char delimiter = ';';

        private int fin = -1;

        private int linesRead = 0;

        #endregion

        #region Constructeurs

        public CsvParser(string path) : base(path)
        {
            reader = new StreamReader(path);

            attrs = getAttributs();
        }

        public CsvParser(string path, char delim) : base(path)
        {
            reader = new StreamReader(path);

            delimiter = delim;

            attrs = getAttributs();

        }

        public CsvParser(string path, int lineDebut, int lineFin) : base(path)
        {
            reader = new StreamReader(path);

            attrs = getAttributs();

            this.fin = lineFin;
            for (int i = 0; i < lineDebut-linesRead; i++)
            {
                reader.ReadLine();
            }
            
        }

        public CsvParser(string path, int lineDebut, int lineFin, char delim) : base(path)
        {
            reader = new StreamReader(path);

            attrs = getAttributs();

            delimiter = delim;

            this.fin = lineFin;
            for (int i = 0; i < lineDebut - linesRead; i++)
            {
                reader.ReadLine();
            }
            
        }

        ~CsvParser()
        {

            Dispose(false);
        }

        #endregion

        #region Methodes

        public override void addObservation(DateTime d, float[] valeurs)
        {
            if (valeurs.Length != attrs.Length)
                throw new ArgumentException("Les valeurs ne correspond pas au attributs existant.");

            Observation o = new Observation(d, attrs, valeurs);

            addObservation(o);
        }

        public override void addObservation(Observation observation)
        {
            reader.Dispose();


            using (var writer = new StreamWriter(getPath()))
                writer.WriteLine(observation.toRaw("csv"));

            reader = new StreamReader(getPath());
            for (int i = 0; i < linesRead; i++)
            {
                reader.ReadLine();
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposable)
        {
            if (reader != null)
                reader.Dispose();
        }

        public override string[] getAttributs()
        {
            if(attrs == null)
            {
                string line;
                while (true)
                {
                    line = reader.ReadLine();
                    linesRead++;
                    if (!line.Contains("#") && line.Trim('\n', '\r') != "")
                        break;
                }

                List<string> attr = line.Trim('\n', '\r').Split(delimiter).ToList();
                attr.RemoveAt(0);
                return attr.ToArray();
            } else
            {
                return attrs;
            }
        }

        public override int getObservation(out string[] attributs, out string[] valeurs)
        {
            string line;
            while (true)
            {
                if (reader.EndOfStream || (this.fin != -1 && linesRead > this.fin))
                    throw new EndOfStreamException();
                line = reader.ReadLine();
                linesRead++;
                if (!line.Contains("#") && line.Trim('\n', '\r') != "")
                    break;
            }

            attributs = attrs;
            valeurs = line.Trim('\n', '\r').Split(delimiter);
            return valeurs.Length;
        }

        public override Observation getObservation()
        {
            string[] attributs;
            string[] valeurs;
            
            try
            {
                getObservation(out attributs, out valeurs);
            }
            catch (EndOfStreamException)
            {
                reader.Dispose();
                return null;
            }

            string date = valeurs[0];

            List<string> tmp = valeurs.ToList();
            tmp.RemoveAt(0);
            valeurs = tmp.ToArray();

            string[] arr = date.Split('.', ':', '/');

            DateTime d = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
            return new Observation(d, attributs, valeurs);
            
        }

        public override Observation[] getObservations()
        {
            List<Observation> obs = new List<Observation>();

            try
            {
                while(true)
                    obs.Add(getObservation());
            } catch(EndOfStreamException) {
                reader.Dispose();
            }

            return obs.ToArray();
        }

        public override void renommerAttribut(string ancien, string nouveau)
        {
            reader.Close();

            string text = string.Empty;
            using (var f = new StreamReader(getPath(), true))
            {
                string line = f.ReadLine().Trim('\n', '\r');
                while (line[0] == '#')
                {
                    text += line + "\n";
                    line = f.ReadLine();
                }
                line = Regex.Replace(line, ancien, nouveau);
                text += line + "\r\n";
                text += f.ReadToEnd();
            }
            using (var f = File.CreateText(getPath() + ".tmp"))
            {
                f.Write(text);
            }
            File.Delete(getPath());
            File.Move(getPath() + ".tmp", getPath());

            reader = new StreamReader(getPath());
            for(int i = 0; i < linesRead; i++)
            {
                reader.ReadLine();
            }
        }

        #endregion
    }
}
