using System;
using Xamarin.Forms;
using BegunokApp.Models;

namespace BegunokApp.Droid.Models
{
    internal class Activity : IActivity
    {
        public static int ActivityCount = 0;
        private int id = 0;

        public Activity(string name, TimeSpan time, Color color)
        {
            Name = name;
            Time = time;
            Color = color;
            Length = SetActivityLength(time);
            State = ActivityState.Next;
            ActivityCount++;
        }

        public Activity(string name, TimeSpan time, Color color, ActivityState state, int length)
        {
            Name = name;
            Time = time;
            Color = color;
            Length = length;
            State = state;
        }

        public Activity(string name, TimeSpan time, Color color, ActivityState state, int length, int id)
        {
            Name = name;
            Time = time;
            Color = color;
            Length = length;
            State = state;
            this.id = id;
        }

        private int SetActivityLength(TimeSpan time)
        {
            int result = Convert.ToInt32(time.TotalSeconds.ToString()) / 10;
            if (result > 0)
                return result;

            return 1;
        }

        public int Id => id;
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public Color Color { get; set; }
        public int Length { get; private set; }
        public ActivityState State { get; set; }
    }
}