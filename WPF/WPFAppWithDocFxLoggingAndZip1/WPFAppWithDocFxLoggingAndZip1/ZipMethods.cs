//***********************************************************************
// Assembly         : WPFAppWithDocFxLoggingAndZip1
// Author UserID    : danny.mcnaught
// Author Full Name : Danny C. McNaught
// Author Phone     : 1-919-239-3306
// Created          : 12-08-2020
//
// Created By       : Danny C. McNaught
// Last Modified By : Danny C. McNaught
// Last Modified On : 12-08-2020
// Change Request # :
// Version Number   :
// Description      :
// File Name        : ZipMethods.cs
// Copyright        : (c) Computer Question. All rights reserved.
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace WPFAppWithDocFxLoggingAndZip1
{
    /// <summary>Class ZipMethods</summary>
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
    /// <description><b>Code Change Date and Time:</b><para>Tuesday, December 8, 2020 18:35</para><b>Code Changes:</b><para>Created XML Comment</para></description>
    /// </item>
    /// </list>
    /// </remarks>
    public static class ZipMethods
    {
        /// <summary>Creates the new file in zip.</summary>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <param name="fileNameToCreate">The file name of new file to create in zip.</param>
        /// <param name="linesToAddToNewFile">The lines to add to new file in zip.</param>
        /// <param name="promptFileExist">Prompt if file exist.</param>
        /// <param name="compressionLevel">The compression level.</param>
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
        /// <description><b>Code Change Date and Time:</b><para>Tuesday, December 8, 2020 18:35</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static void CreateNewFileInZip(string zipFilePath, string fileNameToCreate, string[] linesToAddToNewFile, bool promptFileExist = true, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            if (zipFilePath is null || fileNameToCreate is null || (linesToAddToNewFile is null) || (linesToAddToNewFile.Length == 0))
                return;
            if (!File.Exists(zipFilePath))
            {
                if (promptFileExist)
                {
                    MessageBoxResult myResult = MessageBox.Show($"The file: {zipFilePath} does not exist!{Environment.NewLine}{Environment.NewLine}Do you want to create it?", $"{zipFilePath} not found", MessageBoxButton.OKCancel, MessageBoxImage.Error, MessageBoxResult.Cancel, MessageBoxOptions.ServiceNotification);
                        if (myResult is MessageBoxResult.Cancel)
                            return;
                }
            }
            using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
            {
                ZipArchiveEntry result = archive.GetEntry(fileNameToCreate);
                if (result is not null && result.Name == fileNameToCreate)
                {
                    if (promptFileExist)
                    {
                        MessageBoxResult myResult = MessageBox.Show($"The file: {fileNameToCreate} already exists!{Environment.NewLine}{Environment.NewLine}Do you want to replace it?", $"{fileNameToCreate} exists", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.ServiceNotification);
                        if (myResult != MessageBoxResult.Yes)
                            return;
                    }
                    result.Delete();
                }
            }
            using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Open))
            {
                using ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
                ZipArchiveEntry newFileEntry = archive.CreateEntry(fileNameToCreate, compressionLevel);
                using StreamWriter writer = new StreamWriter(newFileEntry.Open());
                foreach (string line in linesToAddToNewFile)
                    writer.WriteLine(line);
            }
        }
        /// <summary>Adds the file to zip.</summary>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <param name="fileToAddFullName">Full name of the file to add.</param>
        /// <param name="promptZipExist">Prompt if zip file exist.</param>
        /// <param name="promptFileExist">Prompt if file exist.</param>
        /// <param name="compressionLevel">The compression level.</param>
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
        /// <description><b>Code Change Date and Time:</b><para>Tuesday, December 8, 2020 18:35</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static void AddFileToZip(string zipFilePath, string fileToAddFullName, bool promptZipExist = true, bool promptFileExist = true, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            if (zipFilePath is null || fileToAddFullName is null)
                return;
            if (!File.Exists(zipFilePath))
            {
                if (promptFileExist)
                {
                    MessageBoxResult myResult = MessageBox.Show($"The file: {zipFilePath} does not exist!", $"{zipFilePath} not found", MessageBoxButton.OKCancel, MessageBoxImage.Error, MessageBoxResult.Cancel, MessageBoxOptions.ServiceNotification);
                        if (myResult is MessageBoxResult.OK or MessageBoxResult.Cancel)
                            return;
                }
            }
            string fileToAddName = Path.GetFileName(fileToAddFullName);
            //Add existing file to Zip.
            using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
            {
                ZipArchiveEntry result = archive.GetEntry(fileToAddName);
                if (result is not null && result.Name == fileToAddName)
                {
                    if (promptFileExist)
                    {
                        MessageBoxResult myResult = MessageBox.Show($"The file: {fileToAddName} already exists!{Environment.NewLine}{Environment.NewLine}Do you want to replace it?", $"{fileToAddName} exists", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.ServiceNotification);
                        if (myResult != MessageBoxResult.Yes)
                            return;
                    }
                    result.Delete();
                }
            }
            using FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Open);
            {
                using ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
                ZipArchiveEntry existingFileEntry = archive.CreateEntryFromFile(fileToAddFullName, fileToAddName, compressionLevel);
            }
        }
        /// <summary>Creates the zip from dir.</summary>
        /// <param name="DirToZip">The existing directory to zip.</param>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <param name="promptFileExist">Prompt if file exist.</param>
        /// <param name="compressionLevel">The compression level.</param>
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
        /// <description><b>Code Change Date and Time:</b><para>Tuesday, December 8, 2020 18:35</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static void CreateZipFromDir(string DirToZip, string zipFilePath, bool promptFileExist = true, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            if (DirToZip is null || zipFilePath is null || !Directory.Exists(DirToZip))
                return;
            if (File.Exists(zipFilePath))
            {
                if (promptFileExist)
                {
                    MessageBoxResult myResult = MessageBox.Show($"The file: {zipFilePath} exists!{Environment.NewLine}{Environment.NewLine}Do you want to replace it?", $"{zipFilePath} exists", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel, MessageBoxOptions.ServiceNotification);
                    if (!(myResult is MessageBoxResult.OK))
                        return;
                }
                File.Delete(zipFilePath);
            }
            ZipFile.CreateFromDirectory(DirToZip, zipFilePath, compressionLevel, false);
        }
        /// <summary>Deletes the file in zip.</summary>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <param name="fileNameToDelete">The file name in the zip file to delete.</param>
        /// <param name="promptFileExist">Prompt if file exist.</param>
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
        /// <description><b>Code Change Date and Time:</b><para>Tuesday, December 8, 2020 18:35</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static void DeleteFileInZip(string zipFilePath, string fileNameToDelete, bool promptFileExist = true)
        {
            if (zipFilePath is null || fileNameToDelete is null)
                return;
            using ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update);
            ZipArchiveEntry result = archive.GetEntry(fileNameToDelete);
            if (result is not null && result.Name == fileNameToDelete)
            {
                if (promptFileExist)
                {
                    MessageBoxResult myResult = MessageBox.Show($"The file: {fileNameToDelete} exists!{Environment.NewLine}{Environment.NewLine}Do you want to Delete it?", $"{fileNameToDelete} exists", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
                    if (myResult != MessageBoxResult.Yes)
                        return;
                }
                result.Delete();
            }
        }
        /// <summary>Creates the new zip file.</summary>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <param name="fileToAddFullName">Full name of the file to add.</param>
        /// <param name="promptZipExist">Prompt if zip file exist.</param>
        /// <param name="compressionLevel">The compression level.</param>
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
        /// <description><b>Code Change Date and Time:</b><para>Tuesday, December 8, 2020 18:35</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static void CreateNewZipFile(string zipFilePath, string fileToAddFullName, bool promptZipExist = true, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            if (string.IsNullOrEmpty(zipFilePath) || string.IsNullOrEmpty(fileToAddFullName))
                return;
            if (File.Exists(zipFilePath))
            {
                if (promptZipExist)
                {
                    MessageBoxResult myResult = MessageBox.Show($"The file: {zipFilePath} exists!{Environment.NewLine}{Environment.NewLine}Do you want to replace it?", $"File {zipFilePath} found", MessageBoxButton.OKCancel, MessageBoxImage.Error, MessageBoxResult.Cancel, MessageBoxOptions.ServiceNotification);
                    if (!(myResult is MessageBoxResult.OK))
                        return;
                }
                File.Delete(zipFilePath);
            }
            using FileStream zipToOpen = new FileStream(zipFilePath, FileMode.CreateNew);
            using ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
            string fileToAddName = Path.GetFileName(fileToAddFullName);
            ZipArchiveEntry existingFileEntry = archive.CreateEntryFromFile(fileToAddFullName, fileToAddName, compressionLevel);
        }
        /// <summary>Files the exist in zip.</summary>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <param name="fileNameToFind">The file name to find.</param>
        /// <param name="promptFileExist">The prompt file exist.</param>
        /// <returns>bool.</returns>
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
        /// <description><b>Code Change Date and Time:</b><para>Tuesday, December 8, 2020 18:35</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static bool FileExistInZip(string zipFilePath, string fileNameToFind, bool promptFileExist = true)
        {            
            if (zipFilePath is null || fileNameToFind is null)
                return false;
            using ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update);
            ZipArchiveEntry result = archive.GetEntry(fileNameToFind);
            if (result is not null && result.Name == fileNameToFind)
            {
                //Note: MessageBox.Show does not work on GitHub Codespaces.
                if (promptFileExist)
                {
                    MessageBoxResult myResult = MessageBox.Show($"The file: {fileNameToFind} exists!{Environment.NewLine}{Environment.NewLine}", $"{fileNameToFind} exists", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                    if (myResult != MessageBoxResult.OK)
                        return true;
                }
                return true;
            }
            return false;
        }
    }
}
