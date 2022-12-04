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

    }
    public class IfStatement:Statement
    {
        public Exp Condition;
        public Statement Then;
        public Statement Else;

    }
    public class WhileStatement:Statement
    {
        public Exp Condition;
        public Statement Body;

    }
    public class DefStatement:Statement
    {
        public string ID;
        public List<Exp> Parameters;
        public Statement Body;


    }
    public class CompoundStatement:Statement
    {
        public List<Statement> Statements;
    }
    public class PrintStatement : Statement
    {
        public Exp Exp;
        public PrintStatement(Exp exp)
        {
            Exp = exp;
        }
        public override string ToString()
        {
            return $"print {Exp}\r\n";
        }
    }
}
