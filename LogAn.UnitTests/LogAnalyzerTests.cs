using System;
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "filename has to be provided")]
        public void IsValidLogFileName_EmptyFileName_ThrowsException()
        {
            this.analyzer.IsValidLogFileName(string.Empty);
        }

        [TestMethod]
        public void IsValidLogFileName_EmptyFileName_Throws()
        {
            try
            {
                this.analyzer.IsValidLogFileName(string.Empty);

                Assert.Fail("An exception should have been thrown");
            }
            catch (ArgumentException argumentException)
            {
                Assert.AreEqual("filename has to be provided", argumentException.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception of type {0} caught: {1}", ex.GetType(), ex.Message);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.analyzer = null;
        }
    }
}