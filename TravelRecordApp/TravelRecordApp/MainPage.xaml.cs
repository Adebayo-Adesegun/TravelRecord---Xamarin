using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntryUsername.Text) || string.IsNullOrEmpty(EntryUsername.Text))
            {
                await DisplayAlert("Error", "Please provide username and password", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new HomePage());

            }
        }
    }
}
