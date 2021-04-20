using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Routing
{
    [ServiceContract]
    public interface IRoutingService
    {

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Paths?start={startingPos}&end={endingPos}")]
        string GetPaths(string startingPos, string endingPos);
    }

}
