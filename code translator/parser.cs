using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class Parser
    {
        private List<Token> tokens;
        private int position;
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            position = 0;
        }


        public Statement parseProgram()
        {
            
            var statements = new List<Statement>();
            while (this.position < tokens.Count)
            {
                var statement = parseStatement();
                if (statement == null) 
                {
                    break;
                }
                statements.Add(statement);
                
                

            }
            return new CompoundStatement(statements);
        }

        #region expressions
        //pars expressions 
        //E:: == const |
        //  id |
        //  string |
        //  (E)
        //  E+E
        //  E*E
        public Exp parseExp( )
        {
            
            var e1 = parseApplication( );
            if (this.position== tokens.Count)

            {
                return e1;
            }
            var b = tokens[this.position];
            if (b.type == TokenType.PLUS)
            {
                ++this.position;
                var e2 = parseApplication();
                var e = new Combination(ExpType.SUM, e1,e2);
                return e;

            } else if (b.type == TokenType.MINUS)
            {
                ++this.position;
                var e2 = parseApplication();
                var e = new Combination(ExpType.DIFF,e1,e2);
                return e;
            }
            else if (b.type == TokenType.MULT) {
                ++this.position;
                var e2 = parseApplication( );
                var e = new Combination(ExpType.PROD, e1,e2);
                return e;
            }
            else if (b.type == TokenType.RPAREN)
            {
                return e1;
            }
            else if (b.type == TokenType.EQ)
            {
                return e1;
            }
            else if (b.type == TokenType.LT)
            {
                ++this.position;
                var e2= parseApplication();
                var e = new Combination(ExpType.LESSTHAN, e1, e2);
                return e;
            }
            else if (b.type == TokenType.GT)
            {
                ++this.position;
                var e2 = parseApplication();
                var e = new Combination(ExpType.GREATERTHAN, e1, e2);
                return e;
            }
            else if (b.type == TokenType.RCURLY ||
                     b.type == TokenType.WHILE ||
                     b.type == TokenType.COMMA ||
                     b.type == TokenType.EOF)
            {
                return e1;
            }
            throw new NotImplementedException($"Dont understand token {b.type}");
        }
        public Exp parseApplication( )
        {
            var e1 = parseSimpleExp();
            if (this.position== tokens.Count)
            {
                return e1;
            }
            if (tokens[this.position].type == TokenType.LPAREN)
            {
                ++this.position;
                var args = new List<Exp>();
                if (tokens[this.position].type  == TokenType.RPAREN)
                {
                    ++this.position;
                    var result = new ApplicationExpression(e1,args);
                    return result;

                }
                
                while (true)
                {
                    var eArg1 = parseExp();
                    args.Add(eArg1);
                    if (tokens[this.position].type == TokenType.RPAREN)
                    {
                        ++this.position;

                        var result = new ApplicationExpression(e1, args);
                        return result;

                    }
                    else if (tokens[this.position].type == TokenType.COMMA)
                    {
                        
                        ++this.position;
                    }
                    else
                    {
                        reportError("expected comma or right parenthesis");
                    }
                }
                

            }
            return e1;
        }


        public Exp parseSimpleExp( )
        {
            var a = tokens[this.position];
            if (a.type == TokenType.NUMBER)
            {
                var e = new ConstantExpression((int)a.value);
                
                ++this.position;
                return e;


            }
            else if (a.type == TokenType.ID)
            {
                
                var e = new IdExpression((string)a.value);
                ++this.position;
                return e;

            }
            else if (a.type == TokenType.STRING)
            {
                var e = new ConstantExpression((string)a.value);
                
                ++this.position;
                return e;
            }
            return null;
        }
        #endregion
        #region statemenets
        public Statement parseStatement()
        {
            //statement ::= 
            //    'print' '(' Exp ')'
            //    'if' Exp '{' CompoundStatement '}'
            //    'if' Exp '{' CompoundStatement '}' 'else' '{' CompoundStatement '}'
            //    'while' Exp '{' Compound statement '}'
            //    'def' String '(' Exp,* ')' '{' CompoundStatement '}'
            //    'return' Exp*
            


            if (tokens.Count <= this.position)
            {
                return null;
            }
            var t = tokens[this.position];
            if ((t.type == TokenType.PRINT))
            {
                ++this.position;
                ExpectToken(TokenType.LPAREN);
                var e = parseExp();
                ExpectToken(TokenType.RPAREN);
                var p = new PrintStatement(e);
                return p;
            }
            else if ((t.type == TokenType.ID))
            {
                var left = parseExp();
                ExpectToken(TokenType.EQ);
                var right = parseExp();
                var a = new Assign(left, right);
                return a;

            }
            else if ((t.type == TokenType.IF))
            {
                ++this.position;
                var condition = parseExp();
                ExpectToken(TokenType.LCURLY);
                var cs = parseCompoundStatement();
                ExpectToken(TokenType.RCURLY);
                if (tokens[this.position].type == TokenType.ELSE)
                {
                    ++this.position;
                    ExpectToken(TokenType.LCURLY);
                    var csElse = parseCompoundStatement();
                    ExpectToken(TokenType.RCURLY);
                    var ifElse = new IfStatement(condition,cs,csElse);
                    return ifElse;
                }
                var ifstm = new IfStatement(condition,cs);
                return ifstm;
            }
            else if ((t.type == TokenType.WHILE))
            {
                ++this.position;
                var condition = parseExp();
                ExpectToken(TokenType.LCURLY);
                var cs = parseCompoundStatement();
                ExpectToken(TokenType.RCURLY);
                var wstm = new WhileStatement(condition, cs);

                return wstm;
            }
            else if (t.type == TokenType.DEFINE)
            {
                ++this.position;
                var functionName = ExpectToken(TokenType.ID);
                ExpectToken(TokenType.LPAREN);
                var parameters = parseParameters();

                ExpectToken(TokenType.RPAREN);
                ExpectToken(TokenType.LCURLY);
                var cs = parseCompoundStatement();
                ExpectToken(TokenType.RCURLY);
                var dstm = new DefStatement((string)functionName.value, parameters, cs);
                return dstm;

            }
            else if (t.type == TokenType.RETURN)
            {
                ++this.position;
                var returnValue = parseExp();
                var rstm = new ReturnStatement(returnValue);
                return rstm;
                


            }
            else if (t.type == TokenType.EOF)
            {
                return null;
            }
            else
            {
                throw new NotImplementedException($"dont know how to parse {t.type}");
            }
            
        }

        private List<Exp> parseParameters()
        {
            //(a.,b)
            //(a.)

            var parameters = new List<Exp>();
            if (Peek().type != TokenType.ID)
            {
                return parameters;
            }
            var parameterName = ExpectToken(TokenType.ID);
            parameters.Add(new IdExpression((string)parameterName.value));
            while (Peek().type == TokenType.COMMA)
            {
                ++this.position;
                parameterName = ExpectToken(TokenType.ID);
                parameters.Add(new IdExpression((string)parameterName.value));

            }
            return parameters;
            
        }

        private CompoundStatement parseCompoundStatement()
        {
            var statements = new List<Statement>();
            while (this.position != tokens.Count)
            {
                if (  tokens[this.position].type == TokenType.RCURLY)
                {
                    
                    break;
                }
            
                var s = parseStatement();
                statements.Add(s);
                

            }
            var cs = new CompoundStatement(statements);
            return cs;

        }

        private Token Peek()
        {
            return this.tokens[this.position];
        }

        #endregion
        private Token ExpectToken(TokenType type)
        {
            if (tokens.Count <= this.position)
            {
                reportError($"expected {type}");
            }
            var t = tokens[this.position];
            ++this.position;
            if ((t.type != type))
            {
                reportError($"expected {type}, but found {t.type}");
               
            }

            return t;
        }

        private void reportError(string v)
        {
            Console.WriteLine(v);
            throw new ApplicationException(v);
        }

    }

}
