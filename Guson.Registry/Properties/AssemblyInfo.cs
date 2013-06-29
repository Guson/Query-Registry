// -----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Guson">
//   Copyright (c) 2013 mailto://guson@spray.se. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//// Assembly Title
#if DEBUG
[assembly: AssemblyTitle("Guson.Registry (Debug build)")]
#else
[assembly: AssemblyTitle("Guson.Registry")]
#endif

//// Assembly information
[assembly: AssemblyDescription("Contains classes that handle the Windows Registry.")]
[assembly: AssemblyCompany("Guson")]
[assembly: AssemblyProduct("Guson.Registry")]
[assembly: AssemblyCopyright("Copyright © Guson 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguageAttribute("en")]

//// Assembly Configuration
#if DEBUG
#if NET40
[assembly: AssemblyConfiguration("Class library type, Debug|AnyCPU build for .NET V4.0 target.")]
#elif NET401
[assembly: AssemblyConfiguration("Class library type, Debug|AnyCPU build for .NET V4.0.1 target.")]
#elif NET45
[assembly: AssemblyConfiguration("Class library type, Debug|AnyCPU build for .NET V4.5 target.")]
#else
[assembly: AssemblyConfiguration("Class library type, Debug|AnyCPU build for unknown .NET target.")]
#endif
#else
#if NET40
[assembly: AssemblyConfiguration("Class library type, Release|AnyCPU build for .NET V4.0 target.")]
#elif NET401
[assembly: AssemblyConfiguration("Class library type, Release|AnyCPU build for .NET V4.0.1 target.")]
#elif NET45
[assembly: AssemblyConfiguration("Class library type, Release|AnyCPU build for .NET V4.5 target.")]
#else
[assembly: AssemblyConfiguration("Class library type, Release|AnyCPU build for unknown .NET target.")]
#endif
#endif

//// Not visible to other COM components
[assembly: ComVisible(false)]
//// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f521af93-c78c-4c1f-9f95-4365c8a3dd59")]

//// Version information for an assembly consists of the following four values:
//// Major Version ; Minor Version ; Build Number ; Revision
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
