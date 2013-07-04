// -----------------------------------------------------------------------
// <copyright file="ItemCollection.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>Definition class that inherits the List generic class.</summary>
    /// <typeparam name="T">The generic type for the items.</typeparam>
    /// <remarks>
    /// Class Diagram:
    /// <p><img src="..\images\ItemCollection.png" alt="ItemCollection Class Diagram" /></p>
    /// </remarks> 
    public class ItemCollection<T> : List<T>
    {
        /// <summary>Initializes a new instance of the <see cref="ItemCollection&lt;T&gt;"/> class.</summary>
        public ItemCollection()
        {
            Contract.Ensures(this.Count == 0);
        }

        /// <summary>Initializes a new instance of the <see cref="ItemCollection&lt;T&gt;"/> class.</summary>
        /// <param name="collection">Collection to </param>
        public ItemCollection(IEnumerable<T> collection)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection cannot be null");
            Contract.Requires<ArgumentNullException>(Contract.ForAll(collection, i => i != null), "items in collection cannot be null");
            Contract.Ensures(Contract.ForAll(this.ToArray(), i => i != null));
            this.Clear();
            foreach (var item in collection)
            {
                if (item != null)
                {
                    this.Add(item);
                }
            }
        }

        /// <summary>Condition on an instance of the class.</summary>
        [ContractInvariantMethod]
        private void ContractInvariant()
        {
            Contract.Invariant(Contract.ForAll(this.ToArray(), i => i != null), "The items in collection cannot be null");
        }
    }
}
