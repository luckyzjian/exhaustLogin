﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HnWebClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ExternalAccessSoap")]
    public interface ExternalAccessSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/wsInvokeInterface", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string wsInvokeInterface(string KEY, string JKID, string XML);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ExternalAccessSoapChannel : HnWebClient.ServiceReference1.ExternalAccessSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExternalAccessSoapClient : System.ServiceModel.ClientBase<HnWebClient.ServiceReference1.ExternalAccessSoap>, HnWebClient.ServiceReference1.ExternalAccessSoap {
        
        public ExternalAccessSoapClient() {
        }
        
        public ExternalAccessSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExternalAccessSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExternalAccessSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExternalAccessSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string wsInvokeInterface(string KEY, string JKID, string XML) {
            return base.Channel.wsInvokeInterface(KEY, JKID, XML);
        }
    }
}