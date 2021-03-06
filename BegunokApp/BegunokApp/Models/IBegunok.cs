using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BegunokApp.Models
{
    public delegate void BegunokHandler(string message);
    public interface IBegunok
    {
        event BegunokHandler BegunokNotify;

        int ActivityCount { get; set; }

        string HowLeftIs { get; }

        ObservableCollection<IActivity> Activities { get; set; }

        bool IsRunning { get; }

        string TimeToNextActivity { get;}

        string CurrentActivityName { get;}

        void StartBegunok();

        void AddActivity(string activityName, TimeSpan activityTime, Color activityColor);

        void DeleteActivity(int id);

        void ClearBegunok();

        
    }
}
