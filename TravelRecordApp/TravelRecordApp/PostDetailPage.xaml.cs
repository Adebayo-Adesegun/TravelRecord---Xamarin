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
    public partial class PostDetailPage : ContentPage
    {
        readonly Post _post;
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            _post = selectedPost;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            EntryExperience.Text = _post.Experience;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            _post.Experience = EntryExperience.Text;
            // Insert into SQLite
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                try
                {
                    int rows = conn.Update(_post);
                    if (rows > 0)
                    {
                        await DisplayAlert("Success", "Experience updated successfully", "Ok");
                        await Navigation.PushAsync(new HistoryPage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Experience was not updated", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Failure", ex.Message, "Ok");
                }
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            _post.Experience = EntryExperience.Text;
            // Insert into SQLite
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                try
                {
                    int rows = conn.Delete(_post);
                    if (rows > 0)
                    {
                        await DisplayAlert("Success", "Experience deleted successfully", "Ok");
                        await Navigation.PushAsync(new HistoryPage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Experience was not deleted", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Failure", ex.Message, "Ok");
                }
            }
        }
    }
}