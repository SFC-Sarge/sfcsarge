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

namespace WPFAppWithDocFxAndLogging15
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
            string logFileName = AppName();
            string currentLogDirValue = AppPath();
            //var myType = typeof(MainWindow);
            //var currentNamespace = myType.Namespace;
            //string name = nameof(WPFAppWithDocFxAndLogging15);
            if (currentLogDirValue.StartsWith(@"file:\"))
            {
                currentLogDirValue = currentLogDirValue[6..];
            }
            Factory = new LoggerFactory()
            .AddFile(logFileName, currentLogDirValue);
            Logger = Factory.CreateLogger($"{logFileName}_{nameof(WPFAppWithDocFxAndLogging15)}");
            Logger.LogInformation(message: $"{ShowDebugInfo()}Created Log File for {logFileName}.");

        }
        /// <summary>
        /// Applications the path.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string AppPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        /// <summary>
        /// Applications the name.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string AppName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }
        /// <summary>
        /// Shows the debug information scan form. Test Commit to Github Codespaces
        /// </summary>
        /// <returns>System.String.</returns>
        public static string ShowDebugInfo()
        {
            try
            {
                return $"{Environment.NewLine}Current Code line Number: {(new StackFrame(skipFrames: 1, needFileInfo: true).GetFileLineNumber())}:{Environment.NewLine}Method Name: {(new StackFrame(skipFrames: 1, needFileInfo: true).GetMethod())}{Environment.NewLine}Class File Name: {(new StackFrame(skipFrames: 1, needFileInfo: true).GetFileName().Substring(new StackFrame(skipFrames: 1, needFileInfo: true).GetFileName().LastIndexOf(@"\", StringComparison.CurrentCulture) + 1, new StackFrame(skipFrames: 1, needFileInfo: true).GetFileName().Length - new StackFrame(skipFrames: 1, needFileInfo: true).GetFileName().LastIndexOf(@"\", StringComparison.CurrentCulture) - 1))}{Environment.NewLine}";
            }
            catch (NullReferenceException ex)
            {
                return $"{Environment.NewLine}StackFrame Exception Thrown: {ex.GetType()} {ex.StackTrace} {ex.Data} {ex.InnerException} {ex.Source} {ex.Message}{Environment.NewLine}";
            }
        }

    }
}
