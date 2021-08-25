using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BegunokApp.DB;
using BegunokApp.Droid.Services;
using BegunokApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BegunokApp.Droid.Models
{
    public abstract class Begunok
    {

        public Begunok()
        {
            Activities = new ObservableCollection<IActivity>();
        }

        public int ActivityCount { get; set; }

        public ObservableCollection<IActivity> Activities { get; set; }

        public string HowLeftIs => Xamarin.Essentials.Preferences.Get("posOfBegunokVizualization", 0).ToString();

        public bool IsRunning => AndroidServiceHandler.IsRunning;

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

        public virtual void AddActivity(string activityName, TimeSpan activityTime, Color activityColor)
        {
            Activity tempActivity = new Activity(activityName, activityTime, activityColor);
            Activities.Add(tempActivity);
            ActivityCount++;
        }

        public virtual void ClearBegunok()
        {
            ActivityCount = 0;
            Activities.Clear();
            Activity.ActivityCount = 0;
        }

        public virtual void DeleteActivity(int id)
        {
            Activities.Remove(Activities[id]);
            ActivityCount--;
        }

        protected void SetBegunokDataFromDatabase()
        {
            if(Activities.Count>0)
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
        }
    }
}