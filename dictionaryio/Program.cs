using System;

using System.Collections.Generic;

namespace dictionaryio
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = makeDictionary();

            
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
