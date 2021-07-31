using Autofac;
using YAssistant.Droid.Services;
using YAssistant.Services;

namespace YAssistant.Droid.IoC
{
    class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<NavigationService>().As<INavigationService>();
        }

    }
}