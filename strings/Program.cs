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
                }
               
            } else {
                Console.WriteLine("error: you must supply a parameter");
            }
        }
    }
}
