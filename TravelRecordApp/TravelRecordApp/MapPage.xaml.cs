using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public bool hasLocationPermission = false;

        public MapPage()
        {
            InitializeComponent();
            GetPermissions();
        }
        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need location", "Allow app to access location?", "OK", "Cancel");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);

                    status = results[Permission.LocationWhenInUse];
                }

                if (status == PermissionStatus.Granted)
                {
                    //Permission granted, do what you want do.
                    hasLocationPermission = true;
                    MapLocations.IsShowingUser = hasLocationPermission;
                    GetLocation();
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Cannot show your current location.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Location Denied", ex.Message, "OK");
            }
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged; // Subscribe to the Position Changed method
                await locator.StartListeningAsync(TimeSpan.Zero, 100); // Listen for changes on the map
            }
            GetLocation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossGeolocator.Current.StopListeningAsync(); // Stop Listening for changed 
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged; // Unsubscribe from position changed method
        }

        /// <summary>
        /// Executed everytime there's a location change on the Map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            MoveMap(e.Position); // Move to new position on map
        }

        /// <summary>
        /// Get current location based on user's consented permission
        /// </summary>
        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                MoveMap(position);
            }
        }

        /// <summary>
        /// Move to location on map based on Latitude and Longitude passed.
        /// </summary>
        /// <param name="position"></param>
        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var mapSpan = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            MapLocations.MoveToRegion(mapSpan);
        }


    }

   
}