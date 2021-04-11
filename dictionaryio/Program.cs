using System;

using System.IO;

using System.Collections.Generic;

namespace dictionaryio
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = makeDictionary();
            var input = File.OpenText("D:\\code\\cs\\dictionaryio\\input.txt");
            var line = input.ReadLine();
            while(line != null) {
                Console.WriteLine(line);
                line = input.ReadLine();
                
            }
            input.Close();
            

            
        }

        static Dictionary<string, string> makeDictionary() {
            var dict = new Dictionary<string, string> {
                { "fisk", "fish" },
                { "gurka", "cucumber" },
                { "ananas", "pineapple" },
                { "träd", "tree" }
            };
            return dict;
        }
    }
}
