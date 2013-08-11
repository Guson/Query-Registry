// -----------------------------------------------------------------------
// <copyright file="QueryRegistryKey.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.IO;
    using System.Security;
    using Microsoft.Win32;

    /// <summary>
    /// Query the registry, with optional filtering.
    /// </summary>
    /// <remarks>
    /// Class Diagram:
    /// <p><img src="..\images\QueryRegistryKey.png" alt="QueryRegistryKey Class Diagram" /></p>
    /// References:
    /// <ul>
    ///   <li><see href="http://en.wikipedia.org/wiki/Windows_Registry#.REG_files">.REG files (also known as Registration entries)</see></li>
    ///   <li><see href="http://en.wikipedia.org/wiki/Windows_Registry#cite_ref-4">List of standard registry value types</see></li>
    ///   <li><see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724884%28v=vs.85%29.aspx">Registry Value Types</see></li>
    ///   <li><see href="http://msdn.microsoft.com/en-us/library/microsoft.win32.registryvaluekind.aspx">RegistryValueKind Enumeration</see></li>
    /// </ul>
    /// </remarks>
    public class QueryRegistryKey
    {
        #region Private Fields
        /// <summary>Exclude list, containing keys to ignore below <c>key</c> or <c>querys</c> parameters.</summary>
        private Collection<QueryItem> exclude = new Collection<QueryItem>();

        /// <summary>Filter list, containing value name and value data for filtering.</summary>
        private Collection<FilterItem> filter = new Collection<FilterItem>();

        /// <summary>Result list, containing full key, value and data.</summary>
        private ItemCollection<ResultItem> result = new ItemCollection<ResultItem>();

        /// <summary>Error list, containing full key, value and error.</summary>
        private ItemCollection<ErrorItem> error = new ItemCollection<ErrorItem>();
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="QueryRegistryKey"/> class.</summary>
        /// <param name="key">The key to query.</param>
        /// <remarks>Query a register key, add result to <see cref="Result"/>.</remarks>
        public QueryRegistryKey(RegistryKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            Contract.Requires<ArgumentNullException>(key != null && !string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            this.Query(key);
        }

        /// <summary>Initializes a new instance of the <see cref="QueryRegistryKey"/> class.</summary>
        /// <param name="key">The key to query.</param>
        /// <param name="excludeCollection">The keys to ignore, below <paramref name="key"/>.</param>
        /// <param name="filterCollection">Filter values and data.</param>
        /// <remarks>Query a register key, add result to <see cref="Result"/>.</remarks>
        public QueryRegistryKey(RegistryKey key, Collection<QueryItem> excludeCollection, Collection<FilterItem> filterCollection)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            Contract.Requires<ArgumentNullException>(key != null && !string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(excludeCollection != null, "exclude cannot be null");
            Contract.Requires<ArgumentNullException>(filterCollection != null, "filter cannot be null");
            this.Query(key, excludeCollection, filterCollection);
        }

        /// <summary>Initializes a new instance of the <see cref="QueryRegistryKey"/> class.</summary>
        /// <param name="queryCollection">The keys to query.</param>
        /// <param name="excludeCollection">The keys to ignore, below items in <paramref name="queryCollection"/>.</param>
        /// <param name="filterCollection">Filter values and data.</param>
        /// <remarks>Query a register key, add result to <see cref="Result"/>.</remarks>
        public QueryRegistryKey(Collection<QueryItem> queryCollection, Collection<QueryItem> excludeCollection, Collection<FilterItem> filterCollection)
        {
            Contract.Requires<ArgumentNullException>(queryCollection != null, "querys cannot be null");
            Contract.Requires<ArgumentNullException>(excludeCollection != null, "exclude cannot be null");
            Contract.Requires<ArgumentNullException>(filterCollection != null, "filter cannot be null");
            this.Query(queryCollection, excludeCollection, filterCollection);
        }
        #endregion

        #region Properties
        /// <summary>Gets the query result data.</summary>
        /// <remarks>
        /// Property of a result item, {KeyName, ValueName, ValueData}. 
        /// <table>
        /// <tr><th>ItemType</th><th>Format</th></tr>
        /// <caption>Format of the <c>properties</c> in a list item.</caption>
        /// <tr><td>KeyName</td><td>&lt;Hive Name&gt;\&lt;Key Name&gt;\&lt;Subkey Name&gt;</td></tr>
        /// <tr><td>ValueName</td><td>The name or @ for default value.</td></tr>
        /// <tr><td>ValueData</td><td>The data for string type, else &lt;Value type&gt;:&lt;Value data&gt;</td></tr>
        /// </table> 
        /// </remarks>
        public IList<ResultItem> Result
        {
            get
            {
                return this.result;
            }
        }

        /// <summary>Gets the error list, from the query.</summary>
        /// <remarks>
        /// Property of a error item, {KeyName, Name, Error}. 
        /// <table>
        /// <tr><th>ItemType</th><th>Format</th></tr>
        /// <caption>Format of the <c>properties</c> in a list item.</caption>
        /// <tr><td>KeyName</td><td>&lt;Hive Name&gt;\&lt;Key Name&gt;\&lt;Subkey Name&gt;</td></tr>
        /// <tr><td>Name</td><td>The value name, subkey name or method name for the error.</td></tr>
        /// <tr><td>Error</td><td>The exception for query Key Name or Value Name.</td></tr>
        /// </table> 
        /// </remarks>
        public IList<ErrorItem> Error
        {
            get
            {
                return this.error;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>Query a register key, add result to <see cref="Result"/> using current <see cref="exclude"/> and current <see cref="filter"/>.</summary>
        /// <param name="key">The key to query.</param>
        public void Query(RegistryKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            Contract.Requires<ArgumentNullException>(key != null && !string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            this.QueryRegistry(key);
        }

        /// <summary>Query a register key, add result to <see cref="Result"/>, using a new <see cref="exclude"/> and new <see cref="filter"/>.</summary>
        /// <param name="key">The key to query.</param>
        /// <param name="excludeCollection">The keys to ignore, below <paramref name="key"/>.</param>
        /// <param name="filterCollection">The new filter for values and data.</param>
        public void Query(RegistryKey key, Collection<QueryItem> excludeCollection, Collection<FilterItem> filterCollection)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            Contract.Requires<ArgumentNullException>(key != null && !string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(excludeCollection != null, "exclude cannot be null");
            Contract.Requires<ArgumentNullException>(filterCollection != null, "filter cannot be null");
            if (this.exclude != null && excludeCollection != null && excludeCollection.Count > 0)
            {
                foreach (var item in excludeCollection)
                {
                    if (item != null)
                    {
                        this.exclude.Add(item);
                    }
                }
            }

            if (this.filter != null && filterCollection != null && filterCollection.Count > 0)
            {
                foreach (var item in filterCollection)
                {
                    if (item != null)
                    {
                        this.filter.Add(item);
                    }
                }
            }

            this.QueryRegistry(key);
        }

        /// <summary>Query a register key list, add result to <see cref="Result"/>, using a new <see cref="exclude"/> and new <see cref="filter"/>.</summary>
        /// <param name="queryCollection">The keys to query.</param>
        /// <param name="excludeCollection">The keys to ignore, below items in <paramref name="queryCollection"/>.</param>
        /// <param name="filterCollection">Filter values and data.</param>
        /// <remarks>Query a register key, add result to <see cref="Result"/>.</remarks>
        public void Query(Collection<QueryItem> queryCollection, Collection<QueryItem> excludeCollection, Collection<FilterItem> filterCollection)
        {
            Contract.Requires<ArgumentNullException>(queryCollection != null, "querys cannot be null");
            Contract.Requires<ArgumentNullException>(excludeCollection != null, "exclude cannot be null");
            Contract.Requires<ArgumentNullException>(filterCollection != null, "filter cannot be null");
            if (this.exclude != null && excludeCollection != null && excludeCollection.Count > 0)
            {
                foreach (var item in excludeCollection)
                {
                    if (item != null)
                    {
                        this.exclude.Add(item);
                    }
                }
            }

            if (this.filter != null && filterCollection != null && filterCollection.Count > 0)
            {
                foreach (var item in filterCollection)
                {
                    if (item != null)
                    {
                        this.filter.Add(item);
                    }
                }
            }

            if (queryCollection != null && queryCollection.Count > 0)
            {
                foreach (var item in queryCollection)
                {
                    if (item != null)
                    {
                        RootKeyType rootKey = item.RootKey;
                        string keyName = item.KeyName;
                        if (!string.IsNullOrWhiteSpace(keyName))
                        {
                            RegistryKey key = OpenRegistryKey.New(rootKey, keyName);
                            if (key != null && !string.IsNullOrWhiteSpace(key.Name))
                            {
                                this.QueryRegistry(key);
                            }
                            else
                            {
                                this.AddErrorItem(new ErrorItem(keyName, "Query(querys, exclude, filter);", new ArgumentException(string.Format(CultureInfo.CurrentCulture, "\"KeyName\"={0}, don't exist in \"RootKey\"={1}", keyName, rootKey))));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Export the result as Microsoft Registry Editor Export.</summary>
        /// <returns>A list formatted as a Registration Entries (<c>.reg</c>) file.</returns>
        /// <see href="http://en.wikipedia.org/wiki/Windows_Registry#.REG_files">.REG files (also known as Registration entries)</see>
        /// <remarks>Export by <c>regedit.exe</c> wraps hex lines at col 78-80, but is not needed. Only for readability.</remarks>
        public IList<string> Export()
        {
            Contract.Ensures(Contract.Result<IList<string>>() != null);
            IList<string> export = new List<string>();
            if (this.result != null && this.result.Count > 0)
            {
                export.Add("Windows Registry Editor Version 5.00");
                string keyName = string.Empty;
                foreach (var item in this.result)
                {
                    if (item != null && !string.IsNullOrWhiteSpace(item.KeyName) && item.ValueName != null && item.ValueData != null)
                    {
                        if (keyName != item.KeyName)
                        {
                            keyName = item.KeyName;
                            export.Add(string.Empty);
                            export.Add(string.Format(CultureInfo.InvariantCulture, "[{0}]", keyName));
                        }

                        if (string.IsNullOrEmpty(item.ValueName))
                        {
                            export.Add(string.Format(CultureInfo.InvariantCulture, "@={0}", item.ValueData));
                        }
                        else
                        {
                            export.Add(string.Format(CultureInfo.InvariantCulture, "\"{0}\"={1}", item.ValueName, item.ValueData.Replace("\r\n", "\r\n\r\n")));
                        }
                    }
                }

                export.Add(string.Empty);
            }

            return export;
        }

        /// <summary>Adds <paramref name="valueName"/> for all keys in <see cref="Result"/>.</summary>
        /// <param name="valueName">The value name to add.</param>
        public void AddValueName(string valueName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(valueName), "valueName cannot be null, empty or contain only white space");
            ItemCollection<ResultItem> newResult = new ItemCollection<ResultItem>();
            if (this.result != null && this.result.Count > 0)
            {
                string keyName = string.Empty;
                foreach (var item in this.result)
                {
                    try
                    {
                        if (item != null && !string.IsNullOrWhiteSpace(item.KeyName))
                        {
                            if (keyName != item.KeyName)
                            {
                                keyName = item.KeyName;
                                int rootKeyLength = keyName.IndexOf('\\');
                                if (keyName.Length >= 0 && rootKeyLength >= 0)
                                {
                                    string rootKey = keyName.Substring(0, rootKeyLength);
                                    int registryKeyLength = keyName.Length - rootKey.Length - 1;
                                    if (rootKey.Length > 0 && registryKeyLength >= 0)
                                    {
                                        string registryKey = keyName.Substring(rootKeyLength + 1, registryKeyLength);
                                        RegistryKey key = OpenRegistryKey.New(rootKey, registryKey);
                                        if (key != null && !string.IsNullOrWhiteSpace(key.Name) && HasValueName(valueName, key))
                                        {
                                            string valueData = FormatValueData(key, valueName);
                                            if (!string.IsNullOrEmpty(valueData))
                                            {
                                                newResult.Add(new ResultItem(keyName, valueName, valueData));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.AddErrorItem(new ErrorItem(item.KeyName, valueName, ex));
                        if (!(ex is SecurityException || ex is ObjectDisposedException || ex is UnauthorizedAccessException || ex is IOException))
                        {
                            throw;
                        }
                    }

                    newResult.Add(item);
                }

                this.result.Clear();
                this.result = newResult;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>Test if key has value name.</summary>
        /// <param name="valueName">The value name, to find.</param>
        /// <param name="key">The key to use.</param>
        /// <returns>Returns <c>true</c> if <paramref name="key"/> contains <paramref name="valueName"/>, else <c>false</c>.</returns>
        private static bool HasValueName(string valueName, RegistryKey key)
        {
            Contract.Requires(valueName != null, "valueName cannot be null");
            Contract.Requires(!string.IsNullOrWhiteSpace(valueName), "valueName cannot be null, empty or contain only white space");
            Contract.Requires(key != null, "key cannot be null");
            bool result = false;
            if (key.GetValue(valueName) != null)
            {
                result = true;
            }

            return result;
        }

        #region Format value data
        /// <summary>Retrieves the data from the <paramref name="valueName"/> in <paramref name="key"/>.</summary>
        /// <param name="key">The registry key to get value data.</param>
        /// <param name="valueName">The name of the value</param>
        /// <returns>The value data as string, with type prefix if needed.</returns>
        /// <remarks>The format of the data is &lt;Value type&gt;:&lt;Value data&gt;
        /// Registry types not in <see cref="RegistryValueKind"/>, could be read using entry in <c>advapi32.dll</c>.
        /// <para>
        /// References:
        /// <ul>
        ///   <li><see href="http://en.wikipedia.org/wiki/Windows_Registry#.REG_files">.REG files (also known as Registration entries)</see></li>
        ///   <li><see href="http://en.wikipedia.org/wiki/Windows_Registry#cite_ref-4">List of standard registry value types</see></li>
        ///   <li><see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724884%28v=vs.85%29.aspx">Registry Value Types</see></li>
        ///   <li><see href="http://msdn.microsoft.com/en-us/library/microsoft.win32.registryvaluekind.aspx">RegistryValueKind Enumeration</see></li>
        ///   <li><see href="http://www.dotnetframework.org/default.aspx/Dotnetfx_Vista_SP2/Dotnetfx_Vista_SP2/8@0@50727@4016/DEVDIV/depot/DevDiv/releases/whidbey/NetFxQFE/ndp/clr/src/BCL/Microsoft/Win32/RegistryKey@cs/1/RegistryKey@cs">RegistryKey.cs source code in C# .NET</see></li>
        /// </ul>
        /// </para><para>
        /// <table>
        ///   <caption>Numeric values for Registry Predefined Value Types in <c>Winnt.h</c>.</caption>
        ///   <tr><th>Type</th><th>Value</th></tr>
        ///   <tr><td>REG_NONE</td><td>0</td></tr>
        ///   <tr><td>REG_SZ</td><td>1</td></tr>
        ///   <tr><td>REG_EXPAND_SZ</td><td>2</td></tr>
        ///   <tr><td>REG_BINARY</td><td>3</td></tr>
        ///   <tr><td>REG_DWORD</td><td>4</td></tr>
        ///   <tr><td>REG_DWORD_LITTLE_ENDIAN</td><td>4</td></tr>
        ///   <tr><td>REG_DWORD_BIG_ENDIAN</td><td>5</td></tr>
        ///   <tr><td>REG_LINK</td><td>6</td></tr>
        ///   <tr><td>REG_MULTI_SZ</td><td>7</td></tr>
        ///   <tr><td>REG_RESOURCE_LIST</td><td>8</td></tr>
        ///   <tr><td>REG_FULL_RESOURCE_DESCRIPTOR</td><td>9</td></tr>
        ///   <tr><td>REG_RESOURCE_REQUIREMENTS_LIST</td><td>10</td></tr>
        ///   <tr><td>REG_QWORD</td><td>11</td></tr>
        ///   <tr><td>REG_QWORD_LITTLE_ENDIAN</td><td>11</td></tr>
        /// </table>
        /// </para><para>
        /// <table>
        ///   <caption>Numeric values for Registry Types in <see cref="RegistryValueKind"/> Enumeration.</caption>
        ///   <tr><th>Type</th><th>Value</th></tr>
        ///   <tr><td>None</td><td>-1</td></tr>
        ///   <tr><td>Unknown</td><td>0</td></tr>
        ///   <tr><td>String</td><td>1</td></tr>
        ///   <tr><td>ExpandString</td><td>2</td></tr>
        ///   <tr><td>Binary</td><td>3</td></tr>
        ///   <tr><td>DWord</td><td>4</td></tr>
        ///   <tr><td>MultiString</td><td>7</td></tr>
        ///   <tr><td>QWord</td><td>11</td></tr>
        /// </table>
        /// </para>
        /// </remarks>
        private static string FormatValueData(RegistryKey key, string valueName)
        {
            Contract.Requires(key != null);
            Contract.Requires(valueName != null);
            Contract.Ensures(Contract.Result<string>() != null);
            string text = string.Empty;
            RegistryValueKind rvk = key.GetValueKind(valueName);
            object keyValue = key.GetValue(valueName);
            if (keyValue != null)
            {
                switch (rvk)
                {
                    case RegistryValueKind.None:
                        byte[] none = (byte[])keyValue;
                        text = string.Format(CultureInfo.InvariantCulture, "hex(0):{0}", ByteToHexList(none));
                        break;
                    case RegistryValueKind.Unknown:
                        throw new UnauthorizedAccessException("Unknown registry value kind.");
                    case RegistryValueKind.String:
                        string value = (string)keyValue;
                        text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", value.ToString().Replace(@"\", @"\\").Replace(@"""", @"\"""));
                        break;
                    case RegistryValueKind.ExpandString:
                        string expand = (string)keyValue;
                        text = string.Format(CultureInfo.InvariantCulture, "hex(2):{0}", StringToHexList(expand));
                        break;
                    case RegistryValueKind.Binary:
                        byte[] binary = (byte[])keyValue;
                        text = string.Format(CultureInfo.InvariantCulture, "hex:{0}", ByteToHexList(binary));
                        break;
                    case RegistryValueKind.DWord:
                        int dword = (int)keyValue;
                        text = string.Format(CultureInfo.InvariantCulture, "dword:{0:x8}", dword);
                        break;
                    case RegistryValueKind.MultiString:
                        string[] multi = (string[])keyValue;
                        text = string.Format(CultureInfo.InvariantCulture, "hex(7):{0}", StringArrayToHexList(multi));
                        break;
                    case RegistryValueKind.QWord:
                        byte[] qword = (byte[])keyValue;
                        text = string.Format(CultureInfo.InvariantCulture, "hex(b):{0}", ByteToHexList(qword));
                        break;
                    default:
                        throw new UnauthorizedAccessException("Default registry value kind.");
                }
            }

            return text;
        }

        #region Convert to Hex list
        /// <summary>Convert a byte array to a comma-delimited list of hexadecimal values.</summary>
        /// <param name="array">The byte array to convert.</param>
        /// <returns>A <c>string</c> with a comma-delimited list of hexadecimal values.</returns>
        private static string ByteToHexList(byte[] array)
        {
            Contract.Requires(array != null);
            Contract.Ensures(Contract.Result<string>() != null);
            string hex = BitConverter.ToString(array);
            if (hex != null)
            {
                return hex.Replace("-", ",");
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>Convert a string to a comma-delimited list of hexadecimal values.</summary>
        /// <param name="line">The string to convert.</param>
        /// <returns>A <c>string</c> with a comma-delimited list of hexadecimal values.</returns>
        private static string StringToHexList(string line)
        {
            Contract.Requires(line != null);
            Contract.Ensures(Contract.Result<string>() != null);
            string hex = string.Empty;
            if (line.Length > 0)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    if (!string.IsNullOrEmpty(hex))
                    {
                        hex += ",";
                    }

                    string item = string.Format(CultureInfo.InvariantCulture, "{0:x2},00", (byte)line[j]);
                    if (item != null)
                    {
                        hex += item;
                    }
                }

                hex += ",00,00";
            }

            return hex;
        }

        /// <summary>Convert a string array to a comma-delimited list of hexadecimal values.</summary>
        /// <param name="array">The string array to convert.</param>
        /// <returns>A <c>string</c> with a comma-delimited list of hexadecimal values.</returns>
        private static string StringArrayToHexList(string[] array)
        {
            Contract.Requires(array != null);
            Contract.Ensures(Contract.Result<string>() != null);
            string hex = string.Empty;
            if (array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 0)
                    {
                        hex += ",";
                    }

                    string line = array[i];
                    if (line != null)
                    {
                        hex += StringToHexList(line);
                    }
                }

                hex += ",00,00";
            }

            return hex;
        }
        #endregion
        #endregion

        /// <summary>Query registry for key.</summary>
        /// <param name="key">The key to query.</param>
        private void QueryRegistry(RegistryKey key)
        {
            Contract.Requires(key != null, "key cannot be null");
            Contract.Requires(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            if (!this.KeyContainsExcludeItem(key))
            {
                this.QueryRegistryValues(key);
                if (!string.IsNullOrWhiteSpace(key.Name))
                {
                    this.QueryRegistrySubKeys(key);
                }
            }
        }

        /// <summary>Test if registry key contains a ignore item.</summary>
        /// <param name="key">The key to test.</param>
        /// <returns>Returns <c>true</c> if key contains exclude item, else <c>false</c>.</returns>
        private bool KeyContainsExcludeItem(RegistryKey key)
        {
            Contract.Requires(key != null, "key cannot be null");
            Contract.Requires(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            if (this.exclude != null)
            {
                foreach (var item in this.exclude)
                {
                    if (item != null && item.KeyName != null)
                    {
                        if (!string.IsNullOrWhiteSpace(key.Name) && OpenRegistryKey.GetRootKeyType(key) == item.RootKey)
                        {
                            string keyName = key.Name;
                            if (keyName != null && keyName.Contains(item.KeyName))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        #region Query Values
        /// <summary>Query values for key.</summary>
        /// <param name="key">The key to query.</param>
        private void QueryRegistryValues(RegistryKey key)
        {
            Contract.Requires(key != null, "key cannot be null");
            Contract.Requires(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            try
            {
                this.QueryValues(key);
            }
            catch (Exception ex)
            {
                this.AddErrorItem(new ErrorItem(key, "QueryRegistryValues(key);", ex));
                if (!(ex is SecurityException || ex is ObjectDisposedException || ex is UnauthorizedAccessException || ex is IOException))
                {
                    throw;
                }
            }
        }

        /// <summary>Query value names for registry key.</summary>
        /// <param name="key">The registry key to query.</param>
        private void QueryValues(RegistryKey key)
        {
            Contract.Requires(key != null, "key cannot be null");
            Contract.Requires(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            string keyName = key.Name;
            ItemCollection<FilterItem> filterList = null;
            if (this.filter != null)
            {
                filterList = new ItemCollection<FilterItem>(this.filter);
            }

            if (key.ValueCount > 0)
            {
                string[] valueNames = key.GetValueNames();
                if (this.result != null && valueNames != null)
                {
                    foreach (string valueName in valueNames)
                    {
                        if (valueName != null)
                        {
                            try
                            {
                                if (filterList != null && filterList.Count > 0)
                                {
                                    FilterItem filterItem = filterList.Find(item => item.ValueName == valueName);
                                    if (filterItem != null)
                                    {
                                        string filterData = filterItem.ValueData;
                                        string valueData = FormatValueData(key, valueName);
                                        if (filterData != null && string.IsNullOrEmpty(filterData) && valueData != null)
                                        {
                                            this.AddResultItem(new ResultItem(keyName, valueName, valueData));
                                        }
                                    }
                                }
                                else
                                {
                                    string valueData = FormatValueData(key, valueName);
                                    if (valueData != null)
                                    {
                                        this.AddResultItem(new ResultItem(keyName, valueName, valueData));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.AddErrorItem(new ErrorItem(keyName, valueName, ex));
                                if (!(ex is SecurityException || ex is ObjectDisposedException || ex is UnauthorizedAccessException || ex is IOException))
                                {
                                    throw;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Query SubKeys
        /// <summary>Query sub keys for key.</summary>
        /// <param name="key">The key to query.</param>
        private void QueryRegistrySubKeys(RegistryKey key)
        {
            Contract.Requires(key != null, "key cannot be null");
            Contract.Requires(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            try
            {
                this.QuerySubKeys(key);
            }
            catch (Exception ex)
            {
                this.AddErrorItem(new ErrorItem(key, "QueryRegistrySubKeys(key);", ex));
                if (!(ex is SecurityException || ex is ObjectDisposedException || ex is UnauthorizedAccessException || ex is IOException))
                {
                    throw;
                }
            }
        }

        /// <summary>Query all registry sub keys of the registry key.</summary>
        /// <param name="key">The registry key to query.</param>
        private void QuerySubKeys(RegistryKey key)
        {
            Contract.Requires(key != null, "key cannot be null");
            Contract.Requires(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            if (key.SubKeyCount > 0)
            {
                string[] subKeyNames = key.GetSubKeyNames();
                if (subKeyNames != null)
                {
                    foreach (string subKeyName in subKeyNames)
                    {
                        if (subKeyName != null)
                        {
                            RegistryKey subKey = key.OpenSubKey(subKeyName);
                            if (subKey != null && !string.IsNullOrWhiteSpace(subKey.Name))
                            {
                                try
                                {
                                    this.QueryRegistry(subKey);
                                }
                                catch (Exception ex)
                                {
                                    this.AddErrorItem(new ErrorItem(key, subKeyName, ex));
                                    if (!(ex is SecurityException || ex is ObjectDisposedException || ex is UnauthorizedAccessException || ex is IOException))
                                    {
                                        throw;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Add items
        /// <summary>Add an error item.</summary>
        /// <param name="item">The item to add.</param>
        private void AddErrorItem(ErrorItem item)
        {
            Contract.Requires(item != null, "item cannot be null");
            if (this.error != null && item != null)
            {
                this.error.Add(item);
            }
        }

        /// <summary>Add an result item.</summary>
        /// <param name="item">The item to add.</param>
        private void AddResultItem(ResultItem item)
        {
            Contract.Requires(item != null, "item cannot be null");
            if (this.result != null && item != null)
            {
                this.result.Add(item);
            }
        }
        #endregion
        #endregion
    }
}
