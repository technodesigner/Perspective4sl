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
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Perspective.Model;
using Perspective.Core;
using System.Net.NetworkInformation;
using System.Threading;
using System.Globalization;

namespace Perspective
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Host.Settings.EnableFrameRateCounter = true;
            // Host.Settings.EnableRedrawRegions = true;
            // Host.Settings.MaxFrameRate = 60;

            // Host.Settings.EnableCacheVisualization = true;

            //ParametersHelper.LoadInitParametersFrom(e.InitParams);
            //string uiCulture = ParametersHelper.ReadParameterValue("uiculture");
            //if (!String.IsNullOrEmpty(uiCulture)
            //    && !uiCulture.Equals(Thread.CurrentThread.CurrentUICulture.Name))
            //{
            //    var cultureinfo = new CultureInfo(uiCulture);
            //    Thread.CurrentThread.CurrentCulture = cultureinfo;
            //    Thread.CurrentThread.CurrentUICulture = cultureinfo;
            //}

            // if (!Application.Current.IsRunningOutOfBrowser)
            //{
            //    // The ExtensionModel is loaded before RootVisual assignation
            //    // because otherwise fragment navigation from the browser address bar
            //    // may occur before modules are loaded
            ExtensionModel = new ExtensionModel();
            ExtensionModel.ExtensionLinksLoaded +=
                (sender1, e1) =>
                {
                    this.RootVisual = new View.MainPage();
                };
            ExtensionModel.LoadExtensionLinks();
            this.InstallStateChanged +=
                (sender1, e1) =>
                {
                    switch (this.InstallState)
                    {
                        case System.Windows.InstallState.Installing:
                            ExtensionModel.InstallExtensionFiles();
                            break;

                        case System.Windows.InstallState.NotInstalled:
                            ExtensionModel.UninstallExtensionFiles();
                            break;
                    }
                };


            //}
            //else
            //{
            //    // Out Of Browser mode needs more delay to load IsolatedStorageSettings.ApplicationSettings 
            //    // used by ExtensionModel, and doesn't use fragment navigation from the browser address bar
            //    this.RootVisual = new View.MainPage();
            //}
            // this.RootVisual = new View.MainPage();
        }

        public ExtensionModel ExtensionModel { get; private set; }


        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
