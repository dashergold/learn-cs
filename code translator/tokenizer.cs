using Microsoft.VisualBasic;
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
        enum Token
        {
            WHILE = 1,
            IF,
            ELSE,
            ELIF,
            RETURN,
            PLUS,
            FUNCTION,
            EQ,
            TRUE,
            FALSE,
            NUMBER,
            COMMA,
            STRING,
            LPAREN,
            RPAREN,
            LCURLY,
            RCURLY,
            ID,
            DOT,
            GT,
            LT,
            IMPORT,
            PRINT,
        }

        //swedish keywords (will be moved)
        private Dictionary<string, Token> tokenValues = new() {
            {"medan", Token.WHILE },
            {"om", Token.IF },
            {"importera", Token.IMPORT },
            {"skriv", Token.PRINT},
            {"annarsom", Token.ELIF},


    };


        int discardComment(string text, int i)
        {
            while (i < text.Length && text[i] != '\n')
            {
                ++i;

            }
            return i + 1;

        }
        (int, int) tokenizeNumber(string text, int i)
        {


            int n = 0;
            while (i < text.Length)
            {
                var c = text[i];

                if (!char.IsDigit(c))

                {
                    return (n, i);

                }
                var d = c - '0';
                n = n * 10 + d;
                ++i;
            }
            return (n, i);
        }

        (string, int) tokenizeWord(string text, int i)
        {
            var sb = new StringBuilder();
            while (i < text.Length)
            {
                var c = text[i];
                if (!char.IsLetter(c))
                {
                    return (sb.ToString(), i);
                }

                sb.Append(c);
                ++i;
            }
            return (sb.ToString(), i);
        }
        (string, int) tokenizeString(string text, int i)
        {
            var sb = new StringBuilder();

            //skip the '"'
            ++i;
            while (i < text.Length)
            {
                var c = text[i];
                if (c == '"')
                {
                    ++i;
                    return (sb.ToString(), i);
                }
                sb.Append(c);
                ++i;
            }
            throw new InvalidOperationException("An error has occured");


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
                else if (char.IsDigit(c))
                {
                    //read number
                    int a;
                    (a, i) = tokenizeNumber(text, i);
                    Console.Write(Token.NUMBER);
                    Console.Write($" ({a}) ");



                }
                else if (c == '"')
                {
                    string s;
                    (s, i) = tokenizeString(text, i);
                    Console.Write(Token.STRING);
                    Console.Write($" ({s}) ");


                }
                else if (char.IsLetter(c))
                {
                    //tokenize word
                    string s;
                    (s, i) = tokenizeWord(text, i);
                    if (tokenValues.ContainsKey(s))
                    {
                        var t = tokenValues[s];
                        Console.Write(t);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(Token.ID);
                        Console.Write($" ({s}) ");
                    }
                }
                else if (c == '(')
                {
                    Console.Write(Token.LPAREN);
                    Console.Write(" ");
                    ++i;
                }
                else if (c == ')')
                {
                    Console.Write(Token.RPAREN);
                    Console.Write(" ");
                    ++i;
                }
                else if (c == '{')
                {
                    Console.Write(Token.LCURLY);
                    Console.Write(" ");
                    ++i;
                }
                else if (c == '}')
                {
                    Console.Write(Token.RCURLY);
                    Console.Write(" ");
                    ++i;

                }
                else if (c == ',')
                {
                    Console.Write(Token.COMMA);
                    Console.Write(" ");
                    ++i;

                }
                else if (c == '=')
                {
                    Console.Write(Token.EQ);
                    Console.Write(" ");
                    ++i;


                }
                else if (c == '<')
                {
                    Console.Write(Token.LT);
                    Console.Write(" ");
                    ++i;

                }
                else if (c == '>')
                {
                    Console.Write(Token.GT);
                    Console.Write(" ");
                    ++i;

                }
                else if (c == '.')
                {
                    Console.Write(Token.DOT);
                    Console.Write(" ");
                    ++i;

                }
                else if (char.IsWhiteSpace(c))
                {

                    ++i;

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
