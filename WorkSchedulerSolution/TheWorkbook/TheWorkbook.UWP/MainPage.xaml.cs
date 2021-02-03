using Syncfusion.SfDataGrid.XForms.UWP;
using Syncfusion.ListView.XForms.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TheWorkbook.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
this.InitializeComponent();
SfDataGridRenderer.Init();
            SfListViewRenderer.Init();


            /* 
            Android Example
            var dbName = "CustomersDb.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);

            UWP Example
            var dbName = "CustomersDb.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);

            Android Mine
            string dbname = "workbook.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbname);
            */

            string dbname = "workbook.sqlite";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbname);



            LoadApplication(new TheWorkbook.App(path));
        }
    }
}
