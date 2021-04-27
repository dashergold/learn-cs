using System;

namespace arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new string[3];
            a[0] = "hej";
            a[1] = "din";
            a[2] = "tjockskalle";
            var s = string.Join(" ", a);
            var a2 = s.Split(" ");
            Console.WriteLine(s);
            a2[2] = "dumbom";
            var s2 = string.Join(",", a2);
            Console.WriteLine(s2);

            Console.WriteLine(a2[1]);
        }
    }
}
