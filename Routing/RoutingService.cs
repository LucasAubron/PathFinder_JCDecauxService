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
using Paths;

namespace Routing
{
    public class RoutingService : IRoutingService
    {
        private JCDecauxWPSClient jcdClient = new JCDecauxWPSClient();
        private StationServiceClient stationServiceClient = new StationServiceClient();
        private OpenRouteServiceClient openRouteServiceClient = new OpenRouteServiceClient();


        public string GetPaths(string startingPos, string endingPos)
        {

            Uri u1 = new Uri("http://www.somesite.com?" + startingPos);
            Uri u2 = new Uri("http://www.somesite.com?" + endingPos);
            startingPos = u1.ToString().Split('?')[1];
            endingPos = u2.ToString().Split('?')[1];
            float[] startingCoordinate = openRouteServiceClient.AddressToCoordinate(startingPos);
            float[] endingCoordinate = openRouteServiceClient.AddressToCoordinate(endingPos);

            if (startingCoordinate==null || endingCoordinate == null)
            {
                return "404 - Address cannot be located";
            }

            Station startingStation = jcdClient.NearestStationAvailableForPickUp(startingCoordinate[0], startingCoordinate[1]);
            Station endingStation = jcdClient.NearestStationAvailableForDeposit(endingCoordinate[0], endingCoordinate[1]);
            List<Path> res = new List<Path>();

            // from starting position to nearest available station
            res.Add(openRouteServiceClient.GetPath(
                TravelType.Foot,
                startingCoordinate[0], startingCoordinate[1],
                startingStation.position.latitude, startingStation.position.longitude
                ));

            // from starting station to ending station
            res.Add(openRouteServiceClient.GetPath(
                TravelType.Bike,
                 startingStation.position.latitude, startingStation.position.longitude,
                 endingStation.position.latitude, endingStation.position.longitude
                ));

            // from ending station to ending postion
            res.Add(openRouteServiceClient.GetPath(
                TravelType.Foot,
                endingStation.position.latitude, endingStation.position.longitude,
                endingCoordinate[0], endingCoordinate[1]
                ));
            return JsonSerializer.Serialize<List<Path>>(res);
        }

        public string GetStationStatus(string contract, int number)
        {
            StationStatus res = jcdClient.GetStationStatus(contract, number);
            return JsonSerializer.Serialize<StationStatus>(res);
        }

        public string GetContracts()
        {
            return JsonSerializer.Serialize<Contract[]>(this.jcdClient.GetContracts());
        }

        public string GetStationsStatusFromContract(string contract)
        {
            return JsonSerializer.Serialize<StationStatus[]>(this.jcdClient.GetStationsStatusFromContract(contract));
        }
    }
}

