using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Routing
{
    class JCDecauxStations
    {
        private string apiKey = "e3e335df65e7d602cef98a312a3a710335a2d268";
        private List<Station> stations;
        public JCDecauxStations()
        {
            this.LoadStation();
        }
        private void LoadStation()
        {
            Task waitStations = Task.Run(async () =>
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(String.Concat("https://api.jcdecaux.com/vls/v3/stations?contracts&apiKey=", this.apiKey));
                response.EnsureSuccessStatusCode();
                String responseBody = await response.Content.ReadAsStringAsync();
                this.stations = JsonSerializer.Deserialize<List<Station>>(responseBody);
            });
            waitStations.Wait();
        }
        public List<Station> GetStations()
        {
            return this.stations;
        }
    }
}