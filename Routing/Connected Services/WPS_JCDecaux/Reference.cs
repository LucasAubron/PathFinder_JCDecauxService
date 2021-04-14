﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Routing.WPS_JCDecaux {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WPS_JCDecaux.IStationService")]
    public interface IStationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStationService/GetStationStatus", ReplyAction="http://tempuri.org/IStationService/GetStationStatusResponse")]
        string GetStationStatus(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStationService/GetStationStatus", ReplyAction="http://tempuri.org/IStationService/GetStationStatusResponse")]
        System.Threading.Tasks.Task<string> GetStationStatusAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStationService/GetStationIsAvailableForPickUp", ReplyAction="http://tempuri.org/IStationService/GetStationIsAvailableForPickUpResponse")]
        bool GetStationIsAvailableForPickUp(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStationService/GetStationIsAvailableForPickUp", ReplyAction="http://tempuri.org/IStationService/GetStationIsAvailableForPickUpResponse")]
        System.Threading.Tasks.Task<bool> GetStationIsAvailableForPickUpAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStationService/GetStationIsAvailableForDeposit", ReplyAction="http://tempuri.org/IStationService/GetStationIsAvailableForDepositResponse")]
        bool GetStationIsAvailableForDeposit(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStationService/GetStationIsAvailableForDeposit", ReplyAction="http://tempuri.org/IStationService/GetStationIsAvailableForDepositResponse")]
        System.Threading.Tasks.Task<bool> GetStationIsAvailableForDepositAsync(string id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStationServiceChannel : Routing.WPS_JCDecaux.IStationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StationServiceClient : System.ServiceModel.ClientBase<Routing.WPS_JCDecaux.IStationService>, Routing.WPS_JCDecaux.IStationService {
        
        public StationServiceClient() {
        }
        
        public StationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetStationStatus(string id) {
            return base.Channel.GetStationStatus(id);
        }
        
        public System.Threading.Tasks.Task<string> GetStationStatusAsync(string id) {
            return base.Channel.GetStationStatusAsync(id);
        }
        
        public bool GetStationIsAvailableForPickUp(string id) {
            return base.Channel.GetStationIsAvailableForPickUp(id);
        }
        
        public System.Threading.Tasks.Task<bool> GetStationIsAvailableForPickUpAsync(string id) {
            return base.Channel.GetStationIsAvailableForPickUpAsync(id);
        }
        
        public bool GetStationIsAvailableForDeposit(string id) {
            return base.Channel.GetStationIsAvailableForDeposit(id);
        }
        
        public System.Threading.Tasks.Task<bool> GetStationIsAvailableForDepositAsync(string id) {
            return base.Channel.GetStationIsAvailableForDepositAsync(id);
        }
    }
}