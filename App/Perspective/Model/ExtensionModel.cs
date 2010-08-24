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
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Perspective.Hosting;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;

namespace Perspective.Model
{
    /// <summary>
    /// Manages the data that represent the extensions.
    /// </summary>
    public class ExtensionModel
    {
        [ImportMany(AllowRecomposition = true)]
        public Lazy<Extension>[] MefExtensions { get; set; }
        
        public ObservableCollection<Extension> Extensions { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of MainModel.
        /// </summary>
        public ExtensionModel()
        {
            Extensions = new ObservableCollection<Extension>();
            Compose();
        }

        private void Compose()
        {
            var catalogs = new AggregateCatalog();

            int xapCounter = 0;
            string extensionList = Application.Current.Host.InitParams["Extensions"];
            foreach (string xap in extensionList.Split('|'))
            {
                xapCounter++;
                var catalog = new DeploymentCatalog(xap);
                catalog.DownloadCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        throw e.Error;
                    }
                    xapCounter--;
                    if (xapCounter == 0)    // on the last XAP loaded
                    {
                        try
                        {
                            // MEF import
                            CompositionInitializer.SatisfyImports(this);

                            foreach (var extension in MefExtensions)
                            {
                                Extensions.Add(extension.Value);
                            }
                            // Extensions.Sort(CompareExtensions);
                        }
                        catch (ChangeRejectedException ex)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var error in ex.Errors)
                            {
                                sb.Append(error.Description + "\n");
                            }
                            System.Windows.MessageBox.Show(sb.ToString());
                        }
                    }
                };
                catalog.DownloadAsync();
                catalogs.Catalogs.Add(catalog);
            }

            CompositionHost.Initialize(catalogs);
        }

        //private static int CompareExtensions(Extension e1, Extension e2)
        //{
        //    return e1.SortOrder.CompareTo(e2.SortOrder);
        //}
    }
}
