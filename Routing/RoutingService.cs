using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Threading;
using Routing.WPS_JCDecaux;

namespace Routing
{
    public class RoutingService : IRoutingService
    {
        private JCDecauxStations jcdStations = new JCDecauxStations();
        private StationServiceClient stationServiceClient = new StationServiceClient();
        private OpenRouteServiceClient openRouteServiceClient = new OpenRouteServiceClient();


        public string GetPath(string startingPos, string endingPos)
        {
            return ":/";
        }

        public float[] Test(string address)
        {
            return this.openRouteServiceClient.AddressToCoordinate(address);
        }
    }
}
