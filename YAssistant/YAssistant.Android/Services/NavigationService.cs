using System.Threading.Tasks;
using YAssistant.Models;
using YAssistant.Services;
using YAssistant.View;

namespace YAssistant.Droid.Services
{
    class NavigationService : INavigationService
    {
        public async Task NavigateToCreateBegunok(INavigationService navigation, IBegunok begunok)
        {
            //await App.Current.MainPage.Navigation.PushAsync();
            await App.Current.MainPage.Navigation.PushAsync(new CreateBegunokPage(navigation, begunok));
        }

        public async Task NavigateToCreateBegunokActivity()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateBegunokActivityPage());
        }

        public async Task NavigateToMain()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}