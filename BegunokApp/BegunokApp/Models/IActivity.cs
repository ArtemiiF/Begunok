using BegunokApp.DB;
using System;
using Xamarin.Forms;

namespace BegunokApp.Models
{
    public enum ActivityState
    {
        Next,
        Past,
        Current
    }

    public interface IActivity
    {
        int Id { get; }
        string Name { get; set; }

        TimeSpan Time { get; set; }

        Color Color { get; set; }

        int Length { get; }

        ActivityState State { get; set; }     
    }
}
