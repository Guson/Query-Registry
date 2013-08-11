// -----------------------------------------------------------------------
// <copyright file="ReadOnlyDictionary.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

#if NET45
    //// ToDo: Use IReadOnlyDictionary if NET45
    /////// <summary>Represents an immutable/read-only collection of key/value pairs.</summary>
    /////// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /////// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /////// <remarks>
    /////// Class Diagram:
    /////// <p><img src="..\images\ReadOnlyDictionary.png" alt="ReadOnlyDictionary Class Diagram" /></p>
    /////// References:
    /////// <ul>
    ///////   <li><see href="http://msdn.microsoft.com/en-us/library/hh136548.aspx">IReadOnlyDictionary&lt;TKey, TValue&gt; Interface</see>.</li>
    /////// </ul>
    /////// </remarks>
    ////public sealed static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
    ////    this IDictionary<TKey, TValue> dictionary)
    ////{
    ////    Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
    ////    return new ReadOnlyDictionary<TKey, TValue>(dictionary);
    ////}
#else
    /// <summary>Represents an immutable/read-only collection of key/value pairs.</summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <remarks>
    /// Class Diagram:
    /// <p><img src="..\images\ReadOnlyDictionary.png" alt="ReadOnlyDictionary Class Diagram" /></p>
    /// References:
    /// <ul>
    ///   <li><see href="http://msdn.microsoft.com/en-us/library/hh136548.aspx">IReadOnlyDictionary&lt;TKey, TValue&gt; Interface</see>.</li>
    ///   <li><see href="http://stackoverflow.com/questions/678379/is-there-a-read-only-generic-dictionary-available-in-net">Is there a read-only generic dictionary available in .NET?</see>.</li>
    ///   <li><see href="http://www.softwarerockstar.com/2010/10/readonlydictionary-tkey-tvalue/">An Elegant Way to Implement ReadOnlyDictionary&lt;TKey, TValue&gt; in C#</see>.</li>
    /// </ul>
    /// </remarks>
    public sealed class ReadOnlyDictionary<TKey, TValue> : ReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        /// <summary>Initializes a new instance of the <see cref="ReadOnlyDictionary&lt;TKey, TValue&gt;"/> class.</summary>
        /// <param name="items">A dictionary.</param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> items)
            : base(items.ToList())
        {
            Contract.Requires<ArgumentNullException>(items != null, "items cannot be null");
        }

        /// <summary>Get the element that has the specified key.</summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The value for the <paramref name="key"/>.</returns>
        /// <exception cref="NullReferenceException">No value found for given <paramref name="key"/>.</exception>
        public TValue this[TKey key]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
                var valueQuery = this.GetQuery(key);
                if (valueQuery.Count() == 0)
                {
                    return default(TValue);
                }

                return valueQuery.First().Value;
            }
        }

        /// <summary>Determines whether the read-only dictionary contains an element that has the specified key.</summary>
        /// <param name="key">The key to locate.</param>
        /// <returns><c>true</c> if the read-only dictionary contains an element that has the specified <paramref name="key"/>; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            return this.GetQuery(key).Count() > 0;
        }

        /// <summary>Gets the value that is associated with the specified key.</summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the read-only dictionary contains an element that has the specified <paramref name="key"/>; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            var toReturn = this.ContainsKey(key);
            value = toReturn ? this[key] : default(TValue);
            return toReturn;
        }

        /// <summary>The find the elements that match the specified key.</summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>Matching <see cref="KeyValuePair&lt;TKey, TValue&gt;"/>'s for the <paramref name="key"/>.</returns>
        private IEnumerable<KeyValuePair<TKey, TValue>> GetQuery(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            return from t in this.Items where t.Key.Equals(key) select t;
        }
    }
#endif
}
