using Android.App;

namespace BegunokApp.Droid.Services
{
    interface INotification
    {
        Notification GetNotification();
        Notification SetNotificationName(string name);
        Notification SetNotificationText(string text);
        //Notification SetVibroAndSound();
    }
}