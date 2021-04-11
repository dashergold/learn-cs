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
                if(dict.ContainsKey(line)){
                    var translate = dict[line];
                    Console.WriteLine(translate);
                }else {
                    Console.WriteLine("Unknown Word: {0}", line);
                }
                
                line = input.ReadLine();
                
            }
            input.Close();
            

            
        }

        static Dictionary<string, string> makeDictionary() {
            var dict = new Dictionary<string, string> {};
            var input = File.OpenText("D:\\code\\cs\\dictionaryio\\dict.txt");
            var line = input.ReadLine();
            while(line != null) {
                var pieces = line.Split(',');
                dict.Add(pieces[0], pieces[1]);
                line = input.ReadLine();
            }
            input.Close();
            return dict;
        }
    }
}
