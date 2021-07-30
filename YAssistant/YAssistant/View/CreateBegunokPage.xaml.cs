using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBegunokPage : ContentPage
    {
        public CreateBegunokPage()
        {
            InitializeComponent();
        }

        private async void AddBegunokActivityButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CreateBegunokActivityPage());
        }
    }
}