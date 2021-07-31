using System.Windows.Input;
using Xamarin.Forms;
using YAssistant.Services;

namespace YAssistant.ViewModel
{
    class CreateBegunokPageViewModel : BaseViewModel
    {
        public ICommand ClickCommand1 { get; private set; }

        protected INavigationService NavigationService { get; set; }

        public CreateBegunokPageViewModel(INavigationService navigation)
        {
            NavigationService = navigation;
            ClickCommand1 = new Command(CreateBegunokActivityButtonClicked);
        }

        protected virtual void CreateBegunokActivityButtonClicked()
        {
            NavigationService.NavigateToCreateBegunokActivity();
        }

    }
}
