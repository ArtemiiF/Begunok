﻿using System;
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

        protected INavigationService NavigationService { get; set; }

        private IBegunok Begunok { get; set; }

        public string ActivitesCount => Begunok.ActivityCount.ToString();

        public CreateBegunokPageViewModel(INavigationService navigation, IBegunok begunok)
        {
            NavigationService = navigation;
            Begunok = begunok;
            AddBegunokActivityCommand = new Command(CreateBegunokActivityButtonClicked);
            StartBegunokCommand = new Command(StartBegunokButtonClicked);
            OnBackButtonClicked += BackButtonClicked;
        }

        protected void BackButtonClicked()
        {
            Begunok.ClearBegunok();
            OnPropertyChanged(nameof(ActivitesCount));
            NavigationService.NavigateToMain();
        }

        protected void CreateBegunokActivityButtonClicked()
        {
            //NavigationService.NavigateToCreateBegunokActivity();
            Begunok.AddActivity();
            OnPropertyChanged(nameof(ActivitesCount));
        }

        protected void StartBegunokButtonClicked()
        {
            Begunok.StartBegunok();
            NavigationService.NavigateToMain();
        }
    }
}
