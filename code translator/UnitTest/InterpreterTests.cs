using code_translator;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class InterpreterTests
    {
        private Context interpretProgram(string program)
        {
            
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseProgram();
            var c = new Context(null);
            var i = new Interpreter(c);
            var result = i.interpretStatement(s);
            return c;
        }

        [Test]
        public void testInterpreter()
        {
            var program = "3-2";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var e = p.parseExp();
            var i = new Interpreter(new Context(null));
            var result = i.interpretExp(e);
            Assert.AreEqual(1, (int)result);


        }
        [Test]
        public void testInterpreter2()
        {
            var interpreter = new Interpreter(new Context(null));
            var three = new ConstantExpression(3);
            var two = new ConstantExpression(2);
            var five = new ConstantExpression(5);
            var combo = new Combination(ExpType.SUM, five, two);
            var combo2 = new Combination(ExpType.PROD, combo, three);
            var result = interpreter.interpretExp(combo2);
            Assert.AreEqual(21, (int)result);

        }
        [Test]
        public void testParseAndInterpret()
        {
            var program = "skriv(3*4)";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var s = p.parseStatement();
            var i = new Interpreter(new Context(null));
            var result = i.interpretStatement(s);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void testAssignment()
        {
            var program = "x = 4";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo(4));

        }
        [Test]
        public void interpretIfStatement()
        {
            var program =
            @"om 3 < 4{
                x=""mindre""
            }";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo("mindre"));
        
            
        }
        [Test]
        public void interpretIfStatementFails()
        {
            var program =
            @"om 3 > 4{
                x=""mindre""
            }";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.False);


        }
        [Test]
        public void interpretIfElseStatement()
        {
            var program =
            @"om 3 > 4{
                x=""mindre""
            }
            annars {
                x =""större""
            }";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo("större"));
        }
        [Test]
        public void interpretWhileStatement()
        {
            var program =
            @"x = 0
            medan x < 2 {
               skriv(""hej"")
                x = x+1
            }";
            var c = interpretProgram(program);
            var(found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo(2));


        }

        [Test]
        [Ignore("to do")]
        public void interpretNot()
        {
            var program = "x = inte(4>3)";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo(false));
        }
        [Test]
        public void interpretDefStatement()
        {
            var program = "definera x () {svara 0}";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value is DefStatement, Is.EqualTo(true));
        }

        [Test]
        public void interpretParameterlessCall()
        {
            var program =
@"definera y() {
svara 3+1
}
x = y()
";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo(4));

        }

        [Test]
        [Ignore("get this to work")]
        public void interpretWithMath()
        {
            var program =
@"definera y() {
x = 3
svara x+1
}
x = y()
";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo(4));

        }
        [Test]
        [Ignore("need o evaluate arguments before making calls")]
        public void interpretFunctionCallWithParameters()
        {
            var program =
@"definera addera (a,b){
    svara a+b
}
x = addera(1,2)
";
            var c = interpretProgram(program);
            var (found, value) = c.LookUp("x");
            Assert.That(found, Is.True);
            Assert.That(value, Is.EqualTo(3));
        }
        
    }
}
