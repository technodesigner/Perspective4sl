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

namespace Perspective.Core
{
    /// <summary>
    /// An helper class for isolated storage operations.
    /// </summary>
    public class IsolatedStorageHelper
    {
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

        // Risque d'exception si store fermé avant aboutissement de l'opération asynchrone
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

        public static void CopyFileToUserStoreForApplication(Stream source, string fileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                CopyFileToUserStore(store, source, fileName);
            }
        }

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
    }
}