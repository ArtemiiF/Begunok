using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YAssistant.Models;
using YAssistant.Services;
using YAssistant.ViewModel;

namespace YAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBegunokPage : ContentPage , ICustomBackButton
    {
        public Action CustomBackButtonAction { get; set; }
        public CreateBegunokPage(INavigationService navigation, IBegunok begunok)
        {
            InitializeComponent();

            this.BindingContext = new CreateBegunokPageViewModel(navigation, begunok);
            CustomBackButtonAction += BackActionHandler;
        }

        protected void BackActionHandler()
        {
            CreateBegunokPageViewModel vm = (CreateBegunokPageViewModel)BindingContext;
            vm.OnBackButtonClicked.Invoke();
        }
    }
}