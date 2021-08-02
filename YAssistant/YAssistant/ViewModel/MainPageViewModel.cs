using System;
using System.Windows.Input;
using Xamarin.Forms;
using YAssistant.Models;
using YAssistant.Services;

namespace YAssistant.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public ICommand ClickCommand { get; private set; }

        private INavigationService NavigationService { get; set; }

        private IBegunok Begunok { get; set; }

        public string ActivitesCount => Begunok.ActivityCount.ToString();

        public string TimeBeforeCurrentActivityEnd => Begunok.TimeToNextActivity;

        public string NameOfCurrentActivity => Begunok.CurrentActivityName;

        public MainPageViewModel(INavigationService navigation, IBegunok begunok)
        {
            NavigationService = navigation;
            Begunok = begunok;
            ClickCommand = new Command(CreateBegunokButtonClicked);
            Begunok.Notify += BegunokHandler;
        }

        private void BegunokHandler(string str)
        {
            switch (str)
            {
                case "Start":
                    OnPropertyChanged(nameof(ActivitesCount));
                    break;
                case "TimerUpdate":
                    OnPropertyChanged(nameof(TimeBeforeCurrentActivityEnd));
                    break;
                case "Delete":
                    OnPropertyChanged(nameof(ActivitesCount));
                    break;
                default:
                    break;
            }
        }

        private async void CreateBegunokButtonClicked()
        {
            await NavigationService.NavigateToCreateBegunok(NavigationService, Begunok);
        }

        private void ActivityTimerHandler()
        {

        }
    }
}
