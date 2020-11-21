using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFAppWithDocFxAndLogging4
{
    /// <summary>
    ///    Class MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        public static ILoggerFactory Factory { get; set; }
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public static ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Window" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
#if !DEBUG
            DumpExtensions.DisableDumping();
#endif
            string logFileName = AppName();
            string currentLogDirValue = AppPath();
            //var myType = typeof(MainWindow);
            //var currentNamespace = myType.Namespace;
            //string name = nameof(WPFAppWithDocFxAndLogging4);
            if (currentLogDirValue.StartsWith(@"file:\"))
            {
                currentLogDirValue = currentLogDirValue[6..];
            }
            Factory = new LoggerFactory()
            .AddFile(logFileName, currentLogDirValue).Dump("Factory");
            Logger = Factory.CreateLogger($"{logFileName}_{nameof(WPFAppWithDocFxAndLogging4)}").Dump("Logger");
            Logger.LogInformation(message: $"Information; Created Log File for {logFileName}.",
                includeLineInfo: true);
            Logger.LogWarning(message: $"Warning; Creating Log File for {logFileName}.",
                includeLineInfo: true);
            Logger.LogTrace(message: $"Trace; Created Log File for {logFileName}.",
                includeLineInfo: true);
            Logger.LogDebug(message: $"Debug; Created Log File for {logFileName}.",
                includeLineInfo: true);
            try
            {
                throw new Exception("Example Exception.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, message: $"Error; Creating Log File for {logFileName}.",
                    includeLineInfo: true,
                    includeStackTrace: true);
            }
        }
        /// <summary>
        /// Applications the path.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string AppPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Dump("currentLogDirValue");

        /// <summary>
        /// Applications the name.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string AppName() => Assembly.GetExecutingAssembly().GetName().Name.Dump("logFileName");

    }
}
