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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "StationStatus?contract={contract}&number={number}")]
        [OperationContract]
        string GetStationStatus(string contract, int number);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Contracts")]
        [OperationContract]
        string GetContracts();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "StationsStatus?contract={contract}")]
        [OperationContract]
        string GetStationsStatusFromContract(string contract);
    }

}
