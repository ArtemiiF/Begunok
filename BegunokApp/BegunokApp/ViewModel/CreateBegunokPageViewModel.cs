using BegunokApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using BegunokApp.Models;

namespace BegunokApp.ViewModel
{
    class CreateBegunokPageViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public Action OnBackButtonClicked { get; set; }
        public ICommand AddBegunokActivityCommand { get; private set; }
        public ICommand StartBegunokCommand { get; private set; }
        public ICommand DeleteActivityCommand { get; private set; }
        public ObservableCollection<IActivity> ActivitesList { get; private set; }

        protected INavigationService NavigationService { get; set; }

        private IBegunok Begunok { get; set; }

        public string ActivitesCount => Begunok.ActivityCount.ToString();

        public CreateBegunokPageViewModel(INavigationService navigation, IBegunok begunok)
        {
            NavigationService = navigation;
            Begunok = begunok;
            ActivitesList = Begunok.Activities;

            AddBegunokActivityCommand = new Command(CreateBegunokActivityButtonClicked);
            StartBegunokCommand = new Command(StartBegunokButtonClicked);
            DeleteActivityCommand = new Command(OnDeleteActivity);

            OnBackButtonClicked += BackButtonClicked;
            Begunok.BegunokNotify += BegunokHandler;    
        }

        protected void OnDeleteActivity()
        {
            Debug.WriteLine("DeleteActivityButtonClicked");
            Begunok.DeleteActivity(Id);
        }

        protected void BegunokHandler(string str)
        {
            switch (str)
            {
                case "AddActivity":
                    OnPropertyChanged(nameof(ActivitesCount));
                    OnPropertyChanged(nameof(ActivitesList));                 
                    break;
                default:
                    break;
            }
        }

        protected void BackButtonClicked()
        {
            if(!Begunok.IsRunning)
                Begunok.ClearBegunok();

            OnPropertyChanged(nameof(ActivitesCount));
            OnPropertyChanged(nameof(ActivitesList));
            NavigationService.NavigateToMain();
        }

        protected void CreateBegunokActivityButtonClicked()
        {
            NavigationService.NavigateToCreateBegunokActivity(NavigationService, Begunok);
        }

        protected void StartBegunokButtonClicked()
        {
            if (Begunok.ActivityCount == 0)
            {
                App.Current.MainPage.DisplayAlert("Warning", "No activities", "Ok");
                return;
            }
            if (!Begunok.IsRunning)
            {
                Begunok.StartBegunok();
            }
            NavigationService.NavigateToMain();
        }
    }
}
