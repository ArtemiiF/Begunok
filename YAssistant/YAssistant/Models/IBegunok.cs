using System;
using System.Collections.Generic;

namespace YAssistant.Models
{
    public delegate void BegunokHandler(string message);
    public interface IBegunok
    {
        event BegunokHandler Notify;
        int ActivityCount { get; set; }

        List<IActivity> Activities { get; set; }

        string TimeToNextActivity { get;}

        string CurrentActivityName { get;}

        void StartBegunok();

        void AddActivity(string activityName, TimeSpan activityTime);

        void ClearBegunok();
    }
}
