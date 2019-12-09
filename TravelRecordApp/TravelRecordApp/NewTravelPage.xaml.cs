using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        private async void SaveTravelRecord_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntryExperience.Text))
                await DisplayAlert("Error", "Experience cannot be empty", "Ok");

            Post post = new Post()
            {
                Experience = EntryExperience.Text.Trim()
            };

            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                try
                {
                    conn.CreateTable<Post>();
                    int rows = conn.Insert(post);
                    if (rows > 0)
                    {
                        ClearControls();
                        await DisplayAlert("Success", "Experience added successfully", "Ok");
                    }
                    else
                    {
                        ClearControls();
                        await DisplayAlert("Error", "Experience was not added", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Failure", ex.Message, "Ok");
                }
            }
        }
        
        void ClearControls()
        {
            EntryExperience.Text = "";
        }
    }
} 