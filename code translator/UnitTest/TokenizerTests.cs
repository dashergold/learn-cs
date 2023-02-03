using code_translator;
namespace UnitTest
{
    public class TokenizerTests
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void UseDifferentKeywords()
        {
            var enDict = new Dictionary<string, TokenType> {
                {
                    "if",TokenType.IF
                }
            };
            var tokenizer = new Tokenizer(enDict);
            var tokens = tokenizer.tokenize("if");
            Assert.That(tokens.Count, Is.EqualTo(1));
            Assert.That(tokens[0].type, Is.EqualTo(TokenType.IF));


        }
    
    }
}