using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class Statement
    {

    }
    public class Assign : Statement{
        public Exp Left;
        public Exp Right;
        public Assign (Exp left, Exp right)
        {
            Left = left;
            Right = right;

        }

        public override string ToString()
        {
            return $"{Left} = {Right}\r\n";

        }

    }
    public class IfStatement:Statement
    {
        public Exp Condition;
        public Statement Then;
        public Statement Else;
        public IfStatement(Exp condition, Statement then, Statement @else = null)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }
        public override string ToString()
        {
            if (Else == null)
            {
                return $"if {Condition} {{\r\n    {Then}\r\n}}\r\n";
            }
            else
            {
                return $"if {Condition} {{\r\n    {Then}\r\n}}\r\nelse {{\r\n    {Else}\r\n}}\r\n";
            }
        }
    }
    public class WhileStatement:Statement
    {
        public Exp Condition;
        public Statement Body;
        public WhileStatement(Exp condition, Statement body)
        {
            Condition = condition;
            Body = body;
        }
        public override string ToString()
        {
            return $"while {Condition} {{\r\n    {Body}\r\n}}\r\n";
        }
    }
    public class DefStatement:Statement
    {
        public string ID;
        public List<Exp> Parameters;
        public Statement Body;

        public DefStatement(String functionName, List<Exp> parameters, CompoundStatement cs)
        {

            this.Parameters = parameters;
            this.Body = cs;
            this.ID = functionName;

        }
        public override string ToString()
        {
            return $"define {this.ID} ({ParametersToString()}) {{\r\n    {Body}}}\r\n";
        }

        private string ParametersToString()
        {
            var sb = new StringBuilder();
            if (this.Parameters.Count == 0)
            {
                return "";

            }
            sb.Append(this.Parameters[0]);
            foreach (var p in this.Parameters.Skip(1))
            {
                sb.Append(",");
                sb.Append(p.ToString());
            }
            return sb.ToString();
        }
    }
    public class CompoundStatement:Statement
    {
        public List<Statement> Statements;
        public CompoundStatement(List<Statement> statements)
        {
            Statements = statements;
        }
        public override string ToString()
        {
            return String.Join("",Statements);
        }
    }
    public class PrintStatement : Statement
    {
        public Exp Exp;
        public PrintStatement(Exp exp)
        {
            this.Exp = exp;
        }
        public override string ToString()
        {
            return $"print({Exp})\r\n";
        }
    }
    public class ReturnStatement : Statement
    {
        public Exp ReturnValue;

        public ReturnStatement(Exp returnValue)
        {
            this.ReturnValue = returnValue;
        }

        public override string ToString()
        {
            return $"return {ReturnValue}\r\n";

        }

    }
}
