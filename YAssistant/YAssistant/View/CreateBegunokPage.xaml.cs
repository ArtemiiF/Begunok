using CommonServiceLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YAssistant.ViewModel;

namespace YAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBegunokPage : ContentPage
    {
        public CreateBegunokPage()
        {
            InitializeComponent();
            this.BindingContext = ServiceLocator.Current.GetInstance<CreateBegunokPageViewModel>();
        }
    }
}