using code_translator;
namespace UnitTest
{
    public class TokenizerTests
    {
        Dictionary<string, TokenType> enDict = new () {
                { "if",TokenType.IF },
                { "define",TokenType.DEFINE }
        };

        Dictionary<string, TokenType> svDict = new() {
                { "om",TokenType.IF },
                { "definera",TokenType.DEFINE },
                { "svara", TokenType.RETURN },
                { "annars", TokenType.ELSE }
        };

        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void UseDifferentKeywords()
        {
            
            var tokenizer = new Tokenizer(enDict);
            var tokens = tokenizer.tokenize("if");
            Assert.That(tokens.Count, Is.EqualTo(2));
            Assert.That(tokens[0].type, Is.EqualTo(TokenType.IF));


        }

        [Test]
        public void TokenizeDefine()
        {
            var tokenizer = new Tokenizer(enDict);
            var tokens = tokenizer.tokenize("define");
            Assert.That(tokens.Count, Is.EqualTo(2));
            Assert.That(tokens[0].type, Is.EqualTo(TokenType.DEFINE));

        }

        [Test]
        public void TokenizeReturn()
        {
            var tokenizer = new Tokenizer(svDict);
            var tokens = tokenizer.tokenize("svara");
            Assert.That(tokens.Count, Is.EqualTo(2));
            Assert.That(tokens[0].type, Is.EqualTo(TokenType.RETURN));

        }
        [Test]
        public void TokenizeElse()
        {
            var tokenizer = new Tokenizer(svDict);
            var tokens = tokenizer.tokenize("annars");
            Assert.That(tokens.Count, Is.EqualTo(2));
            Assert.That(tokens[0].type, Is.EqualTo(TokenType.ELSE));
        }
        [Test]
        public void TokenizeNot()
        {
            var tokenizer = new Tokenizer(svDict);
            var tokens = tokenizer.tokenize("!");
            Assert.That(tokens.Count, Is.EqualTo(2));
            Assert.That(tokens[0].type, Is.EqualTo(TokenType.NOT));
        }
        [Test]
        public void TokenizeNE()
        {
            var tokenizer = new Tokenizer(svDict);
            var tokens = tokenizer.tokenize("!=");
            Assert.That(tokens.Count, Is.EqualTo(2));
            Assert.That(tokens[0].type, Is.EqualTo(TokenType.NE));
        }


    }
}