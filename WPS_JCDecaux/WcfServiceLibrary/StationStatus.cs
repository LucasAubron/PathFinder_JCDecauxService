using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPS
{
    class StationStatus
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public string status { get; set; }
        public Totalstands totalStands { get; set; }
    }

    public class Position
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Totalstands
    {
        public Availabilities availabilities { get; set; }
    }

    public class Availabilities
    {
        public int bikes { get; set; }
        public int stands { get; set; }

    }
}
