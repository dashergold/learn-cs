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
            if (e is ConstantExpression c)
            {
                return (c.Value);
            }
            else if (e is Combination combo)
            {
                if (combo.Type == ExpType.SUM)
                {
                    var left = interpretExp(combo.Left);
                    var right = interpretExp(combo.Right);
                    var nleft = (int)left;
                    var nright = (int)right;
                    return nleft + nright;
                }
                else if (combo.Type == ExpType.DIFF)
                {
                    var left = interpretExp(combo.Left);
                    var right = interpretExp(combo.Right);
                    var nleft = (int)left;
                    var nright = (int)right;
                    return nleft - nright;
                }
                else if (combo.Type == ExpType.PROD)
                {
                    var left = interpretExp(combo.Left);
                    var right = interpretExp(combo.Right);
                    var nleft = (int)left;
                    var nright = (int)right;
                    return nleft * nright;
                }
            }
            
            throw new NotImplementedException();

        }
    }
}
