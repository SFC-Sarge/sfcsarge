using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using WpfAppDocFxWLoggingZipTestProject1;
using WPFAppWithDocFxLoggingAndZip2;

namespace WPFAppWithDocFxLoggingAndZip2.Tests
{
    [TestClass()]
    public class FileLoggerProviderTests
    {
        public TestContext TestContext { get; set; }
        public string CurrentFileVersion { get; set; }
        public static string CurrentAppPath { get; set; }
        public static string CurrentUnitTestLogDirValue { get; set; }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            CurrentAppPath = FileLoggerProvider.AppPath();
            CurrentUnitTestLogDirValue = Path.Combine(CurrentAppPath, Settings.Default.CurrentUnitTestLogDirValue);
            if (!Directory.Exists(CurrentUnitTestLogDirValue))
                Directory.CreateDirectory(CurrentUnitTestLogDirValue);
            TestProperties.UnitTestLogger.LogInformation($"{context.FullyQualifiedTestClassName} Started Class Testing: ******************************************************************************************", true);
            TestProperties.UnitTestLogger.LogInformation($"{Environment.NewLine}******************************************************************************************{Environment.NewLine}", true);
        }
        [TestInitialize]
        public void TestInitialize()
        {
            CurrentFileVersion = FileVersionInfo.GetVersionInfo($@"{FileLoggerProvider.AppPath()}\{FileLoggerProvider.AppName()}.exe").FileVersion;
            TestProperties.UnitTestLogger.LogInformation($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}Start Test: ******************************************************************************************", true);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {
                TestProperties.UnitTestLogger.LogInformation($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}Expected and Actual Are Equal or StartsWith! Test Result: {TestContext.CurrentTestOutcome}.", true);
            }
            else
            {
                TestProperties.UnitTestLogger.LogWarning($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}Test Result: {TestContext.CurrentTestOutcome}.", true);
            }
            TestProperties.UnitTestLogger.LogInformation($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}End Test: ******************************************************************************************{Environment.NewLine}", true);
            TestProperties.UnitTestLogger.LogInformation($"******************************************************************************************{Environment.NewLine}", true);
        }

        [TestMethod()]
        public void AppPathTest()
        {
            try
            {
                // Arrange
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.AppPath(): Expected Value:{Environment.NewLine}'{true}'", true);
                // Act
                string ActualAppPathValue = FileLoggerProvider.AppPath();
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.AppPath(): Actual Value is not Null:{Environment.NewLine}'{ActualAppPathValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.IsNotNull(ActualAppPathValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"FileLoggerProvider.AppPath() Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}FileLoggerProvider.AppPath(): Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End FileLoggerProvider.AppPath() Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod()]
        public void AppNameTest()
        {
            try
            {
                // Arrange
                string ExpectedAppNameValue = Settings.Default.ExpectedAppNameValue;
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.AppName(): Expected Value:{Environment.NewLine}'{ExpectedAppNameValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                string ActualAppNameValue = FileLoggerProvider.AppName();
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.AppName(): Actual Value:{Environment.NewLine}'{ActualAppNameValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.AreEqual(ExpectedAppNameValue, ActualAppNameValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"FileLoggerProvider.AppName() Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}FileLoggerProvider.AppName(): Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End FileLoggerProvider.AppName() Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void GetLogFileNameTest()
        {
            try
            {
                // Arrange
                string ExpectedGetLogFileNameValue = $@"{Settings.Default.CurrentUnitTestLogDirValue}\{Settings.Default.UnitTestLogFileName}_{FileVersionInfo.GetVersionInfo($@"{FileLoggerProvider.AppPath()}\{FileLoggerProvider.AppName()}.exe").FileVersion}_";
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.GetLogFileName({Settings.Default.UnitTestLogFileName}, {Settings.Default.CurrentUnitTestLogDirValue}): Expected Value:{Environment.NewLine}'{ExpectedGetLogFileNameValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                string ActualGetLogFileNameValue = FileLoggerProvider.GetLogFileName(Settings.Default.UnitTestLogFileName, Settings.Default.CurrentUnitTestLogDirValue);
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.GetLogFileName(){Settings.Default.UnitTestLogFileName}, {Settings.Default.CurrentUnitTestLogDirValue}: Actual Value:{Environment.NewLine}'{ActualGetLogFileNameValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.IsTrue(ActualGetLogFileNameValue.StartsWith(ExpectedGetLogFileNameValue));
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"FileLoggerProvider.GetLogFileName({Settings.Default.UnitTestLogFileName}, {Settings.Default.CurrentUnitTestLogDirValue}) Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}FileLoggerProvider.GetLogFileName({Settings.Default.UnitTestLogFileName}, {Settings.Default.CurrentUnitTestLogDirValue}): Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End FileLoggerProvider.GetLogFileName({Settings.Default.UnitTestLogFileName}, {Settings.Default.CurrentUnitTestLogDirValue}) Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void CreateLoggerTest()
        {
            try
            {
                FileLoggerProvider fileLoggerProvider = new();
                // Arrange
                string ExpectedLoggerValue = Settings.Default.ExpectedILoggerValue;
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.CreateLogger({Settings.Default.UnitTestLogFileName}): Expected Value:{Environment.NewLine}'{ExpectedLoggerValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                string ActualLoggerValue = $"{fileLoggerProvider.CreateLogger(Settings.Default.UnitTestLogFileName)}";
                TestProperties.UnitTestLogger.LogInformation($"FileLoggerProvider.CreateLogger({Settings.Default.UnitTestLogFileName}): Actual Value:{Environment.NewLine}'{ActualLoggerValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.AreEqual(ExpectedLoggerValue, ActualLoggerValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"FileLoggerProvider.CreateLogger({Settings.Default.UnitTestLogFileName}) Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}FileLoggerProvider.CreateLogger({Settings.Default.UnitTestLogFileName}): Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End FileLoggerProvider.CreateLogger({Settings.Default.UnitTestLogFileName}) Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
            //finally
            //{
            //    fileLoggerProvider.Dispose();
            //}
        }
    }
}