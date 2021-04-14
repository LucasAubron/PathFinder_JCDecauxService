using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPS
{
    class JCDecauxItem
    {
        private string apiKey = "e3e335df65e7d602cef98a312a3a710335a2d268";
        private int number;
        private String contractName;
        private StationStatus station;
        private HttpClient client = new HttpClient();
        private String open = "OPEN";

        public JCDecauxItem(String id)
        {
            //id = contractname-stationnumber
            string[] credentials = id.Split('-');
            this.contractName = credentials[0];
            this.number = int.Parse(credentials[1]);
            this.updateStationInfo();
        }

        private void updateStationInfo()
        {
            Task waitStationCompletion = Task.Run(async () =>
            {
                //Console.WriteLine("JCDecaux API called for " + this.contractName + ": " + this.number);
                HttpResponseMessage response = await this.client.GetAsync(String.Concat("https://api.jcdecaux.com/vls/v3/stations/", this.number, "?contract=", this.contractName, "&apiKey=", this.apiKey));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                this.station = JsonSerializer.Deserialize<StationStatus>(responseBody);
            });
            waitStationCompletion.Wait();
        }

        public StationStatus GetStationStatus()
        {
            return this.station;
        }

        public Boolean IsStationOpen()
        {
            return this.station.status == open;
        }

        public Boolean IsStationFull()
        {
            return this.station.totalStands.availabilities.stands == 0;
        }

        public Boolean IsStationEmpty()
        {
            return this.station.totalStands.availabilities.bikes == 0;
        }

        public override string ToString()
        {
            return this.contractName + ": " + this.number + "\n" + this.station;
        }

    }
}
