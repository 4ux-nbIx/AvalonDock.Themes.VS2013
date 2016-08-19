﻿/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using System;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Xceed AvalonDock VS2013 Theme")]
[assembly: AssemblyDescription("VS2013 Theme for Xceed AvalonDock")]

[assembly: AssemblyCompany("Roman '4ux-nbIx' Novitsky")]
[assembly: AssemblyProduct( "Xceed - AvalonDock" )]
[assembly: AssemblyCopyright("Copyright © Roman '4ux-nbIx' Novitsky 2013-2016")]



// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
[assembly: CLSCompliant( true )]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]


[assembly: XmlnsDefinition( "http://schemas.xceed.com/wpf/xaml/avalondock", "Xceed.Wpf.AvalonDock.Themes" )]

#pragma warning disable 1699
[assembly: AssemblyDelaySign( false )]
[assembly: AssemblyKeyFile( @"..\..\sn.snk" )]
[assembly: AssemblyKeyName( "" )]
#pragma warning restore 1699
