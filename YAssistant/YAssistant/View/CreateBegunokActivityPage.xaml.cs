using CommonServiceLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YAssistant.ViewModel;

namespace YAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBegunokActivityPage : ContentPage
    {
        public CreateBegunokActivityPage()
        {
            InitializeComponent();
            this.BindingContext = ServiceLocator.Current.GetInstance<CreateBegunokActivityPageViewModel>();
        }
    }
}