using Syncfusion.DataSource.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TheWorkbook.Model;
using Xamarin.Forms;

namespace TheWorkbook
{
    class GroupHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            if (targetType.Name == "Color")
            {
                if ((int)value == 1)
                {
                    return Application.Current.Resources["buttonscolor"];
                }
                else if ((int)value == 2)
                {
                    return Application.Current.Resources["headerfootercolor"];
                }
                else 
                {
                    return Application.Current.Resources["buttonscolor"];
                }
            }
            else
            {
                
                var items = value as IEnumerable;
                if (items != null)
                {
                    var groupitems = items.ToList<object>().ToList<object>();

                    if (groupitems != null)
                    {
                        result = groupitems.Count; 
                    }
                }
                return $"Jobs scheduled: {result}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
