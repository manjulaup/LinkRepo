﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLayer.MRPService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="MRPService.Service")]
    public interface Service {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/GetCreditorFinal", ReplyAction="urn:Service/GetCreditorFinalResponse")]
        EntityHandler.LINKPayment[] GetCreditorFinal(EntityHandler.LINKPayment objPayment);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/GetCreditorFinalSupplierList", ReplyAction="urn:Service/GetCreditorFinalSupplierListResponse")]
        EntityHandler.LINKPayment[] GetCreditorFinalSupplierList();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/GetCreditorFinalSupplier", ReplyAction="urn:Service/GetCreditorFinalSupplierResponse")]
        EntityHandler.LINKPayment[] GetCreditorFinalSupplier(EntityHandler.LINKPayment objPayment);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/GetCreditorFinalSupplierByID", ReplyAction="urn:Service/GetCreditorFinalSupplierByIDResponse")]
        EntityHandler.LINKPayment[] GetCreditorFinalSupplierByID(EntityHandler.LINKPayment objPayment);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/GetCreditorFinalGRN", ReplyAction="urn:Service/GetCreditorFinalGRNResponse")]
        EntityHandler.LINKPayment[] GetCreditorFinalGRN(EntityHandler.LINKPayment objPayment);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/GetGRNMaterial", ReplyAction="urn:Service/GetGRNMaterialResponse")]
        EntityHandler.LINKPayment[] GetGRNMaterial(EntityHandler.LINKPayment objPayment);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/SetMaterialStatus", ReplyAction="urn:Service/SetMaterialStatusResponse")]
        bool SetMaterialStatus(EntityHandler.LINKPayment objPayment);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service/GetCustomerMaster", ReplyAction="urn:Service/GetCustomerMasterResponse")]
        EntityHandler.CustomerMaster[] GetCustomerMaster();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceChannel : BusinessLayer.MRPService.Service, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<BusinessLayer.MRPService.Service>, BusinessLayer.MRPService.Service {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public EntityHandler.LINKPayment[] GetCreditorFinal(EntityHandler.LINKPayment objPayment) {
            return base.Channel.GetCreditorFinal(objPayment);
        }
        
        public EntityHandler.LINKPayment[] GetCreditorFinalSupplierList() {
            return base.Channel.GetCreditorFinalSupplierList();
        }
        
        public EntityHandler.LINKPayment[] GetCreditorFinalSupplier(EntityHandler.LINKPayment objPayment) {
            return base.Channel.GetCreditorFinalSupplier(objPayment);
        }
        
        public EntityHandler.LINKPayment[] GetCreditorFinalSupplierByID(EntityHandler.LINKPayment objPayment) {
            return base.Channel.GetCreditorFinalSupplierByID(objPayment);
        }
        
        public EntityHandler.LINKPayment[] GetCreditorFinalGRN(EntityHandler.LINKPayment objPayment) {
            return base.Channel.GetCreditorFinalGRN(objPayment);
        }
        
        public EntityHandler.LINKPayment[] GetGRNMaterial(EntityHandler.LINKPayment objPayment) {
            return base.Channel.GetGRNMaterial(objPayment);
        }
        
        public bool SetMaterialStatus(EntityHandler.LINKPayment objPayment) {
            return base.Channel.SetMaterialStatus(objPayment);
        }
        
        public EntityHandler.CustomerMaster[] GetCustomerMaster() {
            return base.Channel.GetCustomerMaster();
        }
    }
}
