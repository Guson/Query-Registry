![Project icon](http://icons.iconarchive.com/icons/aroche/delta/32/Registry-Settings-icon.png)
#Query-Registry
Class library to query the Windows registry, with optional filtering and possible to get a *.REG* file. The library contains to main classes _QueryRegistryKey, OpenRegistryKey_, and several help classes and two WPF controls.

##Main classes
The two main classes _QueryRegistryKey, OpenRegistryKey_, contains all logic.
###QueryRegistryKey
The _QueryRegistryKey_ is used to query the registry, with optional filtering.

###OpenRegistryKey
The _OpenRegistryKey_ is a static class to open a registry key.

##Help classes
The _ItemCollection.cs_ encapsulate the *List* class, and is used in the item classes, _ErrorItem.cs, FilterItem.cs, QueryItem.cs, ResultItem.cs_. The _GlobalSuppressions.cs_ is only for Code Analysis.

##Controls
The _RootKeyComboBox_ is a combo box for the _RootKeyType_ enum. The _RegisterKeyItem_ is a user control with a root key and key name.

##Code documentation
The XML documentation is used by a Sandcastle Help File Bulder project in the doc directory.

## Task list
- [ ] Add a [ContractInvariantMethod] to _ItemCollection.cs_, to require non null items.
- [ ] Clean unused/unnecessary code contacts
- [ ] Test the user controls
- [ ] Add NUnit test
- [ ] Add class diagram with the main classes
- [ ] Add class diagram for all help classes
