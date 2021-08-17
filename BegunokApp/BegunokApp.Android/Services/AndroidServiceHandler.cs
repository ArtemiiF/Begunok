using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BegunokApp.Droid.Services
{
    internal static class AndroidServiceHandler
    {
        public static void StartService<T>(this Context context, Bundle args = null) where T : Service
        {
            System.Diagnostics.Debug.WriteLine("Start service");
            var intent = new Intent(context, typeof(T));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                System.Diagnostics.Debug.WriteLine("Api>=26");
                context.StartForegroundService(intent);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Api<26");
                context.StartService(intent);
            }
        }

        public static void StopService<T>(this Context context) where T : Service
        {
            var intent = new Intent(context, typeof(T));
            context.StopService(intent);
        }
    }
}