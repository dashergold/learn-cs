﻿using code_translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class InterpreterTests
    {
        [Test]
        public void testInterpreter()
        {
            var program = "3-2";
            var t = new Tokenizer();
            var tokens = t.tokenize(program);
            var p = new Parser(tokens);
            var e = p.parseExp(0);
            var i = new Interpreter(new Context(null));
            var result = i.interpretExp(e.Item1);
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
            var s = p.parseStatement(0);
            var i = new Interpreter(new Context(null));
            var result = i.intepretStatement(s.Item1);
            Assert.That(result, Is.Null);
        }


    }
}