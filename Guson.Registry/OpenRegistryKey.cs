// -----------------------------------------------------------------------
// <copyright file="OpenRegistryKey.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using Microsoft.Win32;

    /// <summary>
    /// Static class to open a registry key.
    /// </summary>
    /// <remarks>
    /// Class Diagram:
    /// <p><img src="..\images\OpenRegistryKey.png" alt="OpenRegistryKey Class Diagram" /></p>
    /// References:
    /// <ul>
    ///   <li>ToDo.</li>
    /// </ul>
    /// </remarks>
    public static class OpenRegistryKey
    {
        /// <summary>Immutable dictionary of root key names.</summary>
        /// <remarks>Change to a IDictionary implementation <see href="http://stackoverflow.com/questions/313324/declare-a-dictionary-inside-a-static-class"/>.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification="Is semi immutable, change later.")]
        public static readonly Dictionary<string, RootKeyType> RootKeys = new Dictionary<string, RootKeyType>()
        {
            { "HKEY_LOCAL_MACHINE", RootKeyType.HKLM },
            { "HKEY_CURRENT_CONFIG", RootKeyType.HKCC },
            { "HKEY_CLASSES_ROOT", RootKeyType.HKCR },
            { "HKEY_USERS", RootKeyType.HKU },
            { "HKEY_CURRENT_USER", RootKeyType.HKCU },
            { "HKEY_PERFORMANCE_DATA", RootKeyType.HKPD },
            { "HKLM", RootKeyType.HKLM },
            { "HKCC", RootKeyType.HKCC },
            { "HKCR", RootKeyType.HKCR },
            { "HKU", RootKeyType.HKU },
            { "HKCU", RootKeyType.HKCU },
            { "HKPD", RootKeyType.HKPD }
        };

        /// <summary>Definition of Windows root registry keys(Hives).</summary>
        /// <see href="http://en.wikipedia.org/wiki/Windows_Registry#Root_keys"/>
        public enum RootKeyType
        {
            /// <summary>The HKEY_LOCAL_MACHINE (HKLM)</summary>
            HKLM,

            /// <summary>The HKEY_CURRENT_CONFIG (HKCC)</summary>
            HKCC,

            /// <summary>The HKEY_CLASSES_ROOT (HKCR)</summary>
            HKCR,

            /// <summary>The HKEY_USERS (HKU)</summary>
            HKU,

            /// <summary>The HKEY_CURRENT_USER (HKCU)</summary>
            HKCU,

            /// <summary>The HKEY_PERFORMANCE_DATA (HKPD)</summary>
            HKPD
        }

        #region Public Static Methods
        /// <summary>Create a Registry key.</summary>
        /// <param name="rootKey">The root key to use.</param>
        /// <returns>The opened key.</returns>
        public static RegistryKey New(RootKeyType rootKey)
        {
            return New(rootKey, string.Empty);
        }

        /// <summary>Create a Registry key.</summary>
        /// <param name="rootKey">The root key to use.</param>
        /// <param name="keyName">A valid registry key string.</param>
        /// <returns>The opened key.</returns>
        public static RegistryKey New(RootKeyType rootKey, string keyName)
        {
            Contract.Requires<ArgumentNullException>(keyName != null, "keyName cannot be null");
            if (!string.IsNullOrEmpty(keyName))
            {
                switch (rootKey)
                {
                    case RootKeyType.HKLM:
                        return Registry.LocalMachine.OpenSubKey(keyName);
                    case RootKeyType.HKCC:
                        return Registry.CurrentConfig.OpenSubKey(keyName);
                    case RootKeyType.HKCR:
                        return Registry.ClassesRoot.OpenSubKey(keyName);
                    case RootKeyType.HKU:
                        return Registry.Users.OpenSubKey(keyName);
                    case RootKeyType.HKCU:
                        return Registry.CurrentUser.OpenSubKey(keyName);
                    case RootKeyType.HKPD:
                        return Registry.PerformanceData.OpenSubKey(keyName);
                }
            }
            else
            {
                switch (rootKey)
                {
                    case RootKeyType.HKLM:
                        return Registry.LocalMachine;
                    case RootKeyType.HKCC:
                        return Registry.CurrentConfig;
                    case RootKeyType.HKCR:
                        return Registry.ClassesRoot;
                    case RootKeyType.HKU:
                        return Registry.Users;
                    case RootKeyType.HKCU:
                        return Registry.CurrentUser;
                    case RootKeyType.HKPD:
                        return Registry.PerformanceData;
                }
            }

            throw new ArgumentException("Unknown RootKeyType.");
        }

        /// <summary>Create a Registry key.</summary>
        /// <param name="rootKey">The root key as <c>string</c> to use, need to be defined in the <see cref="RootKeys"/> dictionary.</param>
        /// <param name="keyName">A valid registry key string.</param>
        /// <returns>The opened key.</returns>
        /// <exception cref="ArgumentException">The <paramref name="rootKey"/>, is not defined in the <see cref="RootKeys"/> dictionary.</exception>
        public static RegistryKey New(string rootKey, string keyName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(rootKey), "rootKey cannot be null, empty or contain only white space");
            Contract.Requires<ArgumentNullException>(keyName != null, "keyName cannot be null");
            if (RootKeys.ContainsKey(rootKey))
            {
                RootKeyType root;
                if (RootKeys.TryGetValue(rootKey, out root))
                {
                    return New(root, keyName);
                }
            }

            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument rootKey={0}, is not defined in the \"RootKeys\" dictionary.", rootKey));
        }

        /// <summary>Get the <see cref="RootKeyType"/> from <paramref name="key"/>.</summary>
        /// <param name="key">A registry key.</param>
        /// <returns>The <see cref="RootKeyType"/> from <paramref name="key"/>.</returns>
        /// <exception cref="ArgumentException">The <paramref name="key"/>, is not defined in the <see cref="RootKeys"/> dictionary.</exception>
        public static RootKeyType GetRootKeyType(RegistryKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key cannot be null");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(key.Name), "key.Name cannot be null, empty or contain only white space");
            string keyName = key.Name;
            foreach (var rootKey in RootKeys)
            {
                string rootKeyName = rootKey.Key;
                if (rootKeyName != null)
                {
                    if (keyName.StartsWith(rootKeyName, StringComparison.CurrentCulture))
                    {
                        return rootKey.Value;
                    }
                }
            }

            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument key={0}, is not defined in the \"RootKeys\" dictionary.", key.Name));
        }
        #endregion

        /// <summary>Condition on an instance of the class.</summary>
        [ContractInvariantMethod]
        private static void ContractInvariant()
        {
            Contract.Invariant(RootKeys != null, "Member RootKeys cannot be null");
            Contract.Invariant(Contract.ForAll(RootKeys, i => i.Key != null), "The Key for items in the \"RootKeys\" dictionary cannot be null");
        }
    }
}
