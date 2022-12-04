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


    }
}
