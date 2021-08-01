using Autofac;
using YAssistant.Droid.Models;
using YAssistant.Droid.Services;
using YAssistant.Models;
using YAssistant.Services;

namespace YAssistant.Droid.IoC
{
    class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<Begunok>().As<IBegunok>();
        }

    }
}