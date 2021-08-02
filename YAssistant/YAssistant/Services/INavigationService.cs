using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YAssistant.Models;

namespace YAssistant.Services
{
    public interface INavigationService
    {
        Task NavigateToCreateBegunok(INavigationService navigation, IBegunok begunok);

        Task NavigateToCreateBegunokActivity();

        Task NavigateToMain();

    }
}
