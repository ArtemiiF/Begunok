using System;
using System.Threading;
using Xamarin.Forms;

namespace YAssistant.Models
{
    public interface IActivity
    {
        string Name { get; set; }

        TimeSpan Time { get; set; }

        Color Color { get; set; }

        int Length { get; }
    }
}
