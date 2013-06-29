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

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKLM", Scope = "member", Target = "Guson.Registry.OpenRegistryKey+RootKeyType.#HKLM", Justification = "Using registry naming for consistency.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKPD", Scope = "member", Target = "Guson.Registry.OpenRegistryKey+RootKeyType.#HKPD", Justification = "Using registry naming for consistency.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKCC", Scope = "member", Target = "Guson.Registry.OpenRegistryKey+RootKeyType.#HKCC", Justification = "Using registry naming for consistency.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKCR", Scope = "member", Target = "Guson.Registry.OpenRegistryKey+RootKeyType.#HKCR", Justification = "Using registry naming for consistency.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKU", Scope = "member", Target = "Guson.Registry.OpenRegistryKey+RootKeyType.#HKU", Justification = "Using registry naming for consistency.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKCU", Scope = "member", Target = "Guson.Registry.OpenRegistryKey+RootKeyType.#HKCU", Justification = "Using registry naming for consistency.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Guson", Scope = "namespace", Target = "Guson.Registry.Controls", Justification="Guson is proper name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Guson", Justification = "Guson is proper name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Guson", Scope = "namespace", Target = "Guson.Registry", Justification = "Guson is proper name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "The assembly is delay-signed in Release-build.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Guson.Registry.Controls", Justification = "Using Controls namespace for consistency, whith user controls")]
