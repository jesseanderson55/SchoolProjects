using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheWorkbook.Helper
{
    public class SchedulerAnimation
    {
        public static SfBusyIndicator BusyIndicator()
        {
            SfBusyIndicator busyIndicator = new SfBusyIndicator()
            {
                AnimationType = AnimationTypes.Ball,
                ViewBoxWidth = 100,
                ViewBoxHeight = 100,
                Title = "Loading...",
                BackgroundColor = (Color)Application.Current.Resources["backgroundcolor"],
                TextColor = (Color)Application.Current.Resources["highlightscolor"]
            };

            return busyIndicator;
        }
    }
}
