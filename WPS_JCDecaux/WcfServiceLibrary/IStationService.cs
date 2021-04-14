using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [ServiceContract]
    public interface IStationService
    {
        [OperationContract]
        string GetStationStatus(string id);
        [OperationContract]
        Boolean GetStationIsAvailableForPickUp(string id);
        [OperationContract]
        Boolean GetStationIsAvailableForDeposit(string id);

    }
}
