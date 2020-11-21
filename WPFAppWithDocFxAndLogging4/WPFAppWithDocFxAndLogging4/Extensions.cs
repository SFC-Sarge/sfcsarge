using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace WPFAppWithDocFxAndLogging4
{
    public static class Extensions
    {
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
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid.
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                if (child as T == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    // If the child's name is set for search
                    if (child as FrameworkElement != null && (child as FrameworkElement).Name == childName)
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
        public static T FindVisualParent<T>(Visual element) where T : Visual
        {
            Visual parent = element;
            while (parent != null)
            {
                if (parent as T != null)
                {
                    return parent as T;
                }

                parent = VisualTreeHelper.GetParent(parent) as Visual;
            }
            return null;
        }
        public static IEnumerable<DependencyObject> FindChildren(this DependencyObject obj)
        {
            return Enumerable.Range(0, VisualTreeHelper.GetChildrenCount(obj))
                .Select(i => VisualTreeHelper.GetChild(obj, i));
        }
        public static IEnumerable<T> SelectAllRecursively<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> func)
        {
            return (items ?? Enumerable.Empty<T>()).SelectMany(o => new[] { o }.Concat(SelectAllRecursively(func(o), func)));
        }
        public static IEnumerable<DependencyObject> FindAllChildren(this DependencyObject obj)
        {
            return obj.FindChildren().SelectAllRecursively(o => o.FindChildren());
        }
        public static DependencyObject FindChildControl<T>(DependencyObject control)
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                if (child != null && child is T)
                    return child;
                else
                    FindChildControl<T>(child);
            }
            return null;

        }
        public static DependencyObject FindParentControl<T>(DependencyObject control)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(control);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent;
        }
        public static XElement GetXElement(this XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);
            return xDoc.Root;
        }
        public static XmlNode GetXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }
        public static XDocument GetXDocument(this XmlDocument document)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                document.WriteTo(xmlWriter);
            XmlDeclaration decl = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
            if (decl != null)
            {
                xDoc.Declaration = new XDeclaration(decl.Version, decl.Encoding, decl.Standalone);
            }
            return xDoc;
        }
        public static XmlDocument GetXmlDocument(this XDocument document)
        {
            using (XmlReader xmlReader = document.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                if (document.Declaration != null)
                {
                    XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(document.Declaration.Version,
                        document.Declaration.Encoding, document.Declaration.Standalone);
                    xmlDoc.InsertBefore(dec, xmlDoc.FirstChild);
                }
                return xmlDoc;
            }
        }
        public static bool IsLetter(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
        public static bool IsLetterOrSeparator(this char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';
        public static bool IsNullOrEmpty(this string s)
        {
            return s == null || s.Trim().Length == 0;
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return !(list?.Any() ?? false);
        }
        public static bool NotNullAny<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable?.Any(predicate) == true;
        }
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data?.Any() == true;
        }
        public static int Compare<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            // If one of collection objects is null, use the default Comparer class
            // (null is considered to be less than any other object)
            if (first == null || second == null)
            {
                return Comparer.Default.Compare(first, second);
            }

            Comparer<T> elementComparer = Comparer<T>.Default;
            int compareResult;
            using (IEnumerator<T> firstEnum = first.GetEnumerator())
            using (IEnumerator<T> secondEnum = second.GetEnumerator())
            {
                do
                {
                    bool gotFirst = firstEnum.MoveNext();
                    bool gotSecond = secondEnum.MoveNext();
                    // Reached the end of collections => assume equal
                    if (!gotFirst && !gotSecond)
                    {
                        return 0;
                    }
                    // Different sizes => treat collection of larger size as "greater"
                    if (gotFirst != gotSecond)
                    {
                        return gotFirst ? 1 : -1;
                    }

                    compareResult = elementComparer.Compare(firstEnum.Current, secondEnum.Current);
                } while (compareResult == 0);
            }
            return compareResult;
        }
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
        public static string DecryptStringFromBytes(this byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
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
        public static void PerformClick(this Button btn)
        {
            btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
        {
            List<TSource> sorted = collection.OrderBy(keySelector).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
        private static readonly Action emptyDelegate = delegate () { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, emptyDelegate);
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static IEnumerable<T> Add<T>(this IEnumerable<T> collection, T item)
        {
            foreach (T currentItem in collection) yield return currentItem;
            yield return item;
        }
        public static void Add<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            for (int index = 0; index < range.Count(); index++) collection.Add(range.ElementAt(index));
        }
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> range)
        {
            foreach (T item in range) collection.Add(item);
        }
        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            foreach (var item in collection)
            {
                oc.Add(item);
            }

        }
        public static void AddUniqueIfNotEmpty(this ObservableCollection<string> collection, string text)
        {
            if (!string.IsNullOrEmpty(text) && !collection.Contains(text)) collection.Add(text);
        }
        public static void AppendUniqueOnNewLineIfNotEmpty(this StringBuilder stringBuilder, string text)
        {
            if (text.Trim().Length > 0 && !stringBuilder.ToString().Contains(text)) stringBuilder.AppendFormat("{0}{1}", stringBuilder.ToString().Trim().Length == 0 ? string.Empty : Environment.NewLine, text);
        }
        public static int Count(this System.Collections.IEnumerable collection)
        {
            int count = 0;
            foreach (object item in collection) count++;
            return count;
        }
        public static void FillWithMembers<T>(this ICollection<T> collection)
        {
            if (typeof(T).BaseType != typeof(Enum)) throw new ArgumentException("The FillWithMembers<T> method can only be called with an enum as the generic type.");
            collection.Clear();
            foreach (string name in Enum.GetNames(typeof(T))) collection.Add((T)Enum.Parse(typeof(T), name));
        }
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return Enum.GetName(value.GetType(), value);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            return Enum.GetName(value.GetType(), value);
        }
        public static void Remove<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            for (int index = 0; index < range.Count(); index++) collection.Remove(range.ElementAt(index));
        }
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> inputCollection)
        {
            return new ObservableCollection<T>(inputCollection);
        }

    }
}
