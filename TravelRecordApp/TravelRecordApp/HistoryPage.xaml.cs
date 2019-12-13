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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                using (var conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>(); 
                    List<Post> posts = conn.Table<Post>().ToList();
                    ListViewPosts.ItemsSource = posts;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", ex.Message, "Ok");
            }

        }

        private async void ListViewPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = ListViewPosts.SelectedItem as Post;
            if (selectedPost != null)
            {
                // Naviagte to Detail Page
                await Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
            
        }
    }
}