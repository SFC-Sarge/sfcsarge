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
// File Name        : Extensions.cs
// Copyright        : (c) Computer Question. All rights reserved.
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace WPFAppWithDocFxLoggingAndZip2
{
    /// <summary>Class Extensions</summary>
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
    public static class Extensions
    {
        /// <summary>Skips the exceptions.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;T&gt;.</returns>
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
        public static IEnumerable<T> SkipExceptions<T>(this IEnumerable<T> values)
        {
            using IEnumerator<T> enumerator = values.GetEnumerator();
            bool next = true;
            while (next)
            {
                try
                {
                    next = enumerator.MoveNext();
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
                if (next) yield return enumerator.Current;
            }
        }
        /// <summary>Finds the child.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="childName">Name of the child.</param>
        /// <returns>T.</returns>
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
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid.
            if (parent is null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                if (child as T is null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild is not null) break;
                }
                else if (childName is not null)
                {
                    // If the child's name is set for search
                    if (child as FrameworkElement is not null && (child as FrameworkElement).Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
        /// <summary>Finds the visual parent.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <returns>T.</returns>
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
        public static T FindVisualParent<T>(Visual element) where T : Visual
        {
            Visual parent = element;
            while (parent is not null)
            {
                if (parent as T is not null)
                {
                    return parent as T;
                }

                parent = VisualTreeHelper.GetParent(parent) as Visual;
            }
            return null;
        }
        /// <summary>Finds the children.</summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;System.Windows.DependencyObject&gt;.</returns>
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
        public static IEnumerable<DependencyObject> FindChildren(this DependencyObject obj) => Enumerable.Range(0, VisualTreeHelper.GetChildrenCount(obj))
                .Select(i => VisualTreeHelper.GetChild(obj, i));
        /// <summary>Selects all recursively.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="func">The function.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;T&gt;.</returns>
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
        public static IEnumerable<T> SelectAllRecursively<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> func) => (items ?? Enumerable.Empty<T>()).SelectMany(o => new[] { o }.Concat(SelectAllRecursively(func(o), func)));
        /// <summary>Finds all children.</summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;System.Windows.DependencyObject&gt;.</returns>
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
        public static IEnumerable<DependencyObject> FindAllChildren(this DependencyObject obj) => obj.FindChildren().SelectAllRecursively(o => o.FindChildren());
        /// <summary>Finds the child control.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">The control.</param>
        /// <returns>System.Windows.DependencyObject.</returns>
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
        public static DependencyObject FindChildControl<T>(DependencyObject control)
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                if (child is not null && child is T)
                    return child;
                else
                    FindChildControl<T>(child);
            }
            return null;

        }
        /// <summary>Finds the parent control.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">The control.</param>
        /// <returns>System.Windows.DependencyObject.</returns>
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
        public static DependencyObject FindParentControl<T>(DependencyObject control)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(control);
            while (parent is not null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent;
        }
        /// <summary>Gets the x element.</summary>
        /// <param name="node">The node.</param>
        /// <returns>System.Xml.Linq.XElement.</returns>
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
        public static XElement GetXElement(this XmlNode node)
        {
            XDocument xDoc = new();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);
            return xDoc.Root;
        }
        /// <summary>Gets the XML node.</summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Xml.XmlNode.</returns>
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
        public static XmlNode GetXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }
        /// <summary>Gets the x document.</summary>
        /// <param name="document">The document.</param>
        /// <returns>System.Xml.Linq.XDocument.</returns>
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
        public static XDocument GetXDocument(this XmlDocument document)
        {
            XDocument xDoc = new();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                document.WriteTo(xmlWriter);
            XmlDeclaration decl = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
            if (decl is not null)
            {
                xDoc.Declaration = new XDeclaration(decl.Version, decl.Encoding, decl.Standalone);
            }
            return xDoc;
        }
        /// <summary>Gets the XML document.</summary>
        /// <param name="document">The document.</param>
        /// <returns>System.Xml.XmlDocument.</returns>
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
        public static XmlDocument GetXmlDocument(this XDocument document)
        {
            using (XmlReader xmlReader = document.CreateReader())
            {
                XmlDocument xmlDoc = new();
                xmlDoc.Load(xmlReader);
                if (document.Declaration is not null)
                {
                    XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(document.Declaration.Version,
                        document.Declaration.Encoding, document.Declaration.Standalone);
                    xmlDoc.InsertBefore(dec, xmlDoc.FirstChild);
                }
                return xmlDoc;
            }
        }
        /// <summary>Determines whether the specified c is letter.</summary>
        /// <param name="c">The c.</param>
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
        public static bool IsLetter(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
        /// <summary>Determines whether [is letter or separator] [the specified c].</summary>
        /// <param name="c">The c.</param>
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
        public static bool IsLetterOrSeparator(this char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';
        /// <summary>Determines whether [is null or empty] [the specified s].</summary>
        /// <param name="s">The s.</param>
        /// <remarks><para>
        ///   <b>History:</b>
        /// </para>
        /// <list type="table">
        ///   <item>
        ///     <description>
        ///       <b>Code Changed by:</b>
        ///       <para>Danny C. McNaught</para>
        ///       <para>
        ///         <para>
        ///           <a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a>
        ///         </para>
        ///       </para>
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       <b>Code changed on Visual Studio Host Machine:</b>
        ///       <para>DESKTOP-ACLFE3O</para>
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       <b>Code Change Date and Time:</b>
        ///       <para>Monday, November 23, 2020 12:41 PM</para>
        ///       <b>Code Changes:</b>
        ///       <para>Created XML Comment</para>
        ///     </description>
        ///   </item>
        /// </list></remarks>
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
        public static bool IsNullOrEmpty(this string s) => s is null || s.Trim().Length == 0;
        /// <summary>Determines whether [is null or empty] [the specified list].</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
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
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) => !(list?.Any() ?? false);
        /// <summary>Nots the null any.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="predicate">The predicate.</param>
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
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, December 9, 2020 13:09</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static bool NotNullAny<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) => enumerable?.Any(predicate) == true;
        /// <summary>Determines whether the specified data is any.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
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
        public static bool IsAny<T>(this IEnumerable<T> data) => data?.Any() == true;
        /// <summary>Compares the specified first.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>int.</returns>
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
        public static int Compare<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            // If one of collection objects is null, use the default Comparer class
            // (null is considered to be less than any other object)
            if (first is null || second is null)
                return Comparer.Default.Compare(first, second);

            Comparer<T> elementComparer = Comparer<T>.Default;
            int compareResult;
            using (IEnumerator<T> firstEnum = first.GetEnumerator())
            using (IEnumerator<T> secondEnum = second.GetEnumerator())
                do
                {
                    bool gotFirst = firstEnum.MoveNext();
                    bool gotSecond = secondEnum.MoveNext();
                    // Reached the end of collections => assume equal
                    if (!gotFirst && !gotSecond)
                        return 0;
                    // Different sizes => treat collection of larger size as "greater"
                    if (gotFirst != gotSecond)
                        return gotFirst ? 1 : -1;

                    compareResult = elementComparer.Compare(firstEnum.Current, secondEnum.Current);
                } while (compareResult == 0);
            return compareResult;
        }
        /// <summary>Encrypts the string to bytes.</summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="Key">The key.</param>
        /// <param name="IV">The iv.</param>
        /// <returns>byte[].</returns>
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
        public static byte[] EncryptStringToBytes(this string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        /// <summary>Decrypts the string from bytes.</summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="Key">The key.</param>
        /// <param name="IV">The iv.</param>
        /// <returns>string.</returns>
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
        /// <exception cref="ArgumentNullException">cipherText</exception>
        public static string DecryptStringFromBytes(this byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText is null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
        /// <summary>Performs the click.</summary>
        /// <param name="btn">The BTN.</param>
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
        public static void PerformClick(this Button btn) => btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        /// <summary>Sorts the specified collection.</summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="keySelector">The key selector.</param>
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
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
        {
            List<TSource> sorted = collection.OrderBy(keySelector).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
        /// <summary>The empty delegate</summary>
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
        private static readonly Action emptyDelegate = delegate () { };
        /// <summary>Refreshes the specified UI element.</summary>
        /// <param name="uiElement">The UI element.</param>
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
        public static void Refresh(this UIElement uiElement) => uiElement.Dispatcher.Invoke(DispatcherPriority.Render, emptyDelegate);
        /// <summary>Distincts the by.</summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;TSource&gt;.</returns>
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
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
        /// <summary>Adds the specified collection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="item">The item.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;T&gt;.</returns>
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
        public static IEnumerable<T> Add<T>(this IEnumerable<T> collection, T item)
        {
            foreach (T currentItem in collection) yield return currentItem;
            yield return item;
        }
        /// <summary>Adds the specified collection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="range">The range.</param>
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
        public static void Add<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            for (int index = 0; index < range.Count(); index++) collection.Add(range.ElementAt(index));
        }
        /// <summary>Adds the range.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="range">The range.</param>
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
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> range)
        {
            foreach (T item in range) collection.Add(item);
        }
        /// <summary>Adds the range.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oc">The oc.</param>
        /// <param name="collection">The collection.</param>
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
        /// <exception cref="ArgumentNullException">collection</exception>
        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException("collection");
            }
            foreach (var item in collection)
            {
                oc.Add(item);
            }

        }
        /// <summary>Adds the unique if not empty.</summary>
        /// <param name="collection">The collection.</param>
        /// <param name="text">The text.</param>
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
        public static void AddUniqueIfNotEmpty(this ObservableCollection<string> collection, string text)
        {
            if (!string.IsNullOrEmpty(text) && !collection.Contains(text)) collection.Add(text);
        }
        /// <summary>Appends the unique on new line if not empty.</summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="text">The text.</param>
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
        public static void AppendUniqueOnNewLineIfNotEmpty(this StringBuilder stringBuilder, string text)
        {
            if (text.Trim().Length > 0 && !stringBuilder.ToString().Contains(text)) stringBuilder.AppendFormat("{0}{1}", stringBuilder.ToString().Trim().Length == 0 ? string.Empty : Environment.NewLine, text);
        }
        /// <summary>Counts the specified collection.</summary>
        /// <param name="collection">The collection.</param>
        /// <returns>int.</returns>
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
        public static int Count(this System.Collections.IEnumerable collection)
        {
            int count = 0;
            foreach (object item in collection) count++;
            return count;
        }
        /// <summary>Fills the with members.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
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
        /// <exception cref="ArgumentException">The FillWithMembers<T> method can only be called with an enum as the generic type.</exception>
        public static void FillWithMembers<T>(this ICollection<T> collection)
        {
            if (typeof(T).BaseType != typeof(Enum)) throw new ArgumentException("The FillWithMembers<T> method can only be called with an enum as the generic type.");
            collection.Clear();
            foreach (string name in Enum.GetNames(typeof(T))) collection.Add((T)Enum.Parse(typeof(T), name));
        }
        /// <summary>Gets the description.</summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
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
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo is null) return Enum.GetName(value.GetType(), value);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes is not null && attributes.Length > 0) return attributes[0].Description;
            return Enum.GetName(value.GetType(), value);
        }
        /// <summary>Removes the specified collection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="range">The range.</param>
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
        public static void Remove<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            for (int index = 0; index < range.Count(); index++) collection.Remove(range.ElementAt(index));
        }
        /// <summary>To the observable collection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputCollection">The input collection.</param>
        /// <returns>System.Collections.ObjectModel.ObservableCollection&lt;T&gt;.</returns>
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
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> inputCollection) => new ObservableCollection<T>(inputCollection);
    }
}
