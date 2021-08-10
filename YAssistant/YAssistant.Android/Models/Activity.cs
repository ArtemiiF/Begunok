using System;
using Xamarin.Forms;
using BegunokApp.Models;

namespace BegunokApp.Droid.Models
{
    class Activity : IActivity
    {
        public static int ActivityCount = 0;
        private int id;
        public Activity(string name, TimeSpan time, Color color)
        {
            Name = name;
            Time = time;
            Color = color;
            Length = 0;
            State = ActivityState.Next;
            id = ActivityCount++;
        }

        public int Id => id;
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public Color Color { get; set; }
        public int Length { get; private set; }
        public ActivityState State { get; set;}
    }
}