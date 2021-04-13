using System;

namespace strings
{
    class Program
    {
        static void Main(string[] args)
        {



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
    }
}
