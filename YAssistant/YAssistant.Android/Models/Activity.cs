using System;
using System.Threading;
using Xamarin.Forms;
using YAssistant.Models;

namespace YAssistant.Droid.Models
{
    class Activity : IActivity
    {
        public Activity(string name, TimeSpan time)
        {
            Name = name;
            Time = time;
            Color = Color.Transparent;
            Length = 0;
            State = ActivityState.Next;
        }

        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public Color Color { get; set; }
        public int Length { get; private set; }
        public ActivityState State { get; set;}
    }
}