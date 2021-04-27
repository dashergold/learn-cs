using System;

namespace strings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((char) 8147);
            Console.WriteLine((int) 'A');
            Console.WriteLine(Convert.ToDouble("5675678787867856785647457547575.3"));


            if(args.Length >= 1) {
                for(int i = 0; i < args.Length; i=i+1){
                    string s = args[i];
                    int Length = s.Length;
                    // Console.WriteLine("argument {0} {1} of length {2}", i, args[i], args[i].Length);
                    for(int j = s.Length-1; j>=0; j=j-1) {
                        Console.Write(s[j]);
                        Console.Write(' ');
                    }        
                    Console.WriteLine();
                    chopString(s, 2);
                    chopString(s, 3);
                    dumpString(s);
                }
               
            } else {
                Console.WriteLine("error: you must supply a parameter");
            }
        }
        
        static void chopString(string s, int chop) {
            for(int j = 0; j<s.Length; j=j+chop) {
                        int nChars;
                        if(j+chop<=s.Length){
                            nChars=chop;
                        }else {
                            nChars = s.Length-j;
                        }
                        Console.Write(s.Substring(j, nChars));
                        Console.Write(' ');
                    }
                    Console.WriteLine();
        }
        static void dumpString(string s) {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            for (int i = 0; i<bytes.Length; i=i+1) {
                Console.Write((int)bytes[i]);
                Console.Write(' ');

            }
            Console.WriteLine();
            
        }
    }
}
