using System;
using System.Windows.Input;
using Xamarin.Forms;
using YAssistant.Models;
using YAssistant.Services;

namespace YAssistant.ViewModel
{
    class CreateBegunokActivityPageViewModel : BaseViewModel
    {
        private INavigationService NavigationService { get; set; }
        private IBegunok Begunok { get; set; }
        public ICommand AddActivityClicked { get; private set; }
        public CreateBegunokActivityPageViewModel(INavigationService navigation, IBegunok begunok)
        {
            NavigationService = navigation;
            Begunok = begunok;
            AddActivityClicked = new Command(AddActivity);
        }

        public string NameActivity { get; private set; }

        public TimeSpan ActivityTime { get; private set; }

        public Color ActivityColor { get; set; }

        private void AddActivity()
        {
            Begunok.AddActivity(NameActivity, ActivityTime, ActivityColor);
            NavigationService.PopPage();
        }
    }
}
