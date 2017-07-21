using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogAn.UnitTests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        [TestMethod]
        public void IsValidLogFileName_BadExtension_ReturnFalse()
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool actual = analyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool actual = analyzer.IsValidLogFileName("filewithgoodextension.slf");

            Assert.IsTrue(actual);
        }
    }
}
