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

        private async void BackToHomeButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}