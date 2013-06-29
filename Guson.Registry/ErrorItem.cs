// -----------------------------------------------------------------------
// <copyright file="ErrorItem.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Diagnostics.Contracts;
    using Microsoft.Win32;

    /// <summary>Immutable Value Class. Definition for items in <see cref="QueryRegistryKey.Error"/></summary>
    public class ErrorItem
    {
        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="ErrorItem"/> class. Create a new error item object.</summary>
        /// <param name="key">The registry key.</param>
        /// <param name="name">The subKeyName, valueName or method name.</param>
        /// <param name="ex">The exception that occurred.</param>
        /// <value>A new error item.</value>
        /// <overload>Create a new error item object.</overload>
        public ErrorItem(RegistryKey key, string name, Exception ex)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), "name cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(ex != null, "ex cannot be null");
            ////Contract.Requires<ArgumentNullException>(ex.InnerException != null, "ex.InnerException cannot be null");
            ////Contract.Requires<ArgumentNullException>(ex.InnerException.Message != null, "ex.InnerException.Message cannot be null");
            this.KeyName = key.Name;
            this.Name = name;
            this.Error = ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message : null);
        }

        /// <summary>Initializes a new instance of the <see cref="ErrorItem"/> class. Create a new error item object.</summary>
        /// <param name="keyName">The registry key name.</param>
        /// <param name="name">The subKeyName, valueName or method name.</param>
        /// <param name="ex">The exception that occurred.</param>
        /// <value>A new error item.</value>
        public ErrorItem(string keyName, string name, Exception ex)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(keyName), "keyName cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), "name cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(ex != null, "ex cannot be null");
            ////Contract.Requires<ArgumentNullException>(ex.InnerException != null, "ex.InnerException cannot be null");
            ////Contract.Requires<ArgumentNullException>(ex.InnerException.Message != null, "ex.InnerException.Message cannot be null");
            this.KeyName = keyName;
            this.Name = name;
            this.Error = ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message : null);
        }
        #endregion

        #region Value Properties
        /// <summary>Gets the full key name, including the key root(full registry path).</summary>
        public string KeyName { get; private set; }

        /// <summary>Gets the subKeyName, valueName or method name for the item.</summary>
        public string Name { get; private set; }

        /// <summary>Gets the exception for the error.</summary>
        public string Error { get; private set; }
        #endregion

        /// <summary>Condition on an instance of the class.</summary>
        [ContractInvariantMethod]
        private void ContractInvariant()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.KeyName), "Property KeyName cannot be null, empty or contain only white space");
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.Name), "Property Name cannot be null, empty or contain only white space");
            Contract.Invariant(this.Error != null, "Property Error cannot be null");
        }
    }
}
