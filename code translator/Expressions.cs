﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_translator
{
    public class Exp
    {
       

        

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
        public IdExpression(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
          
         return       $" ({Name}) ";
            
        }
    }
    public class ApplicationExpression : Exp
    {
        public Exp Function;
        public List<Exp> Arguments;

    }
    public class Combination : Exp
    {
        public Combination(ExpType Type, Exp Left, Exp Right)
        {
            this .Type = Type;
            this.Left = Left;
            this .Right = Right;

        }

        public ExpType Type;
        public Exp Left;
        public Exp Right;
        public override string ToString()
        {
            if (Type == ExpType.SUM)
            {
                return $" ({Left} + {Right}) ";
            }
            else if (Type == ExpType.DIFF)
            {
                return $" ({Left} - {Right} ";
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
    public class ConstantExpression : Exp
    {
        public ConstantExpression(object Value)
        {
            this.Value = Value;
        }
        public object Value;
        public override string ToString()
        {
            
               return $" ({Value}) ";
            
        }
    }
}
