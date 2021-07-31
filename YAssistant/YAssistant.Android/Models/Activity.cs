using System;
using Xamarin.Forms;
using YAssistant.Models;

namespace YAssistant.Droid.Models
{
    class Activity : IActivity
    {
        public string Name { get ; set; }
        public DateTime Time { get; set; }
        public Color ActivityColor { get; set; }
    }
}