using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YAssistant.Services
{
    public interface INavigationService
    {
        Task NavigateToCreateBegunok();

        Task NavigateToCreateBegunokActivity();

        Task NavigateToMain();

    }
}
