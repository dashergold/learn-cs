﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class Parser
    {
        private List<Token> tokens;
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        #region expressions
        //pars expressions 
        //E:: == const |
        //  id |
        //  string |
        //  (E)
        //  E+E
        //  E*E
        public (Exp, int i) parseExp( int i)
        {
            
            var (e1,i1) = parseApplication( i);
            if (i1 == tokens.Count)
            {
                return (e1,i1);
            }
            var b = tokens[i1];
            if (b.type == TokenType.PLUS)
            {
                ++i1;
                var (e2, i2) = parseApplication( i1);
                var e = new Combination(ExpType.SUM, e1,e2);
                return (e,i2);

            } else if (b.type == TokenType.MINUS)
            {
                ++i1;
                var (e2,i2) = parseApplication( i1);
                var e = new Combination(ExpType.DIFF,e1,e2);
                return (e,i2);
            }
            else if (b.type == TokenType.MULT) {
                ++i1;
                var (e2,i2) = parseApplication( i1);
                var e = new Combination(ExpType.PROD, e1,e2);
                return (e, i2);
            }
            else if (b.type == TokenType.RPAREN)
            {
                return (e1, i1);
            }
            else if (b.type == TokenType.EQ)
            {
                return (e1, i1);
            }
            throw new NotImplementedException();
        }
        public (Exp, int i) parseApplication( int i)
        {
            var (e1, i1) = parseSimpleExp( i);
            if (i1 == tokens.Count)
            {
                return (e1, i1);
            }
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
                    var (eArg1, i2) = parseExp( i1);
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
            return (e1, i1);
        }


        public (Exp, int i) parseSimpleExp( int i)
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
        #endregion
        #region statemenets
        public (Statement, int i) parseStatement( int i)
        {
            //statement ::= 
            //    'print' '(' Exp ')'
            //    'if' Exp '{' CompoundStatement '}'
            //    'if' Exp '{' CompoundStatement '}' 'else' '{' CompoundStatement '}'
            //    'while' Exp '{' Compound statement '}'
            


            if (tokens.Count <= i)
            {
                return (null, i);
            }
            var t = tokens[i];
            if ((t.type == TokenType.PRINT))
            {
                ++i;
                var (_,i2) = ExpectToken(TokenType.LPAREN,  i);
                var (e,i3) = parseExp( i2);
                var (_, i4) = ExpectToken(TokenType.RPAREN,  i3);
                var p = new PrintStatement(e);
                return (p, i4);
            }
            else if ((t.type == TokenType.ID))
            {
                var (left, i1) = parseExp(i);
                var (_,i2) = ExpectToken(TokenType.EQ, i1);
                var (right, i3) = parseExp(i2);
                var a = new Assign(left, right);
                return (a, i3);

            }
            else
            {
                throw new NotImplementedException();
            }
            
        }
        #endregion
        private (Token, int i) ExpectToken(TokenType type,  int i)
        {
            if (tokens.Count <= i)
            {
                reportError($"expected {type}");
            }
            var t = tokens[i];
            ++i;
            if ((t.type != type))
            {
                reportError($"expected {type}, but found {t.type}");
               
            }

            return (t, i);
        }

        private void reportError(string v)
        {
            Console.WriteLine(v);
            throw new ApplicationException(v);
        }

    }

}
