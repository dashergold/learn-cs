using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_translator;

namespace UnitTest
{
    internal class TokenFileReaderTests
    {
        [Test]
        public void LoadFileContents()
        {
            var fileContents =
@"


om IF
medan WHILE

";
            var tfr = new TokenFileReader(fileContents);
            var d = tfr.Dictionary;
            Assert.That(d.Count, Is.EqualTo(2));
            Assert.That(d["om"], Is.EqualTo(TokenType.IF));
            Assert.That(d["medan"], Is.EqualTo(TokenType.WHILE));
        }
        
    }
}
