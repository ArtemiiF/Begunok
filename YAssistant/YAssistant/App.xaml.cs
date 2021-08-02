using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Xamarin.Forms;
using YAssistant.IoC;

namespace YAssistant
{
    public partial class App : Application
    {
        public App(Module platformModule)
        {
            InitializeComponent();
            InitializeDependencies(platformModule);

            MainPage = new NavigationPage(new View.MainPage())
            {
                BarBackgroundColor = Color.FromHex("#272537")      
            };
            
        }

        protected void InitializeDependencies(Module platformModule)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new CrossplatformModule());
            builder.RegisterModule(platformModule);

            var locator = new AutofacServiceLocator(builder.Build());
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
