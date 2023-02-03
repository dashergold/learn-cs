using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_translator;

namespace UnitTest
{
    internal class OptionsTests
    {
        [Test]
        public void SingleArgument()
        {
            string[] args = new string[]
            {
                "myprog.axol"
            };
            Options o = new Options(args);
            Assert.That(o.FileName,Is.EqualTo ("myprog.axol"));
        }
        [Test]
        public void LanguageArgument()
        {
            string[] args = new string[]
            {
                "-l","sv","myprog.axol"
            };
            Options o = new Options(args);
            Assert.That(o.FileName, Is.EqualTo("myprog.axol"));
            Assert.That(o.Language, Is.EqualTo("sv"));
        }
        [Test]
        
        public void MissingFileName()
        {
            string[] args = new string[]
            {
                "-l","sv"
            };
            Assert.Throws<ArgumentException>(() => new Options (args));
            
        }
        }
    }
