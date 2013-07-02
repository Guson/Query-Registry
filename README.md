Query-Registry
==============

Class library to query the Windows registry, with optional filtering and possible to get a .REG file. The library contains to main classes [QueryRegistryKey, OpenRegistryKey], and several help classes and two WPF controls.

QueryRegistryKey
================
The [QueryRegistryKey] is used to query the registry, with optional filtering.

OpenRegistryKey
===============
The [OpenRegistryKey] is a static class to open a registry key.

Help classes
============
The [ItemCollection.cs] encapsulate the List class, and is used in the item classes, [ErrorItem.cs, FilterItem.cs, QueryItem.cs, ResultItem.cs]. The [GlobalSuppressions.cs] is only for Code Analysis.

Controls
========
The [RootKeyComboBox] is a combo box for the RootKeyType enum. The [RegisterKeyItem] is a user control with a root key and key name.

Code documentation
==================
The XML documentation is used by a Sandcastle Help File Bilder.