using System.Collections.Generic;
using YAssistant.Models;
using Android.Views;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace YAssistant.Droid.Models
{
    class Begunok : IBegunok
    {
        public event BegunokHandler Notify;
        public Begunok()
        {
            ActivityCount = 0;
            Activities = new ObservableCollection<IActivity>();
        }

        public int ActivityCount { get; set; }

        public ObservableCollection<IActivity> Activities { get; set; }

        public string TimeToNextActivity
        {
            get
            {
                foreach (var item in Activities)
                {
                    if (item.State == ActivityState.Current)
                        return new TimeSpan(item.Time.Hours, item.Time.Minutes, item.Time.Seconds).ToString();
                }

                return "0:00";
            }
        }

        public string CurrentActivityName
        {
            get
            {
                foreach (var item in Activities)
                {
                    if (item.State == ActivityState.Current)
                        return item.Name;
                }

                return "No activites";
            }
        }

        public bool IsRunning => timerAlive;

        private int currentActivityIndex = 0;
        private bool timerAlive = false;
        DateTime ActivityEndsTime;

        public void StartBegunok()
        {
            timerAlive = true;
   
            ChangeActivityToCurrentAndSetActivityTimer();
            ActivityTimer();
        }

        
        private void ChangeActivityToCurrentAndSetActivityTimer()
        {
            if (Activities.Last().State == ActivityState.Past)
            {
                timerAlive = false;
                ClearBegunok();
                Notify?.Invoke("BegunokEnds");
                return;
            }

            Debug.WriteLine("ChangeActivityToCurrent \npointer:" + currentActivityIndex + "\nsize:" + ActivityCount);
            Activities[currentActivityIndex].State = ActivityState.Current;
            ActivityEndsTime = new DateTime(DateTime.Now.Ticks + Activities[currentActivityIndex].Time.Ticks);

            Notify?.Invoke("ActivityChanged");
        }

        //Должен обновлять время для current activity пока не выключится/или пока есть активночти
        private void ActivityTimer()
        {
            Debug.WriteLine("ActivityTimer");
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                TimeSpan timeSpan = ActivityEndsTime - DateTime.Now + new TimeSpan(0, 0, 1);

                Activities[currentActivityIndex].Time = timeSpan;

                Debug.WriteLine(timeSpan.ToString());

                Notify?.Invoke("TimerUpdate");

                if (timeSpan.Ticks <= 0)
                {
                    Activities[currentActivityIndex].State = ActivityState.Past;
                    currentActivityIndex++;
                    ChangeActivityToCurrentAndSetActivityTimer();
                }

                return timerAlive;
            });

            Debug.WriteLine("Timer ");

            //Notify?.Invoke("ActivityEnds");
        }

        public void AddActivity(string activityName, TimeSpan activityTime)
        {
            Activities.Add(new Activity(activityName, activityTime));
            ActivityCount++;
            Notify?.Invoke("AddActivity");
        }

        public void ClearBegunok()
        {
            ActivityCount = 0;
            Activities.Clear();
            Notify?.Invoke("Delete");
        }
    }
}