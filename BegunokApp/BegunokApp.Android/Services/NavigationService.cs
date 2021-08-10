using BegunokApp.Services;
using System.Threading.Tasks;
using BegunokApp.Models;
using BegunokApp.View;

namespace BegunokApp.Droid.Services
{
    class NavigationService : INavigationService
    {
        public async Task NavigateToCreateBegunok(INavigationService navigation, IBegunok begunok)
        {
            //await App.Current.MainPage.Navigation.PushAsync();
            await App.Current.MainPage.Navigation.PushAsync(new CreateBegunokPage(navigation, begunok));
        }

        public async Task NavigateToCreateBegunokActivity(INavigationService navigation, IBegunok begunok)
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateBegunokActivityPage(navigation,begunok));
        }

        public async Task NavigateToMain()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async Task PopPage()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

    }
}