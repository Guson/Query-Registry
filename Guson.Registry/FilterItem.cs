// -----------------------------------------------------------------------
// <copyright file="FilterItem.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Immutable Value Class. Definition for filter items in <see cref="QueryRegistryKey"/>.</summary>
    public class FilterItem
    {
        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="FilterItem"/> class. Create a new filter item object.</summary>
        /// <param name="valueName">The value name.</param>
        /// <param name="valueData">The value data.</param>
        /// <value>A new filter item.</value>
        public FilterItem(string valueName, string valueData)
        {
            Contract.Requires<ArgumentNullException>(valueName != null, "valueName cannot be null");
            Contract.Requires<ArgumentNullException>(valueData != null, "valueData cannot be null");
            this.ValueName = valueName;
            this.ValueData = valueData;
        }
        #endregion

        #region Value Properties
        /// <summary>Gets the name of the value item.</summary>
        public string ValueName { get; private set; }

        /// <summary>Gets the data of the value item.</summary>
        public string ValueData { get; private set; }
        #endregion

        /// <summary>Condition on an instance of the class.</summary>
        [ContractInvariantMethod]
        private void ContractInvariant()
        {
            Contract.Invariant(this.ValueName != null, "Property ValueName cannot be null");
            Contract.Invariant(this.ValueData != null, "Property ValueData cannot be null");
        }
    }
}
