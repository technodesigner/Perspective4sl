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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Threading;

namespace Perspective.Core
{
    /// <summary>
    /// An helper class for isolated storage operations.
    /// </summary>
    public class IsolatedStorageHelper
    {
        /// <summary>
        /// Checks if there is enaough room for a stream in Isolated Storage, and tries to increase the quota if necessary.
        /// </summary>
        /// <param name="store">The isolated storage store.</param>
        /// <param name="stream">The Stream object.</param>
        public static void CheckFreeSpace(IsolatedStorageFile store, Stream stream)
        {
            Int64 spaceRequired = stream.Length;
            Int64 spaceAvailable = store.AvailableFreeSpace;
            if (spaceAvailable < spaceRequired)
            {
                if (!store.IncreaseQuotaTo(store.Quota + spaceRequired))
                {
                    throw new IsolatedStorageException();
                }
            }
        }

        /// <summary>
        /// Loads an image from a file in the user-scoped isolated storage for the application.
        /// </summary>
        /// <param name="fileName">The path of the image file.</param>
        /// <returns>A BitmapImage object.</returns>
        public static BitmapImage LoadImageFromUserStoreForApplication(string fileName)
        {
            BitmapImage image;
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream =
                    new IsolatedStorageFileStream(
                        fileName, FileMode.Open, store))
                {
                    image = new BitmapImage();
                    (image as BitmapImage).SetSource(stream);
                }
            }
            return image;
        }

        /// <summary>
        /// Copies a file asynchronously to the user-scoped isolated storage for the application.
        /// </summary>
        /// <param name="sourceUri">The Uri of the source file.</param>
        /// <param name="fileName">The path of the destination file.</param>
        public static void CopyFileToUserStoreForApplicationAsync(Uri sourceUri, string fileName)
        {
            //using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    CopyFileToUserStore(store, sourceUri, fileName);
            //}
            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted +=
                (sender, e) =>
                {
                    Stream stream = e.Result;
                    try
                    {
                        IsolatedStorageHelper.CopyFileToUserStoreForApplication(stream, fileName);
                    }
                    finally
                    {
                        stream.Close();
                    }
                };
            webClient.OpenReadAsync(sourceUri);
        }

        // Exception risk if store closed before completion of the asynchronous operation.
        //public static void CopyFileToUserStore(IsolatedStorageFile store, Uri sourceUri, string fileName)
        //{
        //    WebClient webClient = new WebClient();
        //    webClient.OpenReadCompleted +=
        //        (sender, e) =>
        //        {
        //            Stream stream = e.Result;
        //            try
        //            {
        //                IsolatedStorageHelper.CopyFileToUserStore(store, stream, fileName);
        //            }
        //            finally
        //            {
        //                stream.Close();
        //            }
        //        };
        //    webClient.OpenReadAsync(sourceUri);
        //}

        /// <summary>
        /// Copies a file synchronously to the user-scoped isolated storage for the application.
        /// </summary>
        /// <param name="source">The source Stream object.</param>
        /// <param name="fileName">The path of the destination file.</param>
        public static void CopyFileToUserStoreForApplication(Stream source, string fileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                CopyFileToUserStore(store, source, fileName);
            }
        }

        /// <summary>
        /// Copies a file synchronously to an user store of Isolated Storage.
        /// </summary>
        /// <param name="store">The isolated storage store.</param>
        /// <param name="source">The source stream object.</param>
        /// <param name="fileName">The path of the destination file.</param>
        public static void CopyFileToUserStore(IsolatedStorageFile store, Stream source, string fileName)
        {
            CheckFreeSpace(store, source);
            using (IsolatedStorageFileStream target =
                new IsolatedStorageFileStream(
                    fileName, FileMode.Create, store))
            {
                int bufferLength = 65536;
                byte[] buffer = new byte[bufferLength];
                int count;
                do
                {
                    count = source.Read(buffer, 0, bufferLength);
                    target.Write(buffer, 0, count);
                } while (count == bufferLength);
            }
        }

        /// <summary>
        /// Checks if a file exists in user-scoped isolated storage for the application.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <returns>A boolean value indicating if the file exists.</returns>
        public bool FileExistsInUserStoreForApplication(string filePath)
        {
            bool b = false;
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                b = store.FileExists(filePath);
            }
            return b;
        }

        /// <summary>
        /// Creates a (empty) file in user-scoped isolated storage for the application.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        public static void CreateFileInUserStoreForApplication(string filePath)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var fs = CreateFileInUserStore(store, filePath);
                fs.Close();
            }
        }

        /// <summary>
        /// Creates a file in user-scoped isolated storage.
        /// </summary>
        /// <param name="store">The isolated storage store.</param>
        /// <param name="filePath">The path of the file.</param>
        /// <returns>A IsolatedStorageFileStream object.</returns>
        public static IsolatedStorageFileStream CreateFileInUserStore(IsolatedStorageFile store, string filePath)
        {
            return store.CreateFile(filePath);
        }

        private const String CultureNameKey = "cultureName";

        /// <summary>
        /// Saves the culture name in the settings of the user-scoped isolated storage for the application.
        /// </summary>
        /// <param name="cultureName">The culture name.</param>
        public static void SaveCultureSetting(string cultureName)
        {
            IsolatedStorageSettings.ApplicationSettings[CultureNameKey] = cultureName;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        /// <summary>
        /// Loads the culture from the settings of the user-scoped isolated storage for the application.
        /// </summary>
        /// <remarks>The culture should have been previously saved by the SaveCultureSetting method.</remarks>
        /// <returns>A CultureInfo object.</returns>
        public static CultureInfo LoadCultureSetting()
        {
            var cultureinfo = Thread.CurrentThread.CurrentCulture;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(CultureNameKey))
            {
                cultureinfo = new CultureInfo((String)IsolatedStorageSettings.ApplicationSettings[CultureNameKey]);
            }
            return cultureinfo;
        }
    }
}