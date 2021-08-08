using System;
using Xamarin.Forms;

namespace YAssistant.Models
{
    public enum ActivityState
    {
        Next,
        Past,
        Current
    }

    public interface IActivity
    {
        string Name { get; set; }

        TimeSpan Time { get; set; }

        Color Color { get; set; }

        int Length { get; }

        ActivityState State { get; set; }
    }
}
