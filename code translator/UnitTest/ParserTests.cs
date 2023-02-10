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
            var e = p.parseExp();
            Assert.AreEqual(" ( (x)  +  (3) ) ", e.ToString());
        }
        [Test]
        public void testParseStatement()
        {
            var program = "skriv(\"hej\")";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            Console.Write(s.ToString());
            Assert.AreEqual("print(\"hej\")\r\n", s.ToString());
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
            var s = p.parseStatement();
            Console.Write(s.ToString());

            var expected =
@"while  ( (x)  <  (2)   {
    print(""hej"")
 (x)  =  ( (x)  +  (1) ) 

}
";

            Assert.That(s.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void parseFunctionDefinition()
        {
            var program =
@"definera x (){
    skriv(""hej"")
}
";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            Console.Write(s.ToString());
            var expected =
@"define x () {
    print(""hej"")
}
";
            Assert.That(s.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void parseReturn()
        {
            var program =
@"
svara ""hej""
";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            Console.Write(s.ToString());
            var expected =
@"return ""hej""
";
            Assert.That(s.ToString(), Is.EqualTo(expected));


        }
        [Test]
        [Ignore("get this to work")]
        public void parseReturnAfterAssigment()
        {
            var program =
@" {
x = 3
svara x+1
}
";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            Console.Write(s.ToString());
            var expected =
@"sdkfsd ""hej""
";
            Assert.That(s.ToString(), Is.EqualTo(expected));
        }
    }
}
