using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using static System.FormattableString;

namespace WpfAppDocFxWLoggingZipTestProject1
{
    /// <summary>Class FileLoggerProvider</summary>
    public sealed class FileLoggerProvider : ILoggerProvider
    {
        /// <summary>The writer</summary>
        private readonly StreamWriter _writer;
        /// <summary>The loggers</summary>
        private readonly List<FileLogger> _loggers = new();

        /// <summary>Initializes a new instance of the <see cref="FileLoggerProvider" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="logFolder">The log folder.</param>
        public FileLoggerProvider(string name = null, string logFolder = null) : this(File.CreateText(Path.Combine(logFolder, Invariant($@"{name}_{FileVersionInfo.GetVersionInfo($@"{AppPath()}\{AppName()}.exe").FileVersion}_{DateTime.Now:yyyyMdd_HHmmss}_pid{Environment.ProcessId}.log"))))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FileLoggerProvider" /> class.</summary>
        /// <param name="writer">The writer.</param>
        public FileLoggerProvider(StreamWriter writer) => _writer = writer;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public FileLoggerProvider()
        {
        }

        /// <summary>Gets the name of the log file.</summary>
        /// <param name="name">The name.</param>
        /// <param name="logFolder">The log folder.</param>
        /// <returns>System.String.</returns>
        public static string GetLogFileName(string name, string logFolder) =>
            Path.Combine(logFolder, Invariant($@"{name}_{FileVersionInfo.GetVersionInfo($@"{AppPath()}\{AppName()}.exe").FileVersion}_{DateTime.Now:yyyyMdd_HHmmss}_pid{Environment.ProcessId}.log"));

        /// <summary>Applications the path.</summary>
        /// <returns>System.String.</returns>
        public static string AppPath() => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>Applications the name.</summary>
        /// <returns>System.String.</returns>
        public static string AppName() => Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);

        /// <summary>Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.</summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns>The instance of <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            FileLogger logger = new(categoryName, _writer);
            _loggers.Add(logger);
            return logger;
        }

        /// <summary>Disposes this instance.</summary>
        public void Dispose()
        {
            foreach (var logger in _loggers)
                logger.Dispose();
            _writer.Dispose();
        }
    }
}
