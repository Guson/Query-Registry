// -----------------------------------------------------------------------
// <copyright file="QueryItem.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Immutable Value Class. Definition for a query or exclude item in <see cref="QueryRegistryKey"/>.</summary>
    /// <remarks>
    /// Class Diagram:
    /// <p><img src="..\images\QueryItem.png" alt="QueryItem Class Diagram" /></p>
    /// </remarks>  
    public class QueryItem
    {
        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="QueryItem"/> class. Create a new query item object.</summary>
        /// <param name="rootKey">The root key.</param>
        /// <param name="keyName">The registry key name.</param>
        /// <value>A new query item.</value>
        public QueryItem(RootKeyType rootKey, string keyName)
        {
            Contract.Requires<ArgumentNullException>(keyName != null, "keyName cannot be null");
            this.RootKey = rootKey;
            this.KeyName = keyName;
        }
        #endregion

        #region Value Properties
        /// <summary>Gets the root key for the query item.</summary>
        public RootKeyType RootKey { get; private set; }

        /// <summary>Gets the key name, can be empty or a part of a key for exclude items.</summary>
        public string KeyName { get; private set; }
        #endregion

        /// <summary>Condition on an instance of the class.</summary>
        [ContractInvariantMethod]
        private void ContractInvariant()
        {
            Contract.Invariant(this.KeyName != null, "Property KeyName cannot be null");
        }
    }
}
