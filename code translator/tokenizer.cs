﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace code_translator
{
    internal class Tokenizer
    {
        enum Token {
            WHILE = 1,
            IF = 2,
            ELSE = 3,
            ELIF = 4,
            RETURN = 5,
            PLUS = 6,
            FUNCTION = 7,
            EQ = 8,
            TRUE = 9,
            FALSE = 10,
            NUMBER = 11,
            COMMA = 12,
            STRING = 13,
            LPAREN = 14,
            RPAREN = 15,
            LCURLY = 16,
            RCURLY = 17,
        }

        private Dictionary<string, Token> tokenValues = new() {
        {"medan", Token.WHILE },
        {"om", Token.IF },
        { "+",Token.PLUS},


    };


        int discardComment(string text, int i) {
            while( i < text.Length && text[ i] != '\n'){
                ++i;

            }
            return i + 1;

        }

        public void tokenize(string text)
        {
            int i = 0;

            while (i < text.Length)
            {
                char c = text[i];
                if (c == '#')
                {
                    //discard comment
                    i = discardComment(text, i);
                }
                else
                {
                    Console.Write(c);
                    ++i;
                }
            }
        }
    }   
}
