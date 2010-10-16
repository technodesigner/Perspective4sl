//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective4sl
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;

// Les informations générales relatives à un assembly dépendent de 
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: AssemblyTitle("Perspective.Core")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(Perspective.Core.LibraryInfo.Company)]
[assembly: AssemblyProduct("Perspective.Core")]
[assembly: AssemblyCopyright(Perspective.Core.LibraryInfo.Copyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly 
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de 
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible(false)]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid("df09a559-a80c-4120-aa8a-f50efc3ba6e4")]

// Les informations de version pour un assembly se composent des quatre valeurs suivantes :
//
//      Version principale
//      Version secondaire 
//      Numéro de build
//      Révision
//
// Vous pouvez spécifier toutes les valeurs ou indiquer les numéros de révision et de build par défaut 
// en utilisant '*', comme indiqué ci-dessous :
[assembly: AssemblyVersion(Perspective.Core.LibraryInfo.GlobalVersion)]
[assembly: AssemblyFileVersion(Perspective.Core.LibraryInfo.GlobalVersion)]

[assembly: CLSCompliant(true)]

namespace Perspective.Core
{
    /// <summary>
    /// A class to handle the default assembly attribute values for Perspective.
    /// </summary>
    public sealed class LibraryInfo
    {
        private LibraryInfo() { }

        /// <summary>
        /// Perspective global version number.
        /// 2.0.0.1 : 2.0 alpha
        /// 2.0.0.2 : 2.0 beta
        /// </summary>
        public const string GlobalVersion = "2.0.0.1";

        /// <summary>
        /// Global company name.
        /// </summary>
        public const string Company = "www.odewit.net";

        /// <summary>
        /// Global copyright information.
        /// </summary>
        public const string Copyright = "Copyright © http://perspective4sl.codeplex.com 2009-2010";
    }
}