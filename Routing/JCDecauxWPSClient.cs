using Routing.WPS_JCDecaux;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Routing
{
    class JCDecauxWPSClient
    {
        private string apiKey = "e3e335df65e7d602cef98a312a3a710335a2d268";
        private Station[] stations;
        private StationServiceClient stationServiceClient = new StationServiceClient();
        public JCDecauxWPSClient()
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
                this.stations = JsonSerializer.Deserialize<Station[]>(responseBody);
            });
            waitStations.Wait();
        }
        public Station NearestStationAvailableForPickUp(float latitude, float longitude)
        {
            StationProximity calculator = new StationProximity();
            Station[] sortedStations = calculator.SortStationsByProximity(this.stations, latitude, longitude);
            int indice = 0;
            Station challenger = sortedStations[indice];
            while(!this.stationServiceClient.GetStationIsAvailableForPickUp(String.Concat(challenger.contractName,"-", challenger.number))){
                challenger = sortedStations[++indice];
            }
            return challenger;
        }

        public Station NearestStationAvailableForDeposit(float latitude, float longitude)
        {
            StationProximity calculator = new StationProximity();
            Station[] sortedStations = calculator.SortStationsByProximity(this.stations, latitude, longitude);
            int indice = 0;
            Station challenger = sortedStations[indice];
            while (!this.stationServiceClient.GetStationIsAvailableForDeposit(String.Concat(challenger.contractName, "-", challenger.number)))
            {
                challenger = sortedStations[++indice];
            }
            return challenger;
        }
        public Station[] GetStations()
        {
            return this.stations;
        }
    }
}