﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWorkbook.Droid;
using TheWorkbook;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Java.IO;
using Xamarin.Forms;

namespace TheWorkbook.Droid
{
    public class SaveAndroid : ISave
    {
        public void Save(string filename, string contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string root = null;
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();
            Java.IO.File file = new Java.IO.File(myDir, filename);
            if (file.Exists()) file.Delete();
            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            if (file.Exists() && contentType != "application/html")
            {
                Android.Net.Uri path = Android.Net.Uri.FromFile(file);
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(path, mimeType);
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));

            }
        }
    }
}