// -----------------------------------------------------------------------
// <copyright file="RegisterKeyItem.xaml.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry.Controls
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Windows.Controls;
    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for RegisterKeyItem.xaml
    /// </summary>
    /// <example>
    /// Add user control
    /// <![CDATA[
    ///   <WrapPanel>
    ///       <gr:RegisterKeyItem Width="400" HorizontalAlignment="Stretch" />
    ///       <Image  Source="/image/new.png" Width="20" Height="20" Margin="2,2,2,2" HorizontalAlignment="Right" />
    ///   </WrapPanel>
    /// ]]>
    /// </example>
    public partial class RegisterKeyItem : UserControl
    {
        /// <summary>Initializes a new instance of the <see cref="RegisterKeyItem"/> class.</summary>
        public RegisterKeyItem()
        {
            this.InitializeComponent();
        }

        #region Properties
        /// <summary>Gets or sets query item.</summary>
        public QueryItem Item
        {
            get { return new QueryItem(this.RootKey, this.KeyName); }
            set
            {
                Contract.Requires(value != null);
                this.SetItem(value);
            }
        }

        /// <summary>Gets or sets root key.</summary>
        public OpenRegistryKey.RootKeyType RootKey
        {
            get { return GetRootKey(); }
            set { SetRootKey(value); }
        }

        /// <summary>Gets or sets key name.</summary>
        public string KeyName
        {
            get { return keyName.Text; }
            set { this.SetKeyName(value); }
        }
        #endregion

        #region Methods
        /// <summary>Get if key exist.</summary>
        /// <returns>Returns <c>true</c> if key exist, else <c>false</c>.</returns>
        public bool KeyExist()
        {
            RegistryKey key = OpenRegistryKey.New(this.RootKey, this.KeyName);
            return key != null;
        }

        /// <summary>Gets the root key.</summary>
        /// <returns>The root key.</returns>
        private OpenRegistryKey.RootKeyType GetRootKey()
        {
            if (this.rootKeys != null && this.rootKeys.Text != null)
            {
                return (OpenRegistryKey.RootKeyType)Enum.Parse(typeof(OpenRegistryKey.RootKeyType), rootKeys.Text);
            }
            else
            {
                return OpenRegistryKey.RootKeyType.HKLM;
            }
        }

        /// <summary>Sets root key.</summary>
        /// <param name="value">The value to set.</param>
        private void SetRootKey(OpenRegistryKey.RootKeyType value)
        {
            if (this.rootKeys != null && this.rootKeys.Text != null)
            {
                rootKeys.Items.IndexOf(value);
            }
        }

        /// <summary>Gets key name.</summary>
        /// <returns>The key name.</returns>
        private string GetKeyName()
        {
            if (keyName != null && keyName.Text != null)
            {
                return keyName.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>Sets key name.</summary>
        /// <param name="keyName">The keyName.</param>
        /// <exception cref="ArgumentNullException">keyName is null</exception>
        private void SetKeyName(string keyName)
        {
            Contract.Requires<ArgumentNullException>(keyName != null, "KeyName cannot be null");
            this.KeyName = keyName;
        }

        /// <summary>Set query item.</summary>
        /// <param name="item">The query item.</param>
        /// <exception cref="ArgumentNullException">item is null</exception>
        /// <exception cref="ArgumentNullException">item.KeyName is null</exception>
        private void SetItem(QueryItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null, "item cannot be null");
            Contract.Requires<ArgumentNullException>(item.KeyName != null, "item.KeyName cannot be null");
            if (item != null && item.KeyName != null)
            {
                this.RootKey = item.RootKey;
                this.KeyName = item.KeyName;
            }
        }
        #endregion

        /// <summary>Condition on an instance of the class.</summary>
        [ContractInvariantMethod]
        private void ContractInvariant()
        {
            Contract.Invariant(this.KeyName != null, "Property KeyName cannot be null");
        }
    }
}
