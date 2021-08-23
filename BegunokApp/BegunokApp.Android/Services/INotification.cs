using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BegunokApp.Droid.Services
{
    interface INotification
    {
        Notification GetNotification();
        Notification SetNotificationName(string name);
        Notification SetNotificationText(string text);
        Notification SetVibroAndSound();
    }
}