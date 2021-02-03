using Syncfusion.SfDataGrid.XForms.iOS;
using Syncfusion.XForms.iOS.Chat;
using Syncfusion.XForms.Pickers.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.Accordion;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfCarousel.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using Microsoft.WindowsAzure.MobileServices;

namespace TheWorkbook.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
global::Xamarin.Forms.Forms.Init();
SfDataGridRenderer.Init();
SfChatRenderer.Init();
SfDatePickerRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            SfAccordionRenderer.Init();
            SfListViewRenderer.Init();
            SfCarouselRenderer.Init();
            SfCalendarRenderer.Init();
            CurrentPlatform.Init();

            string dbname = "workbook.sqlite";
            string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "..", "Library");
            string fullPath = Path.Combine(folderPath, dbname);



            LoadApplication(new App(fullPath));




            return base.FinishedLaunching(app, options);
        }
    }
}
