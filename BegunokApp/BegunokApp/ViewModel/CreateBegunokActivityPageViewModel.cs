using BegunokApp.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using BegunokApp.Models;

namespace BegunokApp.ViewModel
{
    class CreateBegunokActivityPageViewModel : BaseViewModel
    {
        private INavigationService NavigationService { get; set; }
        private IBegunok Begunok { get; set; }

        public ICommand AddActivityClicked { get; private set; }
        public string NameActivity { get; set; }
        public TimeSpan ActivityTime { get; set; }
        public Color ActivityColor { get; set; }
        public CreateBegunokActivityPageViewModel(INavigationService navigation, IBegunok begunok)
        {
            NavigationService = navigation;
            Begunok = begunok;
            AddActivityClicked = new Command(AddActivity);
        }

        private void AddActivity()
        {
            if (ActivityTime.TotalMinutes < 1 || NameActivity.Length < 1)
            {
                App.Current.MainPage.DisplayAlert("Error", "Name or/and time are empty", "ok");
                return;
            }

            Begunok.AddActivity(NameActivity, ActivityTime, ActivityColor);
            NavigationService.PopPage();
        }
    }
}
