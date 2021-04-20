using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Paths;

namespace Routing
{
    class OpenRouteServiceClient
    {
        private String[] enumToString = { "cycling-regular", "foot-walking" };
        private HttpClient client = new HttpClient();
        private string openRouteServiceAPIKey = "5b3ce3597851110001cf624862a4d7fc598540538fc397a9414ea635";

        public float[] AddressToCoordinate(String address)
        {
            string responseBody = "Error: AddressToCordinate";
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

        public Path GetPath(TravelType travelType, float startLatitude, float startLongitude, float endLatitude, float endLongitude) {
            string responseBody = "Error: GetPath";
            Task waitForPath = Task.Run(async () =>
            {
                HttpResponseMessage response = await this.client.GetAsync(
                    "https://api.openrouteservice.org/v2/directions/" + this.enumToString[(int)travelType] +
                    "?api_key=" + this.openRouteServiceAPIKey +
                    "&start=" + string.Format("{0:G}", startLongitude).Replace(',', '.') + "," + string.Format("{0:G}", startLatitude).Replace(',', '.') +
                    "&end=" + string.Format("{0:G}", endLongitude).Replace(',', '.') + "," + string.Format("{0:G}", endLatitude).Replace(',', '.'));
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            });
            waitForPath.Wait();
            Path path = JsonSerializer.Deserialize<Path>(responseBody);
            return path;
            /*
            return "https://api.openrouteservice.org/v2/directions/" + this.enumToString[(int)travelType] +
                    "?api_key=" + this.openRouteServiceAPIKey +
                    "&start=" + string.Format("{0:G}", startLatitude).Replace(',', '.') + "," + string.Format("{0:G}", startLongitude).Replace(',', '.') +
                    "&end=" + string.Format("{0:G}", endLatitude).Replace(',', '.') + "," + string.Format("{0:G}", endLongitude).Replace(',', '.');
            */
        }


    }

    enum TravelType
    {
        Bike = 0,
        Foot = 1
    }
}
