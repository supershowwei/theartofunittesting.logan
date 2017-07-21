using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogAn.UnitTests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        public LogAnalyzerTests()
        {
            this.TestContext = null;
        }

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void IsValidLogFileName_BadExtension_ReturnFalse()
        {
            var analyzer = new LogAnalyzer();

            var actual = analyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            var analyzer = new LogAnalyzer();

            var actual = analyzer.IsValidLogFileName("filewithgoodextension.slf");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            var analyzer = new LogAnalyzer();

            var actual = analyzer.IsValidLogFileName("filewithgoodextension.SLF");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DeploymentItem(@"TestSamples\FileNames.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"|DataDirectory|\FileNames.xml", "FileName",
            DataAccessMethod.Sequential)]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue()
        {
            var analyzer = new LogAnalyzer();

            var actual = analyzer.IsValidLogFileName((string)this.TestContext.DataRow[0]);

            Assert.IsTrue(actual);
        }
    }
}