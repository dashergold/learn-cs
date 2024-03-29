﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static code_translator.Tokenizer;
using static System.Net.Mime.MediaTypeNames;

namespace code_translator
{
    public class Token
    {
        public TokenType type;
        public object value;

    }
    public enum TokenType
    {
        EOF = 0,
        WHILE = 1,
        IF,
        ELSE,
        ELIF,
        RETURN,
        PLUS,
        FUNCTION,
        EQ,
        NE,
        GT,
        LT,
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
        IMPORT,
        PRINT,
        MINUS,
        MULT,
        DEFINE,
        NOT,
        
    }
    public class Tokenizer
    {
       
        

        //swedish keywords (will be moved)
        private Dictionary<string, TokenType> swedishTokenValues = new() {
            {"medan", TokenType.WHILE },
            {"om", TokenType.IF },
            {"importera", TokenType.IMPORT },
            {"skriv", TokenType.PRINT },
            {"annarsom", TokenType.ELIF },
            {"definera", TokenType.DEFINE },
            {"svara", TokenType.RETURN },
            {"annars", TokenType.ELSE },
            


    };
        private Dictionary<string, TokenType> tokenValues;


        public Tokenizer()
        {
            this.tokenValues = swedishTokenValues;
        }

        public Tokenizer(Dictionary<string, TokenType> tokenvalues)
        {
            this.tokenValues = tokenvalues;
        }

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
        Token token(TokenType ty)
        {
            Token t = new Token();
            t.type = ty;
            return t;
        }

        public List<Token> tokenize(string text)
        {
            List<Token> tokens = new List<Token>();
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
                    Token number = new Token();
                    number.type = TokenType.NUMBER;
                    number.value = a;
                    tokens.Add(number);

                }
                else if (c == '"')
                {
                    string s;
                    (s, i) = tokenizeString(text, i);
                    Token str = new Token();
                    str.type = TokenType.STRING;
                    str.value = s;
                    tokens.Add(str);

                }
                else if (char.IsLetter(c))
                {
                    //tokenize word
                    string s;
                    (s, i) = tokenizeWord(text, i);
                    if (tokenValues.ContainsKey(s))
                    {
                        //found a keyword
                        var t = tokenValues[s];
                        //Console.Write(t);
                        //Console.Write(" ");
                        Token keyword = new Token();
                        keyword.type = t;
                        tokens.Add(keyword);                        
                    }
                    else
                    {
                    
                        Token id = new Token();
                        id.type = TokenType.ID;
                        id.value = s;
                        tokens.Add(id);

                    }
                }
                else if (c == '(')
                {
                    
                    tokens.Add(token(TokenType.LPAREN));
                    ++i;
                }
                else if (c == ')')
                {
                    tokens.Add(token(TokenType.RPAREN));
                    ++i;
                }
                else if (c == '{')
                {
                    tokens.Add(token(TokenType.LCURLY));
                    ++i;
                }
                else if (c == '}')
                {
                    tokens.Add(token(TokenType.RCURLY));

                    ++i;

                }
                else if (c == ',')
                {
                    tokens.Add(token(TokenType.COMMA));

                    ++i;

                }
                else if (c == '=')
                {
                    tokens.Add(token(TokenType.EQ));

                    ++i;


                }
                else if (c == '<')
                {
                    tokens.Add(token(TokenType.LT));

                    ++i;

                }
                else if (c == '>')
                {
                    tokens.Add(token(TokenType.GT));

                    ++i;

                }
                else if (c == '.')
                {
                    tokens.Add(token(TokenType.DOT));

                    ++i;

                }
                else if (c == '+')
                {
                    tokens.Add(token(TokenType.PLUS));

                    ++i;

                }
                else if (c == '-')
                {
                    tokens.Add(token(TokenType.MINUS));

                    ++i;

                }
                else if (c == '*')
                {
                    tokens.Add(token(TokenType.MULT));
                    ++i;
                }
                else if (c == '!')
                {

                    ++i;
                    if (i >= text.Length)
                    {
                        tokens.Add(token(TokenType.NOT));
                    }
                    else
                    {
                        c = text[i];

                        if (c == '=')
                        {
                            ++i;
                            tokens.Add(token(TokenType.NE));
                        }
                        else
                        {
                            tokens.Add(token(TokenType.NOT));
                        }
                    }
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
            tokens.Add(token(TokenType.EOF));
            return tokens;

        }
    }
}
