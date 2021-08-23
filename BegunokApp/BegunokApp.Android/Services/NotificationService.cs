using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BegunokApp.Droid.Services
{
    class NotificationService : INotification
    {
        private static readonly string foregroundChannelId = "9001";
        private static readonly Context context = Application.Context;
        private NotificationCompat.Builder notifBuilder;

        public Notification GetNotification()
        {
            System.Diagnostics.Debug.WriteLine("Build notification");
            // Building intent
            var intent = new Intent(context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra("Title", "Message");

            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            notifBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
                .SetContentTitle("Your Title")
                .SetContentText("Main Text Body")
                .SetSmallIcon(Resource.Drawable.AddBegunokButton)
                .SetOngoing(true)
                .SetContentIntent(pendingIntent);

            // Building channel if API verion is 26 or above
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Title", NotificationImportance.High)
                {
                    Importance = NotificationImportance.High
                };
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationChannel.SetShowBadge(true);
                notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                if (context.GetSystemService(Context.NotificationService) is NotificationManager notifManager)
                {
                    notifBuilder.SetChannelId(foregroundChannelId);
                    notifManager.CreateNotificationChannel(notificationChannel);
                }
            }

            return notifBuilder.Build();
        }

        public Notification SetNotificationName(string name)
        {
            notifBuilder.SetContentTitle(name);
            return notifBuilder.Build();
        }

        public Notification SetVibroAndSound()
        {
            //notifBuilder.SetVibrate(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });
            //notifBuilder.SetSound(NotificationDefaults.Sound);
            return notifBuilder.SetDefaults((int)NotificationDefaults.Vibrate | (int)NotificationDefaults.Sound).Build();
        }

        public Notification SetNotificationText(string text)
        {
            notifBuilder.SetContentText(text);
            return notifBuilder.Build(); ;
        }
    }
}