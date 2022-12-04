// See https://aka.ms/new-console-template for more information
using code_translator;
using System.Text;

//var a = File.OpenText(@"D:\code\cs\code translator\index.txt");
//var n = readNumber(a);

var a = File.ReadAllText(@"D:\code\python\Translate code python\translations\job.axol");
var t = new Tokenizer();
var tokens = t.tokenize(a);
foreach (var token in tokens)
{
    Console.Write(token.type);
    Console.Write(" ");
    if (token.value != null)
    {
        
        Console.Write($" ({token.value}) ");
    }
}
 
////test("3");
//test("gurka");
//test("\"gurka\"");
//testParser("x+3");
//testInterpreter();
//testInterpreter2();
//testInterpreter("halt()");
//testParseAndInterpret("skriv(1+2)");
//testParseAndInterpret("skriv(3*4)");
//testParseAndInterpret("skriv(2-1)");








////while (n != null)
//{
//    Console.WriteLine(n);
//    n = readNumber(a);

//}





