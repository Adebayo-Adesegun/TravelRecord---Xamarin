using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;

namespace TravelRecordApp.Logic
{
    public class VenueLogic
    {
        readonly HttpClient _httpClient;
        public VenueLogic()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            var url = VenueRoot.GenerateURL(latitude, longitude);
            var response = await _httpClient.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

            var venues = venueRoot.Response.Venues as List<Venue>;
            return venues;
        }
    }
}
