// -----------------------------------------------------------------------
// <copyright file="ResultItem.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Diagnostics.Contracts;
    using Microsoft.Win32;

    /// <summary>Immutable Value Class. Definition for items in <see cref="QueryRegistryKey.Result"/>.</summary>
    /// <remarks>
    /// Class Diagram:
    /// <p><img src="..\images\ResultItem.png" alt="ResultItem Class Diagram" /></p>
    /// </remarks>  
    public class ResultItem
    {
        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="ResultItem"/> class. Create a new result item object.</summary>
        /// <param name="key">The registry key.</param>
        /// <param name="valueName">The value name.</param>
        /// <param name="valueData">The value data.</param>
        /// <value>A new result item.</value>
        /// <overload>Create a new result item object.</overload>
        public ResultItem(RegistryKey key, string valueName, string valueData)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(valueName != null, "valueName cannot be null");
            Contract.Requires<ArgumentNullException>(valueData != null, "valueData cannot be null");
            this.KeyName = key.Name;
            this.ValueName = valueName;
            this.ValueData = valueData;
        }

        /// <summary>Initializes a new instance of the <see cref="ResultItem"/> class. Create a new result item object.</summary>
        /// <param name="keyName">The registry key name.</param>
        /// <param name="valueName">The value name.</param>
        /// <param name="valueData">The value data.</param>
        /// <value>A new result item.</value>
        public ResultItem(string keyName, string valueName, string valueData)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(keyName), "keyName cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(valueName != null, "valueName cannot be null");
            Contract.Requires<ArgumentNullException>(valueData != null, "valueData cannot be null");
            this.KeyName = keyName;
            this.ValueName = valueName;
            this.ValueData = valueData;
        }
        #endregion

        #region Value Properties
        /// <summary>Gets the full key name, including the key root(full registry path).</summary>
        public string KeyName { get; private set; }

        /// <summary>Gets the name of the value item.</summary>
        public string ValueName { get; private set; }

        /// <summary>Gets the data of the value item.</summary>
        public string ValueData { get; private set; }
        #endregion

        /// <summary>Condition on an instance of the class.</summary>
        [ContractInvariantMethod]
        private void ContractInvariant()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.KeyName), "Property KeyName cannot be null, empty or contain only white space");
            Contract.Invariant(this.ValueName != null, "Property ValueName cannot be null");
            Contract.Invariant(this.ValueData != null, "Property ValueData cannot be null");
        }
    }
}
