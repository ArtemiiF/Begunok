using Android.App;
using Android.Content;
using Android.OS;
using BegunokApp.DB;
using BegunokApp.Droid.Models;
using BegunokApp.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace BegunokApp.Droid.Services
{
    [Service(Name = "ru.artemiif.BegunokTimer")]
    class BegunokTimerService : Service
    {
        private TimerBegunok begunok;
        private Notification notification;
        private bool timerAlive = false;
        private DateTime ActivityEndsTime;
        private int currentActivityIndex = 0;
        private INotification notifService;

        public IBinder Binder { get; private set; }

        public override void OnCreate()
        {
            System.Diagnostics.Debug.WriteLine("Begunok timer create");
            begunok = new TimerBegunok();
            System.Diagnostics.Debug.WriteLine($"{begunok.ActivityCount}");

            base.OnCreate();
        }

        public override IBinder OnBind(Intent intent)
        {
            System.Diagnostics.Debug.WriteLine("Begunok timer bind");
            return null;
        }

        public const int ServiceRunningNotifID = 10000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            notifService = new NotificationService();
            notification = notifService.GetNotification();
            StartForeground(ServiceRunningNotifID, notification);


            StartTimer();

            return StartCommandResult.Sticky;
        }

        private void StartTimer()
        {
            System.Diagnostics.Debug.WriteLine("ActivityTimer");

            if (!timerAlive)
            {
                timerAlive = true;
                ChangeActivityToCurrentAndSetActivityTimer();
            }

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
               {
                   if (!timerAlive)
                       return timerAlive;

                   TimeSpan timeSpan = ActivityEndsTime - DateTime.Now + new TimeSpan(0, 0, 1);
                   begunok.Activities[currentActivityIndex].Time = timeSpan;

                   System.Diagnostics.Debug.WriteLine(timeSpan.ToString());

                   //ЭТО ПЕРЕДАЛТЬ
                   BegunokDB tempActivityDB = new BegunokDB(new Models.Activity(begunok.Activities[currentActivityIndex].Name,
                       begunok.Activities[currentActivityIndex].Time,
                       begunok.Activities[currentActivityIndex].Color,
                       begunok.Activities[currentActivityIndex].State,
                       begunok.Activities[currentActivityIndex].Length),
                       currentActivityIndex + 1);

                   System.Diagnostics.Debug.WriteLine($"Id:{tempActivityDB.Id}, Time:{tempActivityDB.TimeInSeconds}, State:{tempActivityDB.State}");
                   App.Database.SaveItem(tempActivityDB);

                   SetNotificationTitleAndText();

                   MessagingCenter.Send<BegunokTimerService>(this, "TimerUpdate");

                   if (timeSpan.Seconds % 10 == 0)
                   {
                       System.Diagnostics.Debug.WriteLine("BegunokVizualization moved to left");

                       // App.Current.Properties["posOfBegunokVizualization"] = Convert.ToInt32(App.Current.Properties["posOfBegunokVizualization"]) - 1;

                       int tempPos = Xamarin.Essentials.Preferences.Get("posOfBegunokVizualization", 0);

                       Xamarin.Essentials.Preferences.Set("posOfBegunokVizualization", tempPos - 1);

                       System.Diagnostics.Debug.WriteLine($"BegunokVizualization moved to left." +
                           $" Pos:{Xamarin.Essentials.Preferences.Get("posOfBegunokVizualization", 0)}");
                   }

                   if (timeSpan.TotalSeconds <= 0)
                   {
                       begunok.Activities[currentActivityIndex].State = ActivityState.Past;
                       App.Database.SaveItem(new BegunokDB(begunok.Activities[currentActivityIndex], currentActivityIndex + 1));
                       currentActivityIndex++;
                       ChangeActivityToCurrentAndSetActivityTimer();
                   }
                   return timerAlive;
               });
            System.Diagnostics.Debug.WriteLine("Timer");
        }

        private void SetNotificationTitleAndText()
        {
            notification = notifService.SetNotificationName(begunok.Activities[currentActivityIndex].Name);

            string hours = begunok.Activities[currentActivityIndex].Time.Hours.ToString();
            string minutes = begunok.Activities[currentActivityIndex].Time.Minutes.ToString();
            string seconds = begunok.Activities[currentActivityIndex].Time.Seconds.ToString();

            if (begunok.Activities[currentActivityIndex].Time.Hours < 10)
                hours = "0" + begunok.Activities[currentActivityIndex].Time.Hours.ToString();

            if (begunok.Activities[currentActivityIndex].Time.Minutes < 10)
                minutes = "0" + begunok.Activities[currentActivityIndex].Time.Minutes.ToString();

            if (begunok.Activities[currentActivityIndex].Time.Seconds < 10)
                seconds = "0" + begunok.Activities[currentActivityIndex].Time.Seconds.ToString();

            notification = notifService.SetNotificationText($"Time remain:{hours}:" +
                $"{minutes}:" +
                $"{seconds}");

            StartForeground(ServiceRunningNotifID, notification);
        }

        private void ChangeActivityToCurrentAndSetActivityTimer()
        {

            Xamarin.Essentials.Vibration.Vibrate();

            if (begunok.Activities.Last().State == ActivityState.Past)
            {
                timerAlive = false;
                begunok.ClearBegunok();
                MessagingCenter.Send<BegunokTimerService>(this, "TimerUpdate");
                return;
            }

            begunok.Activities[currentActivityIndex].State = ActivityState.Current;
            ActivityEndsTime = new DateTime(DateTime.Now.Ticks + begunok.Activities[currentActivityIndex].Time.Ticks);
            System.Diagnostics.Debug.WriteLine($"Id:{begunok.Activities[currentActivityIndex].Id}, State:{begunok.Activities[currentActivityIndex].State}");
        }

        public override void OnDestroy()
        {
            timerAlive = false;
            base.OnDestroy();
        }

        public override bool StopService(Intent name)
        {
            timerAlive = false;
            return base.StopService(name);
        }

    }
}