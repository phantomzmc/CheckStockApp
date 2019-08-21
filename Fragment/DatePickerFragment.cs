using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CheckStockApp
{
    public class DatePickerFragment : DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        //Action<DateTime> _dateSelectedHandler = delegate { };
        Action<string> _dateSelectedHandler = delegate { };


        //public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        //{
        //    DatePickerFragment frag = new DatePickerFragment();
        //    frag._dateSelectedHandler = onDateSelected;
        //    return frag;
        //}

        public static DatePickerFragment NewInstance(Action<string> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                           this,
                                                           currently.Year,
                                                           currently.Month-1,
                                                           currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            try
            {
                int monthSum = monthOfYear + 1;
                string selectedDate = "" + year + "-" + monthSum + "-" + dayOfMonth;
                //DateTime selectedDate = new DateTime(year, monthOfYear, dayOfMonth);
                //Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);
            }
            catch(Exception ex)
            {
                int monthSum = monthOfYear + 1;
                string error = ex.Message;
                DateTime selectedDate = new DateTime(year, monthOfYear, dayOfMonth);
                Toast.MakeText(Activity, selectedDate.ToString(), ToastLength.Long).Show();

            }
        }
    }
}