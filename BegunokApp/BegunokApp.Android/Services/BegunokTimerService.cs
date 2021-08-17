using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BegunokApp.Droid.Services
{
    [Service(Name ="ru.artemiif.BegunokTimer")]
    class BegunokTimerService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            System.Diagnostics.Debug.WriteLine("Begunok timer bind");
            return null;
        }

        public const int ServiceRunningNotifID = 10000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            Notification notif = new NotificationService().GetNotification();
            StartForeground(ServiceRunningNotifID, notif);

            AwesomeFunc();

            return StartCommandResult.Sticky;
        }

        void AwesomeFunc()
        {

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }
    }
}