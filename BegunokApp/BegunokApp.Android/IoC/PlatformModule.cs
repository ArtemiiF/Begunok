using Autofac;
using BegunokApp.Droid.Models;
using BegunokApp.Droid.Services;
using BegunokApp.Models;
using BegunokApp.Services;

namespace BegunokApp.Droid.IoC
{
    class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<Begunok>().As<IBegunok>();
            builder.RegisterType<DB.BegunokDBRepository>().As<DB.IBegunokDBRepository>();
        }

    }
}