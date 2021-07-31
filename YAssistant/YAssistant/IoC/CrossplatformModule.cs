using Autofac;
using YAssistant.ViewModel;

namespace YAssistant.IoC
{
    class CrossplatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainPageViewModel>();
            builder.RegisterType<CreateBegunokPageViewModel>();
            builder.RegisterType<CreateBegunokActivityPageViewModel>();
        }
    }
}
