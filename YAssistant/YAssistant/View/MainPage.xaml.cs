using CommonServiceLocator;
using System;
using Xamarin.Forms;
using YAssistant.ViewModel;

namespace YAssistant.View
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
