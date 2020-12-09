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
// File Name        : MainWindow.xaml.cs
// Copyright        : (c) Computer Question. All rights reserved.
// <summary></summary>
// ***********************************************************************
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

namespace WPFAppWithDocFxLoggingAndZip2
{
    /// <summary>Class MainWindow</summary>
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
    public partial class MainWindow : Window
    {
        /// <summary>Gets or sets the factory.</summary>
        /// <value>The factory.</value>
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
        public static ILoggerFactory Factory { get; set; }
        /// <summary>Gets or sets the logger.</summary>
        /// <value>The logger.</value>
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
        public static ILogger Logger { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Window" /> class.</summary>
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
        public MainWindow()
        {
            InitializeComponent();
#if !DEBUG
            DumpExtensions.DisableDumping();
#endif
            string logFileName = AppName().Dump("AppName()");
            string currentLogDirValue = AppPath().Dump("AppPath()");
            Factory = new LoggerFactory()
            .AddFile(logFileName, currentLogDirValue).Dump("Factory");
            Logger = Factory.CreateLogger($"{logFileName}_{nameof(WPFAppWithDocFxLoggingAndZip2)}").Dump("Logger");
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
                //Sample Logger showing Dump Feature, This feature requires the Visual Dump Extension to be installed so the Visual Dump window is available or you will get a timeout error.
                Logger.LogDebug(message: $"Debug; Throwing new Exception.".Dump("Debug Message"), includeLineInfo: true.Dump("IncludeLineInfo"));
                throw new Exception("Example Exception.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, message: $"Error; Creating Log File for {logFileName}.",
                    includeLineInfo: true,
                    includeStackTrace: true);
            }
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

    }
}
