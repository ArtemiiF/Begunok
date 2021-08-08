using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using YAssistant.Models;
using YAssistant.Services;

namespace YAssistant.ViewModel
{
    class CreateBegunokPageViewModel : BaseViewModel
    {
        public Action OnBackButtonClicked { get; set; }
        public ICommand AddBegunokActivityCommand { get; private set; }
        public ICommand StartBegunokCommand { get; private set; }
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
            OnBackButtonClicked += BackButtonClicked;
            Begunok.Notify += BegunokHandler;    
        }

        private void BegunokHandler(string str)
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
            Begunok.ClearBegunok();
            OnPropertyChanged(nameof(ActivitesCount));
            NavigationService.NavigateToMain();
        }

        protected void CreateBegunokActivityButtonClicked()
        {
            NavigationService.NavigateToCreateBegunokActivity(NavigationService, Begunok);
        }

        protected void StartBegunokButtonClicked()
        {
            Begunok.StartBegunok();
            NavigationService.NavigateToMain();
        }
    }
}
