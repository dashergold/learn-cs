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
        private Exp parseExpression(string program)
        {
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var e = p.parseExp();
            return e;
        }

        private Statement parseStatement(string program)
        {
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            return s;
        }

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
@"while  ( (x)  <  (2) )  {
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
        public void parseReturnAfterAssigment()
        {
            var program =
@"definera y () {
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
@"define y () {
     (x)  =  (3) 
return  ( (x)  +  (1) ) 
}
";
            Assert.That(s.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void parseElseStatement()
        {
            var program =
@"om a < b {
skriv(""hej"")
}
annars {
skriv(""hejdå"")
}
";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            Console.Write(s.ToString());
            var expected =
@"if  ( (a)  <  (b) )  {
    print(""hej"")

}
else {
    print(""hejdå"")

}
";
            Assert.That(s.ToString(), Is.EqualTo(expected));
        }
        [Test]
        public void parseFunctionWithParameters()
        {
            var program =
@"definera x (a,b){
    svara a+b
}
";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            Console.Write(s.ToString());
            var expected =
@"define x ( (a) , (b) ) {
    return  ( (a)  +  (b) ) 
}
";
            Assert.That(s.ToString(), Is.EqualTo(expected));
        }
        [Test]
        public void parsFunctionCallWithTwoParameters()
        {
            var program =
@"y = x (a,b)";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            Console.Write(s.ToString());
            var expected =
@" (y)  =   (x) ( (a) , (b) ) 
";
            Assert.That(s.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void parseNotFunction()
        {
            var program =
@"medan !a{

}
";
            var expected =
@"while  ! ( (a) )  {
    
}
";
            var s = parseStatement(program);
            Assert.That(s.ToString(), Is.EqualTo(expected));
        }
        [Test]
        public void parseParenthesisExpression()
        {

            var e = parseExpression("(x+1)");
            var expected = @" ( (x)  +  (1) ) ";
            Assert.That(e.ToString(), Is.EqualTo(expected));
        }
        [Test]
        public void parseMultiplication()
        {

            var e = parseExpression("3*x");
            var expected = @" ( (3)  *  (x) ) ";
            Assert.That(e.ToString(), Is.EqualTo(expected));

        }
        [Test]
        public void parseNE()
        {

            var e = parseExpression("3 != 4");
            var expected = @" ( (3)  !=  (4) ) ";
            Assert.That(e.ToString(), Is.EqualTo(expected));

        }
    }
}
