using System;
using System.Threading;
using Xamarin.Forms;
using YAssistant.Models;

namespace YAssistant.Droid.Models
{
    class Activity : IActivity
    {
        public Activity()
        {
            Name = "";
            Time = new TimeSpan(0, 0, 0);
            Color = Color.Transparent;
            Length = 0;
        }

        public string Name { get ; set; }
        public TimeSpan Time { get; set; }
        public Color Color { get; set; }
        public int Length { get; private set; }
    }
}