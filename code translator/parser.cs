using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    internal class Parser
    {
        //pars expressions 
        //E:: == const |
        //  id |
        //  string |
        //  (E)
        //  E+E
        //  E*E
        public (Exp, int i) parseExp(List<Token> tokens, int i)
        {
            
            var (e1,i1) = parseSimpleExp(tokens, i);
            if (i1 == tokens.Count)
            {
                return (e1,i1);
            }
            var b = tokens[i1];
            if (b.type == TokenType.PLUS)
            {
                ++i1;
                var (e2, i2) = parseExp(tokens, i1);
                var e = new Exp();
                e.type = ExpType.SUM;
                e.exp1 = e1;
                e.exp2 = e2;
                return (e,i2);

            } else if (b.type == TokenType.MINUS)
            {
                ++i1;
                var (e2,i2) = parseExp(tokens, i1);
                var e = new Exp();
                e.type = ExpType.DIFF;
                e.exp1 = e1;
                e.exp2 = e2;
                return (e,i2);
            }
            throw new NotImplementedException();
        }
        public (Exp, int i) parseSimpleExp(List<Token> tokens, int i)
        {
            var a = tokens[i];
            if (a.type == TokenType.NUMBER)
            {
                var e = new Exp();
                e.number = (int)a.value;
                e.type = ExpType.NUMBER;
                ++i;
                return (e, i);


            }
            else if (a.type == TokenType.ID)
            {
                var e = new Exp();
                e.idName = (string)a.value;
                e.type = ExpType.ID;
                ++i;
                return (e, i);
            }
            else if (a.type == TokenType.STRING)
            {
                var e = new Exp();
                e.str = (string)a.value;
                e.type = ExpType.STRING;
                ++i;
                return (e, i);
            }
            return (null, i);
        }

    }
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
            }else if (type == ExpType.ID)
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
        NUMBER =1,   
        ID,
        STRING,
        EXP,
        SUM,
        PROD,
        DIFF,
    }
}
