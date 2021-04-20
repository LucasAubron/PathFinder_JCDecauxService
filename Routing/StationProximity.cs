using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routing
{
    class StationProximity
    {
        public float DistanceBeetween(float latitude1, float longitude1, float latitude2, float longitude2)
        {
            return (float)Math.Sqrt(Math.Pow(latitude1 - latitude2, 2) + Math.Pow(longitude1 - longitude2, 2));
        }

        public Station[] SortStationsByProximity(Station[] stations, float latitude, float longitude)
        {
            Array.Sort(stations, (station1, station2) => {
                float distance1 = DistanceBeetween(latitude, longitude, station1.position.latitude, station1.position.longitude);
                float distance2 = DistanceBeetween(latitude, longitude, station2.position.latitude, station2.position.longitude);
                return distance1 > distance2 ? 1 : -1;
            });
            return stations;
        }
    }
}
