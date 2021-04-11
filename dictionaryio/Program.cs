using System;

using System.IO;

using System.Collections.Generic;

namespace dictionaryio
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordDict = makeWordDictionary();
            var numDict = makeNumDictionary();
            var input = File.OpenText("D:\\code\\cs\\dictionaryio\\input.txt");
            var line = input.ReadLine();
            while(line != null) {
                if(wordDict.ContainsKey(line)){
                    var translate = wordDict[line];
                    Console.WriteLine(translate);
                }else if (numDict.ContainsKey(line)){
                    var translate = numDict[line];
                    Console.WriteLine(translate);
                } else {
                    Console.WriteLine("Unknown Word: {0}", line);
                }
                
                line = input.ReadLine();
                
            }
            input.Close();
            

            
        }

        static Dictionary<string, string> makeWordDictionary() {
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
        static Dictionary<string, int> makeNumDictionary() {
            var dict = new Dictionary<string, int> {
                { "noll", 0 },
                { "ett", 1 },
                { "två", 2 },
                { "tre", 3 },
                { "fyra", 4 },
                { "fem", 5 },
                { "sex", 6 },
                { "sju", 7 },
                { "åtta", 8 },
                { "nio", 9 }
                
            };
            return dict;
        }

    }
}
