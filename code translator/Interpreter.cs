using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class Interpreter
    {
        private Context context;
        public object interpretExp(Exp e)
        {
            if (e is ConstantExpression c)
            {
                return (c.Value);
            }
            else if (e is IdExpression id)
            {
                var (found, value )= context.LookUp(id.Name);
                if (!found)
                {
                    throw new ApplicationException($"the variable {id.Name} has no value");

                } 
                return (value);
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
                else if (combo.Type == ExpType.LESSTHAN)
                {
                    var left = interpretExp(combo.Left);
                    var right = interpretExp(combo.Right);
                    var nleft = (int)left;
                    var nright = (int)right;
                    return nleft < nright;
                }
                else if (combo.Type == ExpType.GREATERTHAN)
                {
                    var left = interpretExp(combo.Left);
                    var right = interpretExp(combo.Right);
                    var nleft = (int)left;
                    var nright = (int)right;
                    return nleft > nright;
                }


            }
            else if (e is ApplicationExpression a)
            {
                //to do: evaluate arguments
                if (a.Function is not IdExpression fnname)
                {
                    throw new NotImplementedException($"dont know how to interpret {e}");
                }
                var (found, definition) = this.context.LookUp(fnname.Name);
                if (!found)
                {
                    throw new InvalidOperationException($"unknown function {fnname.Name}");
                }
                if (definition is not DefStatement def)
                {
                    throw new InvalidOperationException($"{fnname.Name} is not a function");
                }
                //if (def.Parameters.Count != a.Arguments.Count)
                //{
                //    throw new InvalidOperationException($"{fnname.Name} expected {def.Parameters.Count} arguments, but recieved {a.Arguments.Count}");
                //}
                var fncontext = new Context(this.context);
                var argumentsValues = new List<object>();
                for (int i = 0; i<a.Arguments.Count; ++i)
                {
                    var arg = a.Arguments[i];
                    var parameter = def.Parameters[i];
                    var value = interpretExp(arg);
                    fncontext.SetValue(parameter.Name, value);
                }             
                var oldContext = this.context;
                this.context = fncontext;
                var reply = interpretStatement(def.Body);
                this.context = oldContext;
                return reply;
            }
            
            

            throw new NotImplementedException($"dont know how to interpret {e}");

        }

        public object interpretStatement(Statement stm)
        {
            if (stm is PrintStatement p)
            {
                var value = interpretExp(p.Exp);
                Console.WriteLine(value);
                return null;
            }
            else if (stm is Assign a)
            {
                var value = interpretExp(a.Right);
                var id = (IdExpression)a.Left;
                this.context.SetValue(id.Name, value);
                return value;
            }
            else if (stm is CompoundStatement cs)
            {
                object value = null;

                foreach (var s in cs.Statements)
                {
                    value = interpretStatement(s);

                }
                return value;
            }
            else if  (stm is IfStatement ifstm)
            {
                var condition = interpretExp(ifstm.Condition);
                bool condition2 = InterpretAsBool(condition);
                if (condition2)
                {
                    return interpretStatement(ifstm.Then);
                }
                else
                {
                    if (ifstm.Else != null)
                    {
                        return interpretStatement(ifstm.Else);

                    }
                    return null;
                }

            }
            else if (stm is WhileStatement wstm)
            {
                var condition = interpretExp(wstm.Condition);
                bool condition2 = InterpretAsBool(condition);
                while (condition2)
                {
                     interpretStatement(wstm.Body);
                     condition = interpretExp(wstm.Condition);
                     condition2 = InterpretAsBool(condition);
                }
                return null;
            }
            else if (stm is DefStatement dstm)
            {
                this.context.SetValue(dstm.ID, dstm);
                return null;
            }
            else if (stm is ReturnStatement rstm)
            {
                var answer = interpretExp(rstm.ReturnValue);
                return answer;
            }
            else
            {
                throw new NotImplementedException($"dont know how to interpret {stm}");
            }
            
        }

        private bool InterpretAsBool(object condition)
        {
            if (condition is null)
            {
                return false;

            }
            else if (condition is Boolean b)
            {
                return b;
            }
            else if (condition is int i)
            {
                return i != 0;
            }
            else if (condition is string s)
            {
                return s.Length != 0;
            }
            else
            {
                throw new NotImplementedException($"dont know how to convert {condition} to bool");
            }
        }

        public Interpreter(Context globals)
        {
            this.context = globals;
        }
    }

}
