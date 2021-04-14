using System;
using WPS;
using System.Text.Json;

namespace WcfServiceLibrary
{
    public class StationService : IStationService
    {
        ProxyCache<JCDecauxItem> proxyCache = new ProxyCache<JCDecauxItem>();

        public string GetStationStatus(string id)
        {
            JCDecauxItem res = this.proxyCache.Get(id);
            string jsonString = JsonSerializer.Serialize<StationStatus>(res.GetStationStatus());
            return jsonString;
        }

        public Boolean GetStationIsAvailableForPickUp(string id)
        {
            JCDecauxItem res = this.proxyCache.Get(id);
            return !res.IsStationEmpty() && res.IsStationOpen();

        }

        public Boolean GetStationIsAvailableForDeposit(string id)
        {
            JCDecauxItem res = this.proxyCache.Get(id);
            return !res.IsStationFull() && res.IsStationOpen();
        }
    }
}
