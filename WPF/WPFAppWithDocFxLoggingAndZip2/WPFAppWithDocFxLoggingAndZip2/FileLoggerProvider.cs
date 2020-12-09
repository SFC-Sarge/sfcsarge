//***********************************************************************
// Assembly         : WPFAppWithDocFxLoggingAndZip2
// Author UserID    : danny.mcnaught
// Author Full Name : Danny C. McNaught
// Author Phone     : 1-919-239-3306
// Created          : 12-09-2020
//
// Created By       : Danny C. McNaught
// Last Modified By : Danny C. McNaught
// Last Modified On : 12-09-2020
// Change Request # :
// Version Number   :
// Description      :
// File Name        : FileLoggerProvider.cs
// Copyright        : (c) Computer Question. All rights reserved.
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using static System.FormattableString;

namespace WPFAppWithDocFxLoggingAndZip2
{
    /// <summary>Class FileLoggerProvider</summary>
    /// <remarks>
    /// <para><b>History:</b></para>
    /// <list type="table">
    /// <item>
    /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
    /// </item>
    /// <item>
    /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
    /// </item>
    /// <item>
    /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
    /// </item>
    /// </list>
    /// </remarks>
    public sealed class FileLoggerProvider : ILoggerProvider
    {
        /// <summary>The writer</summary>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        private readonly StreamWriter _writer;
        /// <summary>The loggers</summary>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        private readonly List<FileLogger> _loggers = new List<FileLogger>();

        /// <summary>Initializes a new instance of the <see cref="FileLoggerProvider" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="logFolder">The log folder.</param>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public FileLoggerProvider(string name = null, string logFolder = null)
            : this(File.CreateText(GetLogFileName(name, logFolder)))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FileLoggerProvider" /> class.</summary>
        /// <param name="writer">The writer.</param>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public FileLoggerProvider(StreamWriter writer) => _writer = writer;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public FileLoggerProvider()
        {
        }

        /// <summary>Gets the name of the log file.</summary>
        /// <param name="name">The name.</param>
        /// <param name="logFolder">The log folder.</param>
        /// <returns>System.String.</returns>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static string GetLogFileName(string name, string logFolder)
        {
            if (string.IsNullOrEmpty(logFolder))
                logFolder = Path.GetTempPath();
            return Path.Combine(logFolder, Invariant($@"{name}_{FileVersionInfo.GetVersionInfo($@"{AppPath()}\{AppName()}.exe").FileVersion}_{DateTime.Now:yyyyMdd_HHmmss}_pid{Process.GetCurrentProcess().Id}.log"));
        }

        /// <summary>Applications the path.</summary>
        /// <returns>System.String.</returns>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static string AppPath() => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>Applications the name.</summary>
        /// <returns>System.String.</returns>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static string AppName() => Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);

        /// <summary>Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.</summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns>The instance of <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.</returns>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public ILogger CreateLogger(string categoryName)
        {
            var logger = new FileLogger(categoryName, _writer);
            _loggers.Add(logger);
            return logger;
        }

        /// <summary>Disposes this instance.</summary>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>SURFACE-PRO-LTE</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public void Dispose()
        {
            foreach (var logger in _loggers)
                logger.Dispose();
            _writer.Dispose();
        }
    }
}
