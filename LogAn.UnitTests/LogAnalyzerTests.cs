using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogAn.UnitTests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        private LogAnalyzer analyzer;

        public LogAnalyzerTests()
        {
            this.TestContext = null;
        }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.analyzer = new LogAnalyzer();
        }

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

            var actual = analyzer.IsValidLogFileName(this.TestContext.DataRow[0].ToString());

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DeploymentItem(@"TestSamples\FileNames2.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"|DataDirectory|\FileNames2.xml", "FileName",
            DataAccessMethod.Sequential)]
        public void IsValidLogFileName_VariousExtensions_ChecksThem()
        {
            var analyzer = new LogAnalyzer();
            var file = this.TestContext.DataRow["Data"].ToString();
            var expected = bool.Parse(this.TestContext.DataRow["Expected"].ToString());

            var actual = analyzer.IsValidLogFileName(file);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidFileName_validFileLowerCased_ReturnsTrue()
        {
            bool actual = this.analyzer.IsValidLogFileName("whatever.slf");

            Assert.IsTrue(actual, "filename should be valid!");
        }

        [TestMethod]
        public void IsValidFileName_validFileUpperCased_ReturnsTrue()
        {
            bool actual = this.analyzer.IsValidLogFileName("whatever.SLF");

            Assert.IsTrue(actual, "filename should be valid!");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.analyzer = null;
        }
    }
}