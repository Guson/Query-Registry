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
    /// <remarks>
    /// Class Diagram:
    /// <p><img src="..\images\RootKeyComboBox.png" alt="RootKeyComboBox Class Diagram" /></p>
    /// </remarks>
    public partial class RootKeyComboBox : UserControl
    {
        /// <summary>Initializes a new instance of the <see cref="RootKeyComboBox"/> class.</summary>
        public RootKeyComboBox()
        {
            ////this.InitializeComponent();
        }

        /// <summary>Gets or sets root key.</summary>
        public RootKeyType RootKey
        {
            get { return this.GetRootKey(); }
            set { this.SetRootKey(value); }
        }

        /// <summary>Gets the root key.</summary>
        /// <returns>The root key.</returns>
        private RootKeyType GetRootKey()
        {
            if (this.rootKeys != null && this.rootKeys.Text != null)
            {
                return (RootKeyType)Enum.Parse(typeof(RootKeyType), rootKeys.Text);
            }
            else
            {
                return RootKeyType.HKLM;
            }
        }

        /// <summary>Sets root key.</summary>
        /// <param name="value">The value to set.</param>
        private void SetRootKey(RootKeyType value)
        {
            if (this.rootKeys != null && this.rootKeys.Text != null)
            {
                rootKeys.Items.IndexOf(value);
            }
        }
    }
}
