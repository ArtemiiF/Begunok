using System;
using System.Diagnostics;
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
                case "BegunokEnds":
                    Debug.WriteLine("BegunokEnds");
                    OnPropertyChanged(nameof(NameOfCurrentActivity));
                    OnPropertyChanged(nameof(TimeBeforeCurrentActivityEnd));
                    break;
                case "ActivityChanged":
                    OnPropertyChanged(nameof(NameOfCurrentActivity));
                    break;
                case "TimerUpdate":
                    OnPropertyChanged(nameof(TimeBeforeCurrentActivityEnd));
                    break;
                default:
                    break;
            }
        }

        private async void CreateBegunokButtonClicked()
        {
            await NavigationService.NavigateToCreateBegunok(NavigationService, Begunok);
        }

    }
}
