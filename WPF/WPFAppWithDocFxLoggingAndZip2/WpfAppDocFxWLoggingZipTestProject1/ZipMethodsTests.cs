using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.IO.Compression;
using WpfAppDocFxWLoggingZipTestProject1;
using WPFAppWithDocFxLoggingAndZip2
;

namespace WPFAppWithDocFxLoggingAndZip2.Tests
{
    [TestClass()]
    public class ZipMethodsTests
    {

        [TestMethod()]
        public void AddFileToZipTest()
        {
            bool ActualAddFileToZipValue;
            try
            {
                // Arrange
                bool ExpectedAddFileToZipValue = Settings.Default.ExpectedBoolValueNameValue;
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.AddFileToZip: Expected Value:{Environment.NewLine}'{ExpectedAddFileToZipValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                if (!File.Exists(ZipFilePath))
                {
                    ActualAddFileToZipValue = false;
                    TestProperties.UnitTestLogger.LogWarning($"File {ZipFilePath} does not exist.:{Environment.NewLine}", true);
                }
                else
                {
                    string fileToAddName = Path.GetFileName(FileToAddFullName);

                    TestProperties.UnitTestLogger.LogWarning($"File {ZipFilePath} exists.:{Environment.NewLine}", true);
                    ZipMethods.AddFileToZip(ZipFilePath, FileToAddFullName, PromptZipExist, PromptFileExist, CompressionLevelMode);
                    ActualAddFileToZipValue = ZipMethods.FileExistInZip(ZipFilePath, fileToAddName, false);
                }
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.AddFileToZip: Actual Value:{Environment.NewLine}'{ActualAddFileToZipValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.AreEqual(ExpectedAddFileToZipValue, ActualAddFileToZipValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"ZipMethods.AddFileToZip Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}ZipMethods.AddFileToZip: Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End ZipMethods.AddFileToZip Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
        }

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

        [TestCleanup()]
        public void Cleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {
                TestProperties.UnitTestLogger.LogInformation($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}Expected and Actual Are Equal! Test Result: {TestContext.CurrentTestOutcome}.", true);
            }
            else
            {
                TestProperties.UnitTestLogger.LogWarning($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}Test Result: {TestContext.CurrentTestOutcome}.", true);
            }
            TestProperties.UnitTestLogger.LogInformation($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}End Test: ******************************************************************************************{Environment.NewLine}", true);
            TestProperties.UnitTestLogger.LogInformation($"******************************************************************************************{Environment.NewLine}", true);
        }

        [TestMethod()]
        public void CreateNewFileInZipTest()
        {
            if (File.Exists(ZipFilePath))
                File.Delete(ZipFilePath);
            try
            {
                // Arrange
                bool ExpectedCreateNewFileInZipValue = Settings.Default.ExpectedBoolValueNameValue;
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.CreateNewFileInZip: Expected Value:{Environment.NewLine}'{ExpectedCreateNewFileInZipValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                ZipMethods.CreateNewFileInZip(ZipFilePath, FileNameToCreate, LinesToAddToNewFile, PromptFileExist, CompressionLevelMode);
                bool ActualCreateNewFileInZipValue = ZipMethods.FileExistInZip(ZipFilePath, FileNameToCreate, false);
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.CreateNewFileInZip: Actual Value:{Environment.NewLine}'{ActualCreateNewFileInZipValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.AreEqual(ExpectedCreateNewFileInZipValue, ActualCreateNewFileInZipValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"ZipMethods.CreateNewFileInZip Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}ZipMethods.CreateNewFileInZip: Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End ZipMethods.CreateNewFileInZip Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void CreateNewZipFileTest()
        {
            if (File.Exists(ZipFilePath))
                File.Delete(ZipFilePath);
            try
            {
                // Arrange
                bool ExpectedCreateNewZipFileValue = Settings.Default.ExpectedBoolValueNameValue;
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.CreateNewZipFile: Expected Value:{Environment.NewLine}'{ExpectedCreateNewZipFileValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                string fileToCheckFor = Path.GetFileName(FileToAddFullName);
                ZipMethods.CreateNewZipFile(ZipFilePath, FileToAddFullName, PromptFileExist, CompressionLevelMode);
                bool ActualCreateNewZipFileValue = ZipMethods.FileExistInZip(ZipFilePath, fileToCheckFor, false);
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.CreateNewZipFile: Actual Value:{Environment.NewLine}'{ActualCreateNewZipFileValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.AreEqual(ExpectedCreateNewZipFileValue, ActualCreateNewZipFileValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"ZipMethods.CreateNewZipFile Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}ZipMethods.CreateNewZipFile: Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End ZipMethods.CreateNewZipFile Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void CreateZipFromDirTest()
        {
            if (File.Exists(ZipFilePath))
                File.Delete(ZipFilePath);
            try
            {
                // Arrange
                bool ExpectedCreateZipFromDirValue = Settings.Default.ExpectedBoolValueNameValue;
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.CreateZipFromDir: Expected Value:{Environment.NewLine}'{ExpectedCreateZipFromDirValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                ZipMethods.CreateZipFromDir(DirToZip, ZipFilePath, PromptFileExist, CompressionLevelMode);
                bool ActualCreateZipFromDirValue = ZipMethods.FileExistInZip(ZipFilePath, FileInDirExist, false);
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.CreateZipFromDir: Actual Value:{Environment.NewLine}'{ActualCreateZipFromDirValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.AreEqual(ExpectedCreateZipFromDirValue, ActualCreateZipFromDirValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"ZipMethods.CreateZipFromDir Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}ZipMethods.CreateZipFromDir: Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End ZipMethods.CreateZipFromDir Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void DeleteFileInZipTest()
        {
            bool ActualDeleteFileInZipValue;
            try
            {
                // Arrange
                bool ExpectedDeleteFileInZipValue = Settings.Default.ExpectedBoolValueNameValue;
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.DeleteFileInZip: Expected Value:{Environment.NewLine}'{ExpectedDeleteFileInZipValue.ToString(Settings.Default.cultures)}'", true);
                // Act
                if (!File.Exists(ZipFilePath))
                {
                    ActualDeleteFileInZipValue = false;
                    TestProperties.UnitTestLogger.LogWarning($"File {ZipFilePath} does not exist.:{Environment.NewLine}", true);
                }
                else
                {
                    TestProperties.UnitTestLogger.LogWarning($"File {ZipFilePath} exists.:{Environment.NewLine}", true);
                    ZipMethods.DeleteFileInZip(ZipFilePath, FileNameToDelete, PromptFileExist);
                    ActualDeleteFileInZipValue = ZipMethods.FileExistInZip(ZipFilePath, FileNameToCreate, false);
                }
                TestProperties.UnitTestLogger.LogInformation($"ZipMethods.DeleteFileInZip: Actual Value:{Environment.NewLine}'{ActualDeleteFileInZipValue.ToString(Settings.Default.cultures)}'", true);
                // Assert
                Assert.AreNotEqual(ExpectedDeleteFileInZipValue, ActualDeleteFileInZipValue);
            }
            catch (AssertFailedException ex)
            {
                TestProperties.UnitTestLogger.LogError(ex, $"ZipMethods.DeleteFileInZip Exception:{Environment.NewLine}{ex.Message}{Environment.NewLine}ZipMethods.DeleteFileInZip: Failed", true, true);
                TestProperties.UnitTestLogger.LogInformation($"End ZipMethods.DeleteFileInZip Test: ******************************************************************************************{Environment.NewLine}{Environment.NewLine}", true);
                Assert.Fail(ex.Message);
            }
        }
        [TestInitialize]
        public void TestInitialize()
        {
            ZipFilePath = Path.Combine(CurrentAppPath, Settings.Default.UnitTestZipFile);
            FileNameToCreate = Settings.Default.UnitTestZipFileNameToCreate;
            FileNameToDelete = Settings.Default.UnitTestZipFileNameToDelete;
            LinesToAddToNewFile = Settings.Default.UnitTestZipFileLinesToAddToNewFile.Split(',');
            FileToAddFullName = Path.Combine(CurrentAppPath, Settings.Default.UnitTestZipFileToAddFullName);
            DirToZip = Path.Combine(CurrentAppPath, Settings.Default.UnitTestZipFileFromDirectory);
            FileInDirExist = Settings.Default.UnitTestZipFileFromDirectoryFileExist;
            PromptZipExist = false;
            PromptFileExist = false;
            CompressionLevelMode = Settings.Default.UnitTestZipFileCompressionLevel;
            ZipMethods.CreateNewZipFile(ZipFilePath, FileToAddFullName, PromptFileExist, CompressionLevelMode);
            TestProperties.UnitTestLogger.LogInformation($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}(){Environment.NewLine}Start Test: ******************************************************************************************", true);
        }

        public CompressionLevel CompressionLevelMode { get; set; }
        public static string CurrentAppPath { get; set; }
        public static string CurrentUnitTestLogDirValue { get; set; }
        public string DirToZip { get; set; }
        public string FileInDirExist { get; set; }
        public string FileNameToCreate { get; set; }
        public string FileNameToDelete { get; set; }
        public string FileToAddFullName { get; set; }
        public string[] LinesToAddToNewFile { get; set; }
        public bool PromptFileExist { get; set; }
        public bool PromptZipExist { get; set; }
        public TestContext TestContext { get; set; }
        public string ZipFilePath { get; set; }

    }
}