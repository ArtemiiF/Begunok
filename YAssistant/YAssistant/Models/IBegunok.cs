using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace YAssistant.Models
{
    public delegate void BegunokHandler(string message);
    public interface IBegunok
    {
        event BegunokHandler Notify;
        int ActivityCount { get; set; }

        ObservableCollection<IActivity> Activities { get; set; }

        bool IsRunning { get; }

        string TimeToNextActivity { get;}

        string CurrentActivityName { get;}

        void StartBegunok();

        void AddActivity(string activityName, TimeSpan activityTime);

        void ClearBegunok();
    }
}
