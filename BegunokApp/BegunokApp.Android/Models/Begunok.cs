using BegunokApp.Models;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace BegunokApp.Droid.Models
{
    public class Begunok : IBegunok
    {
        public event BegunokHandler Notify;
        public Begunok()
        {
            ActivityCount = 0;
            Activities = new ObservableCollection<IActivity>();
            SetStartPositionOfBegunokVizualization();
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

                return "0:00:00";
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

        public string HowLeftIs => posOfBegunokVizualization.ToString();


        private int posOfBegunokVizualization = 0;
        private int currentActivityIndex = 0;
        private bool timerAlive = false;
        private DateTime ActivityEndsTime;

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

        private void SetStartPositionOfBegunokVizualization()
        {
            Xamarin.Essentials.DisplayInfo screenInfo = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
            posOfBegunokVizualization = Convert.ToInt32(screenInfo.Width / screenInfo.Density - 0.5f) / 2;

            Debug.WriteLine("posOfBegunokVizualization:" + posOfBegunokVizualization);            
        }

        //Это просто ужасно но как передлать я пока незнаю 08.08.2021
        private void ActivityTimer()
        {
            Debug.WriteLine("ActivityTimer");
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                //Обработка на случай досрочного завершения бегунка
                if (!timerAlive)
                    return timerAlive;

                TimeSpan timeSpan = ActivityEndsTime - DateTime.Now + new TimeSpan(0, 0, 1);

                Activities[currentActivityIndex].Time = timeSpan;

                Debug.WriteLine(timeSpan.ToString());

                Notify?.Invoke("TimerUpdate");

                if (timeSpan.Seconds % 10 == 0)
                {
                    Debug.WriteLine("BegunokVizualization moved to left");
                    Debug.WriteLine("posOfBegunokVizualization:" + posOfBegunokVizualization);
                    posOfBegunokVizualization--;
                }
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

        public void AddActivity(string activityName, TimeSpan activityTime, Color activityColor)
        {
            Activities.Add(new Activity(activityName, activityTime, activityColor));
            ActivityCount++;
            Notify?.Invoke("AddActivity");
        }

        public void DeleteActivity(int id)
        {
            if (IsRunning)
            {
                App.Current.MainPage.DisplayAlert("Warning", "You can't delete activity while begunok is running", "Ok");
                return;
            }

            foreach (var item in Activities)
            {
                if (item.Id == id)
                {
                    Activities.Remove(item);
                    Debug.WriteLine($"{item.Name} is deleted");
                    ActivityCount--;
                    Notify?.Invoke("AddActivity");
                    return;
                }
            }

        }

        public void ClearBegunok()
        {
            timerAlive = false;
            ActivityCount = 0;
            SetStartPositionOfBegunokVizualization();
            Activities.Clear();
            Activity.ActivityCount = 0;
        }
    }
}