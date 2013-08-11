// -----------------------------------------------------------------------
// <copyright file="RootKeyType.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Guson.Registry
{
    /// <summary>Definition of Windows root registry keys(Hives).</summary>
    /// <remarks>
    /// References:
    /// <ul>
    ///   <li><see href="http://en.wikipedia.org/wiki/Windows_Registry#Root_keys">Root_keys</see>.</li>
    /// </ul>
    /// </remarks>
    public enum RootKeyType
    {
        /// <summary>The HKEY_LOCAL_MACHINE (<c>HKLM</c>)</summary>
        HKLM,

        /// <summary>The HKEY_CURRENT_CONFIG (<c>HKCC</c>)</summary>
        HKCC,

        /// <summary>The HKEY_CLASSES_ROOT (<c>HKCR</c>)</summary>
        HKCR,

        /// <summary>The HKEY_USERS (<c>HKU</c>)</summary>
        HKU,

        /// <summary>The HKEY_CURRENT_USER (<c>HKCU</c>)</summary>
        HKCU,

        /// <summary>The HKEY_PERFORMANCE_DATA (<c>HKPD</c>)</summary>
        HKPD
    }
}
