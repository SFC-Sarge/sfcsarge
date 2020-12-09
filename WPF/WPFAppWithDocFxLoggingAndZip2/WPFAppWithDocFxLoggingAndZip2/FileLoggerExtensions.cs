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
// File Name        : FileLoggerExtensions.cs
// Copyright        : (c) Computer Question. All rights reserved.
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace WPFAppWithDocFxLoggingAndZip2
{
    /// <summary>Class FileLoggerExtensions</summary>
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
    public static class FileLoggerExtensions
    {
        /// <summary>Adds the file.</summary>
        /// <param name="factory">The factory.</param>
        /// <param name="name">The name.</param>
        /// <param name="logFolder">The log folder.</param>
        /// <returns>ILoggerFactory.</returns>
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
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string name, string logFolder)
        {
            if (factory is null || name is null || logFolder is null)
                return null;

            factory.AddProvider(provider: new FileLoggerProvider(name: name, logFolder: logFolder));
            return factory;
        }

        /// <summary>Logs the specified log level.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
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
        public static void Log(this ILogger logger, LogLevel logLevel, string message, params object[] args) => logger.Log(logLevel: logLevel, eventId: 0, exception: null, message: message, args: args);

        /// <summary>Logs the specified log level.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="includeStackTrace">if set to <c>true</c> [include stack trace].</param>
        /// <param name="args">The arguments.</param>
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
        public static void Log(this ILogger logger, LogLevel logLevel, Exception exception, string message, bool includeStackTrace = false, params object[] args)
        {
            if (includeStackTrace)
                message += $"{Environment.NewLine}Stack Trace: {exception.StackTrace}";
            logger.Log(logLevel: logLevel, eventId: 0, exception: exception, message: message, args: args);
        }

        /// <summary>Logs the error.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="includeLineInfo">if set to <c>true</c> [include line information].</param>
        /// <param name="includeStackTrace">if set to <c>true</c> [include stack trace].</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
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
        public static void LogError(this ILogger logger, Exception exception,
        string message, bool includeLineInfo, bool includeStackTrace,
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
        {
            if (callerMemberName is not null && includeLineInfo != false)
                message += $"{Environment.NewLine}Project Source: {exception.Source}{Environment.NewLine}Caller Member Name: {callerMemberName}{Environment.NewLine}Caller File Path: {callerFilePath}{Environment.NewLine}Caller Line Number: {callerLineNumber}";
            Log(logger: logger, logLevel: LogLevel.Error, exception: exception, message: message, includeStackTrace: includeStackTrace, args: null);
        }

        /// <summary>Logs the information.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="includeLineInfo">if set to <c>true</c> [include line information].</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
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
        public static void LogInformation(this ILogger logger,
        string message, bool includeLineInfo,
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
        {
            if (callerMemberName is not null && includeLineInfo != false)
                message += $"{Environment.NewLine}Caller Member Name: {callerMemberName}{Environment.NewLine}Caller File Path: {callerFilePath}{Environment.NewLine}Caller Line Number: {callerLineNumber}";

            Log(logger: logger, logLevel: LogLevel.Information, message: message, args: null);
        }

        /// <summary>Logs the trace.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="includeLineInfo">if set to <c>true</c> [include line information].</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
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
        public static void LogTrace(this ILogger logger,
        string message, bool includeLineInfo,
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
        {
            if (callerMemberName is not null && includeLineInfo != false)
                message += $"{Environment.NewLine}Caller Member Name: {callerMemberName}{Environment.NewLine}Caller File Path: {callerFilePath}{Environment.NewLine}Caller Line Number: {callerLineNumber}";

            Log(logger: logger, logLevel: LogLevel.Trace, message: message, args: null);
        }

        /// <summary>Logs the debug.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="includeLineInfo">if set to <c>true</c> [include line information].</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
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
        public static void LogDebug(this ILogger logger,
        string message, bool includeLineInfo,
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
        {
            if (callerMemberName is not null && includeLineInfo != false)
                message += $"{Environment.NewLine}Caller Member Name: {callerMemberName}{Environment.NewLine}Caller File Path: {callerFilePath}{Environment.NewLine}Caller Line Number: {callerLineNumber}";

            Log(logger: logger, logLevel: LogLevel.Debug, message: message, args: null);
        }

        /// <summary>Logs the warning.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="includeLineInfo">if set to <c>true</c> [include line information].</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
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
        public static void LogWarning(this ILogger logger,
        string message, bool includeLineInfo,
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
        {
            if (callerMemberName is not null && includeLineInfo != false)
                message += $"{Environment.NewLine}Caller Member Name: {callerMemberName}{Environment.NewLine}Caller File Path: {callerFilePath}{Environment.NewLine}Caller Line Number: {callerLineNumber}";

            Log(logger: logger, logLevel: LogLevel.Warning, message: message, args: null);
        }
    }
}
