using Microsoft.Extensions.Logging;
using System;

namespace WpfAppDocFxWLoggingZipTestProject1
{
    public partial class TestProperties
    {
        public static WpftestPropertiesValues WpftestPropertiesValues = new();

        public static ILoggerFactory UnitTestFactory { get; set; } = new LoggerFactory()
            .AddFile(Settings.Default.UnitTestLogFileName, Settings.Default.CurrentUnitTestLogDirValue);
        public static ILogger UnitTestLogger { get; set; } = UnitTestFactory.CreateLogger(Settings.Default.UnitTestLogFileName);

        public TestProperties()
        {
        }

    }
}
