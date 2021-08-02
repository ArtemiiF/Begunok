using System.Collections.Generic;
using YAssistant.Models;
using Android.Views;
using Xamarin.Forms;

namespace YAssistant.Droid.Models
{
    class Begunok : IBegunok
    {
        public event BegunokHandler Notify;
        public Begunok()
        {
            ActivityCount = 0;
            Activities = new List<IActivity>();
        }

        public int ActivityCount { get; set; }
        public List<IActivity> Activities { get; set; }
        public string TimeToNextActivity
        {
            get
            {
                if (ActivityCount == 0)
                    return "No Activites";

                return Activities[0].Time.ToString();
            }
        }
        public string CurrentActivityName
        {
            get
            {
                if (ActivityCount == 0)
                    return "No Activites";

                return Activities[0].Name;
            }
        }

        public void StartBegunok()
        {
            Notify?.Invoke("Start");
        }

        public void AddActivity()
        {
            //Activities.Add(activity);
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