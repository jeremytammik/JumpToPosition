using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle( "JumpToPosition" )]
[assembly: AssemblyDescription( ".NET C# Revit add-in setting view target and view direction to specified values" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "" )]
[assembly: AssemblyProduct( "JumpToPosition" )]
[assembly: AssemblyCopyright( "Copyright (C) 2019 by Jeremy Tammik, The Building Coder, https://thebuildingcoder.typepad.com" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid( "645dea2a-82de-4aaa-87a5-884c8d262aa4" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// History:
//
// 2019-09-19 2020.0.0.1 working version with jump and view refresh
// 2019-09-26 2020.0.0.2 renamed CmdJumpToRevitPosition and implemented CmdJumpToBrowserView
// 2019-10-11 2020.0.0.3 renamed GetBrowserViewInfoFor
// 2019-10-11 2020.0.0.3 implemented GetLevelFor
// 2019-10-11 2020.0.0.3 implemented GetBrowserViewInfoFor helper methods, except for view direction
// 2019-10-11 2020.0.0.3 implemented GetView3dInfo
// 2019-10-11 2020.0.0.3 implemented GetViewInfo
// 2019-10-11 2020.0.0.3 tested, drop redundant 3D view info, add document version guid
// 2019-10-11 2020.0.0.3 convert data to TaskDialog MainContent
// 2019-10-11 2020.0.0.3 successfully tested
// 2019-10-11 2020.0.0.4 added DocumentVersion.NumberOfSaves
// 
[assembly: AssemblyVersion( "2020.0.0.4" )]
[assembly: AssemblyFileVersion( "2020.0.0.4" )]
