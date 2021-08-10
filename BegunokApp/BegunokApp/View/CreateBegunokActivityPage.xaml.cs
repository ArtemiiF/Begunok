using BegunokApp.Services;
using CommonServiceLocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BegunokApp.Models;
using BegunokApp.ViewModel;


namespace BegunokApp.View
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