using BegunokApp.Services;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using BegunokApp.Models;
using System.Collections.ObjectModel;

namespace BegunokApp.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public ICommand ClickCommand { get; private set; }

        public ICommand EndBegunokCommand { get; private set; }
        private INavigationService NavigationService { get; set; }

        private IBegunok Begunok { get; set; }

        public string TimeBeforeCurrentActivityEnd => Begunok.TimeToNextActivity;

        public string NameOfCurrentActivity => Begunok.CurrentActivityName;

        public string HowLeftBegunokIs => Begunok.HowLeftIs;
        public ObservableCollection<IActivity> Activities { get; private set; }
        public MainPageViewModel(INavigationService navigation, IBegunok begunok)
        {
            NavigationService = navigation;
            Begunok = begunok;
            ClickCommand = new Command(CreateBegunokButtonClicked);
            EndBegunokCommand = new Command(EndBegunok);
            Activities = begunok.Activities;
            Begunok.BegunokNotify += BegunokHandler;
        }

        private void EndBegunok()
        {
            Begunok.ClearBegunok();
            OnPropertyChanged(nameof(HowLeftBegunokIs));
            OnPropertyChanged(nameof(TimeBeforeCurrentActivityEnd));
            OnPropertyChanged(nameof(Activities));
            OnPropertyChanged(nameof(NameOfCurrentActivity));
        }

        private void BegunokHandler(string str)
        {
            switch (str)
            {
                //Это ужасно
                case "TimerUpdate":
                    OnPropertyChanged(nameof(NameOfCurrentActivity));
                    OnPropertyChanged(nameof(HowLeftBegunokIs));
                    OnPropertyChanged(nameof(TimeBeforeCurrentActivityEnd));
                    OnPropertyChanged(nameof(Activities));                   
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
