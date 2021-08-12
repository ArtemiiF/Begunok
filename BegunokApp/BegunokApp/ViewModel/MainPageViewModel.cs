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
            Activities = begunok.Activities;
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
                //Я знаю что обновление визуала бегунка сюда запихивать плохо но мне все равно ;-)
                case "TimerUpdate":
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
