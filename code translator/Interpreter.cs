using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    internal class Interpreter
    {
        public object interpretExp(Exp e)
        {
            if (e.type == ExpType.NUMBER)
            {
                return (e.number);
            }
            else if (e.type == ExpType.SUM)
            {
                var left = interpretExp(e.exp1);
                var right = interpretExp(e.exp2);
                var nleft = (int)left;
                var nright = (int)right;
                return nleft + nright;
            }
            throw new NotImplementedException();

        }
    }
}
