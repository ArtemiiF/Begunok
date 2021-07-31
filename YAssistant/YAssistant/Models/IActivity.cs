using System;
using Xamarin.Forms;

namespace YAssistant.Models
{
    public interface IActivity
    {
        string Name { get; set; }

        DateTime Time { get; set; }

        Color ActivityColor { get; set; }
    }
}
