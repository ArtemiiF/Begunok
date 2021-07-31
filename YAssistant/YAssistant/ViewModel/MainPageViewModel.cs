using System.Windows.Input;
using Xamarin.Forms;
using YAssistant.Services;

namespace YAssistant.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public ICommand ClickCommand { get; private set; }

        private INavigationService NavigationService1 { get; set; }

        public MainPageViewModel(INavigationService navigation)
        {
            NavigationService1 = navigation;
            ClickCommand = new Command(CreateBegunokButtonClicked);
        }

        private async void CreateBegunokButtonClicked()
        {           
            await NavigationService1.NavigateToCreateBegunok();
        }

    }
}
