using Android.App;
using Android.Content;
using Android.OS;

namespace BegunokApp.Droid.Services
{
    internal static class AndroidServiceHandler
    {
        private static bool isOn = false;
        public static bool IsRunning => isOn;

        internal static void StartService<T>(this Context context, Bundle args = null) where T : Service
        {
            System.Diagnostics.Debug.WriteLine("Start service");
            var intent = new Intent(context, typeof(T));

            if (args != null)
            {
                intent.PutExtras(args);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                System.Diagnostics.Debug.WriteLine("Api>=26");
                context.StartForegroundService(intent);
                isOn = true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Api<26");
                context.StartService(intent);
                isOn = true;
            }
        }

        internal static void StopService<T>(this Context context) where T : Service
        {
            var intent = new Intent(context, typeof(T));
            context.StopService(intent);
            isOn = false;
        }
    }
}