using code_translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ParserTests
    {
        [Test]
        public void testParser()
        {
            var program = "x+3";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var e = p.parseExp(0);
            Assert.AreEqual("( ( (x)  +  (3) ) , 3)", e.ToString());
        }
        [Test]
        public void testParseStatement()
        {
            var program = "skriv(\"hej\")";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement(0);
            Console.Write(s.Item1.ToString());
            Assert.AreEqual("print  (hej) \r\n", s.Item1.ToString());
        }
        [Test]
        public void parseWhileStatement()
        {
            var program =
            @"
            medan x < 2 {
               skriv(""hej"")
                x = x+1
            }";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement(0);
            Console.Write(s.Item1.ToString());
            var expected =
@"while  ( (x)  <  (2)   {
    code_translator.CompoundStatement
}
";
            Assert.That(s.Item1.ToString(), Is.EqualTo(expected));
        }
    }
}
