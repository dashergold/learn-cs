using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            
            var (e1,i1) = parseApplication(tokens, i);
            if (i1 == tokens.Count)
            {
                return (e1,i1);
            }
            var b = tokens[i1];
            if (b.type == TokenType.PLUS)
            {
                ++i1;
                var (e2, i2) = parseApplication(tokens, i1);
                var e = new Combination(ExpType.SUM, e1,e2);
                return (e,i2);

            } else if (b.type == TokenType.MINUS)
            {
                ++i1;
                var (e2,i2) = parseApplication(tokens, i1);
                var e = new Combination(ExpType.DIFF,e1,e2);
                return (e,i2);
            }
            throw new NotImplementedException();
        }
        public (Exp, int i) parseApplication(List<Token> tokens, int i)
        {
            var (e1, i1) = parseSimpleExp(tokens, i);
            if (tokens[i1].type == TokenType.LPAREN)
            {
                ++i1;
                var args = new List<Exp>();
                if (tokens[i1].type  == TokenType.RPAREN)
                {
                    ++i1;
                    var result = new ApplicationExpression(e1,args);
                    return (result,i1);

                }
                
                while (true)
                {
                    var (eArg1, i2) = parseExp(tokens, i1);
                    args.Add(eArg1);
                    if (tokens[i1].type == TokenType.RPAREN)
                    {
                        i1 = i2 + 1;

                        var result = new ApplicationExpression(e1, args);
                        return (result, i1);

                    }
                    else if (tokens[i1].type == TokenType.COMMA)
                    {
                        
                        i1 = i2 + 1;
                    }
                    else
                    {
                        reportError("expected comma or rigth parenthesis");
                    }
                }
                

            }
            throw new NotImplementedException();
        }

        private void reportError(string v)
        {
            Console.WriteLine(v);
            throw new ApplicationException(v);
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
