using CommonServiceLocator;
using System;
using Xamarin.Forms;
using BegunokApp.ViewModel;

namespace BegunokApp.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = ServiceLocator.Current.GetInstance<MainPageViewModel>();
        }
    }
}
