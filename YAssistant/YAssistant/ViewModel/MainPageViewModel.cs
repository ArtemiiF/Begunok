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

        public string TimeBeforeCurrentActivityEnd
        {
            get
            {
                if (Begunok.Activities.Count == 0)
                    return "1:11";
                return "0:00";
            }        
        }

        public MainPageViewModel(INavigationService navigation,IBegunok begunok)
        {
            NavigationService = navigation;
            Begunok = begunok;
            ClickCommand = new Command(CreateBegunokButtonClicked);
        }

        private async void CreateBegunokButtonClicked()
        {           
            await NavigationService.NavigateToCreateBegunok();
        }

        private void ActivityTimerHandler()
        {

        }
    }
}
