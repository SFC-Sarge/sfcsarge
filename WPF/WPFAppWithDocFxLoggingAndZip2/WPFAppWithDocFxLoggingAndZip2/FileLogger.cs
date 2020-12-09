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
// File Name        : FileLogger.cs
// Copyright        : (c) Computer Question. All rights reserved.
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace WPFAppWithDocFxLoggingAndZip2
{
    /// <summary>Class FileLogger</summary>
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
    internal sealed class FileLogger : ILogger, IDisposable
    {
        /// <summary>The category</summary>
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
        private readonly string _category;
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
        private volatile StreamWriter _writer;

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
        public FileLogger()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FileLogger" /> class.</summary>
        /// <param name="category">The category.</param>
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
        public FileLogger(string category, StreamWriter writer)
        {
            _category = category;
            _writer = writer;
        }

        /// <summary>Begins a logical operation scope.</summary>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An <see cref="T:System.IDisposable" /> that ends the logical operation scope on dispose.</returns>
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
        /// <exception cref="NotImplementedException"></exception>
        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();

        /// <summary>Begins the scope.</summary>
        /// <typeparam name="TState">The type of the t state.</typeparam>
        /// <param name="state">The state.</param>
        /// <returns>IDisposable.</returns>
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
        /// <exception cref="NotImplementedException"></exception>
        public IDisposable BeginScope<TState>(object state) => throw new NotImplementedException();

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
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
        public void Dispose() => _writer = null;

        /// <summary>Checks if the given <paramref name="logLevel" /> is enabled.</summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
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
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>Logs the specified log level.</summary>
        /// <typeparam name="TState">The type of the t state.</typeparam>
        /// <param name="logLevel">The log level.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="state">The state.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatter">The formatter.</param>
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
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            StreamWriter writer = _writer;
            if (writer is null)
                return;
            string message = formatter(arg1: state, arg2: exception);
            lock (writer)
            {
                writer.WriteLine(value: $"[{DateTime.Now}] <{_category}> ({logLevel.ToString()[0]}):");
                writer.WriteLine(value: message);

                if (exception is not null)
                    writer.WriteLine(value: $"Exception: {exception}");

                writer.WriteLine();
                writer.Flush();
            }
        }
    }
}
