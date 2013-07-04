// -----------------------------------------------------------------------
// <copyright file="RootKeyComboBox.xaml.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry.Controls
{
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for RootKeyComboBox.xaml
    /// </summary>
    public partial class RootKeyComboBox : UserControl
    {
        /// <summary>Initializes a new instance of the <see cref="RootKeyComboBox"/> class.</summary>
        public RootKeyComboBox()
        {
            this.InitializeComponent();
        }

        /// <summary>Gets or sets root key.</summary>
        public OpenRegistryKey.RootKeyType RootKey
        {
            get { return this.GetRootKey(); }
            set { this.SetRootKey(value); }
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
    }
}
