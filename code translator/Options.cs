using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class Options
    {
        public string FileName;
        public string Language;
        public Options(string[] args)
        {
            if (args.Length == 1)
            {
                FileName = args[0];
            }
            else if (args.Length >= 3)
            {
                FileName = args[2];
                Language = args[1];
            }
            else
            {
                throw new ArgumentException("Invalid arguments");
            }
        }
    }
}
