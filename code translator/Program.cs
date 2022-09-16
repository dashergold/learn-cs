// See https://aka.ms/new-console-template for more information
using System.Text;

var a = File.OpenText(@"D:\code\cs\code translator\index.txt");
var s = a.Read();

while (s != -1)
{
    var c = (char)s;
    if (char.IsLetter(c))
    {
        StringBuilder sb = ReadWord(a, c);
        Console.Write(sb.Length);
        Console.Write(", ");
        Console.Write(sb.ToString());
        Console.WriteLine();
    }
    s = a.Read();
}


//while (s != -1)
//{

//    var c = (char)s;

//    switch (c)
//    {
//        case 'i':
//        case 'I':

//            Console.Write("1");
//            break;
//        case 'o':
//        case 'O':

//            Console.Write("0");
//            break;
//        case 'e':
//        case 'E':

//            Console.Write("3");
//            break;
//        case 't':
//        case 'T':
//            Console.Write("7");
//            break;
//        default: Console.Write(c); break;
//    }

//if (c == 'i'||c=='I')
//{
//    Console.Write("1");
//}
//else if (c == 'o' || c == 'O')
//{
//    Console.Write("0");

//}
//else if (c == 'e'||c=='E')
//{
//    Console.Write("3");
//}
//else if (c == 's'||c=='S')
//{
//    Console.Write("5");
//}
//else if (c == 't'||c=='T')
//{
//    Console.Write("7");

//}
//else
//{
//    Console.Write(c);
//}








//    s = a.Read();


//}

//var l = new List<string>();
//while (s != null)
//{
//    l.Add(char);
//    s = a.Read();
//}
//l.Reverse();
//foreach (var Line in l)
//{
//    Console.WriteLine(Line);
//}
//while(s != null)
//{
//    Console.Write("* ");
//    Console.WriteLine(s);
//    s = a.ReadLine();

//}


a.Close();

static StringBuilder ReadWord(StreamReader a, char c)
{
    var sb = new StringBuilder();
    sb.Append(c);
    var s = a.Read();
    while (s != -1)
    {
        c = (char)s;
        if (!char.IsLetter(c))
        {

            break;

        }
        sb.Append(c);
        s = a.Read();
    }

    return sb;
}