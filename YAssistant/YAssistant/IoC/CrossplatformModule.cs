using Autofac;
using BegunokApp.ViewModel;

namespace BegunokApp.IoC
{
    class CrossplatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainPageViewModel>();
        }
    }
}
