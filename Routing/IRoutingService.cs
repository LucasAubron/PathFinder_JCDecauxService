using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Routing
{
    [ServiceContract]
    public interface IRoutingService
    {
        /*
        [OperationContract]
        float[] Test(string address);
        */

        [OperationContract]
        string GetPath(string startingPos, string endingPos);


    }

}
