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
                var e = new Combination(ExpType.SUM, e1,e2);
                return (e,i2);

            } else if (b.type == TokenType.MINUS)
            {
                ++i1;
                var (e2,i2) = parseExp(tokens, i1);
                var e = new Combination(ExpType.DIFF,e1,e2);
                return (e,i2);
            }
            throw new NotImplementedException();
        }
        public (Exp, int i) parseSimpleExp(List<Token> tokens, int i)
        {
            var a = tokens[i];
            if (a.type == TokenType.NUMBER)
            {
                var e = new ConstantExpression((int)a.value);
                
                ++i;
                return (e, i);


            }
            else if (a.type == TokenType.ID)
            {
                
                var e = new IdExpression((string)a.value);
                ++i;
                return (e, i);
            }
            else if (a.type == TokenType.STRING)
            {
                var e = new ConstantExpression((string)a.value);
                
                ++i;
                return (e, i);
            }
            return (null, i);
        }

    }
    
}
