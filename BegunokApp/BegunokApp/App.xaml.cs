using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Xamarin.Forms;
using BegunokApp.IoC;
using BegunokApp.DB;
using System.IO;
using System;
using Xamarin.Essentials;

namespace BegunokApp
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "begunok.db";
        private static BegunokDBRepository database;
        public static BegunokDBRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new BegunokDBRepository(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App(Module platformModule)
        {
            InitializeComponent();
            InitializeDependencies(platformModule);

            if (!App.Current.Properties.ContainsKey("posOfBegunokVizualization"))
            {
                App.Current.Properties.Add("posOfBegunokVizualization", 0);
            }            

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
