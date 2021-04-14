using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Routing
{
    class OpenRouteServiceClient
    {
        private HttpClient client = new HttpClient();
        private string openRouteServiceAPIKey = "5b3ce3597851110001cf624862a4d7fc598540538fc397a9414ea635";

        private float[] AddressToCoordinate(String address)
        {
            string responseBody = "Error";
            Task waitForCoordinates = Task.Run(async () =>
            {
                HttpResponseMessage response = await this.client.GetAsync(String.Concat("https://api.openrouteservice.org/geocode/search?api_key=", this.openRouteServiceAPIKey, "&text=", address));
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

            });
            waitForCoordinates.Wait();
            Coordinates coordinates = JsonSerializer.Deserialize<Coordinates>(responseBody);
            float[] res = coordinates.features[0].geometry.coordinates;
            Array.Reverse(res);
            return res;
        }
    }
}
