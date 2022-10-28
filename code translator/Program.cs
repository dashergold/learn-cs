// See https://aka.ms/new-console-template for more information
using code_translator;
using System.Text;

//var a = File.OpenText(@"D:\code\cs\code translator\index.txt");
//var n = readNumber(a);
#if whole_file 
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
#endif 
////test("3");
//test("gurka");
//test("\"gurka\"");
test("x+3");
static void test(string program)
{
    var p = new Parser();
    var t = new Tokenizer();
    var tokens = t.tokenize(program);
    var e = p.parseExp(tokens, 0);
    Console.WriteLine(e.ToString());
}



////while (n != null)
//{
//    Console.WriteLine(n);
//    n = readNumber(a);

//}


static int? readNumber(TextReader a)
{
    var s = a.Read();
    while (s != -1)
    {
        var cs = (char)s;
        if (char.IsDigit(cs))
        {
            var n = s - 48;
            s = a.Read();
            while (s != -1)
            {
                cs = (char)s;
                if (!char.IsDigit(cs))
                {
                    return n;

                }
                var d = s - 48;
                n = n * 10 + d;
                s=a.Read(); 

            }

        }
        s = a.Read();
    }
    return null;
} 

//a.Close();
