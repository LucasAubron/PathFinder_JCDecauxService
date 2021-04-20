using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routing
{
    class Station
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public float[] GetCoordinates()
        {
            float[] coordinates = { this.position.longitude, this.position.latitude };
            return coordinates;
        }

        public override string ToString()
        {
            return
                "Name: " + this.name +
                "\n" + "Address: " + this.address +
                "\n" + "Latitude: " + this.position.latitude +
                "\n" + "Longitude: " + this.position.longitude;
        }
    }

    class Position
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }
}
