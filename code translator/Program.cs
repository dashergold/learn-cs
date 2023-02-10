// See https://aka.ms/new-console-template for more information
using code_translator;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var o = new Options(args);
        if (o.Language == null)
        {
            o.Language = "en";

        }

        var dict = loadLanguage(o.Language);
        var program = File.ReadAllText(o.FileName);
        var t = new Tokenizer(dict);
        var tokens = t.tokenize(program);
        var p = new Parser(tokens);
        var s = p.parseProgram();
        var c = new Context(null);
        var i = new Interpreter(c);
        i.interpretStatement(s);
        


    }

    private static Dictionary<string,TokenType> loadLanguage(string language)
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var languageFileName = Path.Combine(appDataFolder, "axol",$"{language}.txt");
        var rdr = File.OpenText(languageFileName);
        var tfr = new TokenFileReader(rdr);
        var result = tfr.Dictionary;
        rdr.Close();
        return result;

    }
}

