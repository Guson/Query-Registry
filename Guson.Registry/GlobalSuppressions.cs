// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

//// This file is used by Code Analysis to maintain SuppressMessage 
//// attributes that are applied to this project.
//// Project-level suppressions either have no target or are given 
//// a specific target and scoped to a namespace, type, member, etc.
////
//// To add a suppression to this file, right-click the message in the 
//// Code Analysis results, point to "Suppress Message", and click 
//// "In Suppression File".
//// You do not need to add suppressions to this file manually.
//// Do add Justification, for all added suppressions

using System.Diagnostics.CodeAnalysis;

#if !NET45
[assembly: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "Guson.Registry.ReadOnlyDictionary`2", Justification = "This collection is used as a dictionary.")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "Guson.Registry.ReadOnlyDictionary`2", Justification = "This collection is used as a dictionary.")]
[assembly: SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "Guson.Registry.OpenRegistryKey.#RootKeys", Justification = "This collection is immutable.")]
#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKLM", Scope = "member", Target = "Guson.Registry.RootKeyType.#HKLM", Justification = "Using registry naming for consistency.")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKPD", Scope = "member", Target = "Guson.Registry.RootKeyType.#HKPD", Justification = "Using registry naming for consistency.")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKCC", Scope = "member", Target = "Guson.Registry.RootKeyType.#HKCC", Justification = "Using registry naming for consistency.")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKCR", Scope = "member", Target = "Guson.Registry.RootKeyType.#HKCR", Justification = "Using registry naming for consistency.")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKU", Scope = "member", Target = "Guson.Registry.RootKeyType.#HKU", Justification = "Using registry naming for consistency.")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKCU", Scope = "member", Target = "Guson.Registry.RootKeyType.#HKCU", Justification = "Using registry naming for consistency.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Guson.Registry.Controls", Justification = "Using Controls namespace for consistency, whith user controls")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "The assembly is delay-signed in Release-build.")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Guson.Registry.QueryRegistryKey.#FormatValueData(Microsoft.Win32.RegistryKey,System.String)", Justification = "The local variable /'keyValue/' is used in a switch.")]
