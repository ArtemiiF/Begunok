using System.Threading.Tasks;
using BegunokApp.Models;

namespace BegunokApp.Services
{
    public interface INavigationService
    {
        Task NavigateToCreateBegunok(INavigationService navigation, IBegunok begunok);

        Task NavigateToCreateBegunokActivity(INavigationService navigation, IBegunok begunok);

        Task NavigateToMain();

        Task PopPage();
    }
}
