using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigUtils
{
    class Wilaya
    {
        public string path { get; set; }
        public int code { get; set; }
        public Dictionary<string, string> attrs { get; set; }

        public Wilaya()
        {
            attrs = new Dictionary<string, string>();
        }

        public Wilaya(string path, int code, Dictionary<string, string> attrs)
        {
            this.path = path;
            this.attrs = attrs;
            this.code = code;
        }
    }
}
