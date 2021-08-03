using CommonServiceLocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YAssistant.Models;
using YAssistant.Services;
using YAssistant.ViewModel;


namespace YAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBegunokActivityPage : ContentPage
    {
        public CreateBegunokActivityPage(INavigationService navigation, IBegunok begunok)
        {
            InitializeComponent();
            this.BindingContext = new CreateBegunokActivityPageViewModel(navigation, begunok);
        }
    }
}