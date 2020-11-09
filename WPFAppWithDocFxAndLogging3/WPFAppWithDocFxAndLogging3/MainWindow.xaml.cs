//***********************************************************************
// Assembly         : WPFAppWithDocFxAndLogging3
// Author UserID    : danny.mcnaught
// Author Full Name : Danny C. McNaught
// Author Phone     : 1-919-239-3306
// Created          : 11-09-2020
//
// Created By       : Danny C. McNaught
// Last Modified By : Danny C. McNaught
// Last Modified On : 11-09-2020
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

namespace WPFAppWithDocFxAndLogging3
{
    /// <summary>Class MainWindow Test</summary>
    /// <remarks>
    /// <para><b>History:</b></para>
    /// <list type="table">
    /// <item>
    /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
    /// </item>
    /// <item>
    /// <description><b>Code changed on Visual Studio Host Machine:</b><para>DESKTOP-ACLFE3O</para></description>
    /// </item>
    /// <item>
    /// <description><b>Code Change Date and Time:</b><para>Monday, November 9, 2020 11:20 AM</para><b>Code Changes:</b><para>Created XML Comment</para></description>
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
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>DESKTOP-ACLFE3O</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Monday, November 9, 2020 11:20 AM</para><b>Code Changes:</b><para>Created XML Comment</para></description>
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
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>DESKTOP-ACLFE3O</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Monday, November 9, 2020 11:20 AM</para><b>Code Changes:</b><para>Created XML Comment</para></description>
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
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>DESKTOP-ACLFE3O</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Monday, November 9, 2020 11:20 AM</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public MainWindow()
        {
            InitializeComponent();
            string logFileName = AppName();
            string currentLogDirValue = AppPath();
            //var myType = typeof(MainWindow);
            //var currentNamespace = myType.Namespace;
            //string name = nameof(WPFAppWithDocFxAndLogging3);
            if (currentLogDirValue.StartsWith(@"file:\"))
            {
                currentLogDirValue = currentLogDirValue[6..];
            }
            Factory = new LoggerFactory()
            .AddFile(logFileName, currentLogDirValue);
            Logger = Factory.CreateLogger($"{logFileName}_{nameof(WPFAppWithDocFxAndLogging3)}");
            Logger.LogInformation(message: $"{ShowDebugInfo()}Created Log File for {logFileName}.");

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
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>DESKTOP-ACLFE3O</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Monday, November 9, 2020 11:20 AM</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static string AppPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        /// <summary>Applications the name.</summary>
        /// <returns>System.String.</returns>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>DESKTOP-ACLFE3O</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Monday, November 9, 2020 11:20 AM</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static string AppName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }
        /// <summary>Shows the debug information. Test Update.</summary>
        /// <returns>System.String.</returns>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>DESKTOP-ACLFE3O</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Monday, November 9, 2020 11:20 AM</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
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

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello", "You Clicked Me.", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }
    }
}
