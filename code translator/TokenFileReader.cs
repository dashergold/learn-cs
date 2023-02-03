using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class TokenFileReader
    {
        public TokenFileReader(TextReader rdr)
        {
            this.Dictionary = new Dictionary<string, TokenType>();
            
            var line = rdr.ReadLine();
            while (line != null)
            {
                processLine(line);
                line = rdr.ReadLine();
            }

        }
        public TokenFileReader(string fileContents) : this(new StringReader(fileContents))
        {
        }

        private void processLine(string line)
        {
            if (line.Length== 0)
            {
                return;
            }
            var fields = line.Split(" ");
            if (fields.Length != 2)
            {
                return;
            }
            var tt = Enum.Parse<TokenType>(fields[1]);
            this.Dictionary.Add(fields[0],tt);
        }

        public Dictionary<string, TokenType> Dictionary { get; set; }
    }
}
