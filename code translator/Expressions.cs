using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class Exp
    {
        public ExpType type;
        public string idName;
        public int number;
        public string str;
        public Exp exp1, exp2;

        public override string ToString()
        {

            string s = type.ToString();
            if (type == ExpType.NUMBER)
            {
                s += $" ({number}) ";
            }
            else if (type == ExpType.ID)
            {
                s += $" ({idName}) ";
            }
            else if (type == ExpType.STRING)
            {
                s += $" ({str}) ";
            }
            else if (type == ExpType.SUM)
            {
                s += $" ({exp1} + {exp2}) ";
            }
            else if (type == ExpType.DIFF)
            {
                s += $" ({exp1} - {exp2} ";
            }
            return s.ToString();

        }

    }
    public enum ExpType
    {
        NUMBER = 1,
        ID,
        STRING,
        EXP,
        SUM,
        PROD,
        DIFF,
    }
    public class IdExpression : Exp
    {
        public string Name;
    }
    public class ApplicationExpression : Exp
    {
        public Exp Function;
        public List<Exp> Arguments;

    }
    public class Combination : Exp
    {
        public ExpType Type;
        public Exp Left;
        public Exp Right;

    }
    public class ConstantExpression : Exp
    {
        public object Value;

    }
}
