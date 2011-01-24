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
using System.IO.IsolatedStorage;
using System.Text;

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
            // Host.Settings.EnableFrameRateCounter = true;
            // Host.Settings.EnableRedrawRegions = true;
            // Host.Settings.MaxFrameRate = 60;

            // Host.Settings.EnableCacheVisualization = true;

            var cultureinfo = IsolatedStorageHelper.LoadCultureSetting();
            if (cultureinfo != Thread.CurrentThread.CurrentCulture)
            {
                Thread.CurrentThread.CurrentCulture = cultureinfo;
                Thread.CurrentThread.CurrentUICulture = cultureinfo;
            }

            //ParametersHelper.LoadInitParametersFrom(e.InitParams);
            //string uiCulture = ParametersHelper.ReadParameterValue("uiculture");
            //if (!String.IsNullOrEmpty(uiCulture)
            //    && !uiCulture.Equals(Thread.CurrentThread.CurrentUICulture.Name))
            //{
            //    var cultureinfo = new CultureInfo(uiCulture);
            //    Thread.CurrentThread.CurrentCulture = cultureinfo;
            //    Thread.CurrentThread.CurrentUICulture = cultureinfo;
            //}

            ExtensionModel = new ExtensionModel();
            ExtensionModel.ExtensionLinksLoaded +=
                (sender1, e1) =>
                {
                    this.RootVisual = new View.MainPage();
                };
            ExtensionModel.LoadExtensionLinks(15); // timeout 15 s
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
        }

        public ExtensionModel ExtensionModel { get; private set; }


        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                var errorStringBuilder = new StringBuilder();
                var exception = e.ExceptionObject;
                errorStringBuilder.AppendLine(exception.ToString());
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    errorStringBuilder.AppendLine("");
                    errorStringBuilder.AppendLine("InnerException :");
                    errorStringBuilder.AppendLine(exception.ToString());
                }
                var errortext = errorStringBuilder.ToString();
                MessageBox.Show(errortext, "Exception", MessageBoxButton.OK);
            }
            else
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
