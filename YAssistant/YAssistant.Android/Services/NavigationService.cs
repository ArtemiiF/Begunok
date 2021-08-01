using System.Threading.Tasks;
using YAssistant.Services;
using YAssistant.View;

namespace YAssistant.Droid.Services
{
    class NavigationService : INavigationService
    {
        public async Task NavigateToCreateBegunok()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateBegunokPage());
        }

        public async Task NavigateToCreateBegunokActivity()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateBegunokActivityPage());
        }

        public async Task NavigateToMain()
        {
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}