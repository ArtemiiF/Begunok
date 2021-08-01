using System.Windows.Input;
using Xamarin.Forms;
using YAssistant.Services;

namespace YAssistant.ViewModel
{
    class CreateBegunokPageViewModel : BaseViewModel
    {
        public ICommand ClickCommand { get; private set; }

        protected INavigationService NavigationService { get; set; }

        public CreateBegunokPageViewModel(INavigationService navigation)
        {
            NavigationService = navigation;
            ClickCommand = new Command(CreateBegunokActivityButtonClicked);
        }

        protected virtual void CreateBegunokActivityButtonClicked()
        {
            NavigationService.NavigateToCreateBegunokActivity();
        }

    }
}
