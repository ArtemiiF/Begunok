using BegunokApp.Models;
using BegunokApp.Droid.Services;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using BegunokApp.DB;

namespace BegunokApp.Droid.Models
{
    public class Begunok : IBegunok
    {
        public event BegunokHandler BegunokNotify;
        public Begunok()
        {
            Activities = new ObservableCollection<IActivity>();
            SetStartPositionOfBegunokVizualization();
            MessagingCenter.Subscribe<BegunokTimerService>(this, "TimerUpdate", MessageHandler);

            if (AndroidServiceHandler.IsRunning)
            {
                System.Diagnostics.Debug.WriteLine($"Load from DB");
                SetBegunokDataFromDatabase();
                
                System.Diagnostics.Debug.WriteLine("posOfBegunokVizualization:" + posOfBegunokVizualization);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"First Load");
                ActivityCount = 0;
            }
        }

        private void MessageHandler(BegunokTimerService obj)
        {
            System.Diagnostics.Debug.WriteLine("Begunok MessageRetrived");
            SetBegunokDataFromDatabase();
            BegunokNotify?.Invoke("TimerUpdate");
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

        public bool IsRunning => AndroidServiceHandler.IsRunning;

        public string HowLeftIs => posOfBegunokVizualization.ToString();

        private int posOfBegunokVizualization = 0;

        //Здесь запускается foregroundservice
        public void StartBegunok()
        {
            System.Diagnostics.Debug.WriteLine("StartBegunokButton clicked");
            AndroidServiceHandler.StartService<BegunokTimerService>(Android.App.Application.Context);
        }

        private void SetBegunokDataFromDatabase()
        {
            Activities.Clear();

            List<BegunokDB> tempList = App.Database.GetItems().ToList();

            for (int i = 0; i < tempList.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine($"Id:{tempList[i].Id}, Name:{tempList[i].Name}, Time:{tempList[i].TimeInSeconds}," +
                 //   $"Color:{Color.FromHex(tempList[i].Color)},State:{tempList[i].State}");

                Activities.Add(new Activity(tempList[i].Name, new TimeSpan(0, 0, 0, tempList[i].TimeInSeconds),
                    Color.FromHex(tempList[i].Color),
                    tempList[i].State,
                    tempList[i].Length,
                    tempList[i].Id));
            }
            ActivityCount = Activities.Count;
            SetStartPositionOfBegunokVizualization();
        }

        private void SetStartPositionOfBegunokVizualization()
        {
            if(AndroidServiceHandler.IsRunning)
            {
                posOfBegunokVizualization = Convert.ToInt32(App.Current.Properties["posOfBegunokVizualization"]);
                return;
            }


            Xamarin.Essentials.DisplayInfo screenInfo = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
            posOfBegunokVizualization = Convert.ToInt32(screenInfo.Width / screenInfo.Density - 0.5f) / 2;
            App.Current.Properties["posOfBegunokVizualization"] = posOfBegunokVizualization;
        }

        public void AddActivity(string activityName, TimeSpan activityTime, Color activityColor)
        {
            Activity tempActivity = new Activity(activityName, activityTime, activityColor);
            App.Database.SaveItem(new DB.BegunokDB(tempActivity));

            Activities.Add(tempActivity);
            ActivityCount++;

            BegunokNotify?.Invoke("AddActivity");
        }

        //Передалать
        public void DeleteActivity(int id)
        {
            if (IsRunning)
            {
                App.Current.MainPage.DisplayAlert("Warning", "You can't delete activity while begunok is running", "Ok");
                return;
            }


            Activities.Remove(Activities[id]);
            int indexOfDeleteitemDB = App.Database.DeleteItem(id);

            ActivityCount--;
            BegunokNotify?.Invoke("AddActivity");
            System.Diagnostics.Debug.WriteLine($"{Activities[id].Name} is deleted. Also id:{indexOfDeleteitemDB} of DB");
            return;

        }

        public void ClearBegunok()
        {     
            ActivityCount = 0;          
            Activities.Clear();
            Activity.ActivityCount = 0;
            AndroidServiceHandler.StopService<BegunokTimerService>(Android.App.Application.Context);
            SetStartPositionOfBegunokVizualization();
            App.Database.DeleteTableItems();           
        }
    }
}