﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// 此源代码由 wsdl 自动生成, Version=4.0.30319.1。
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name = "IInspServiceHttpBinding", Namespace = "http://service.webservice.vems2.powerun.com")]
public partial class IInspService : System.Web.Services.Protocols.SoapHttpClientProtocol
{

    private System.Threading.SendOrPostCallback getInspReg2OperationCompleted;

    private System.Threading.SendOrPostCallback getInspRegOperationCompleted;

    /// <remarks/>
    public IInspService()
    {
        this.Url = "http://10.33.139.24/services/inspService";
    }
    public IInspService(string url)
    {
        this.Url = url;
    }

    /// <remarks/>
    public event getInspReg2CompletedEventHandler getInspReg2Completed;

    /// <remarks/>
    public event getInspRegCompletedEventHandler getInspRegCompleted;

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.webservice.vems2.powerun.com", ResponseNamespace = "http://service.webservice.vems2.powerun.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("out", IsNullable = true)]
    public string getInspReg2([System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in0, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in1, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in2, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in3, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in4)
    {
        object[] results = this.Invoke("getInspReg2", new object[] {
                    in0,
                    in1,
                    in2,
                    in3,
                    in4});
        return ((string)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BegingetInspReg2(string in0, string in1, string in2, string in3, string in4, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("getInspReg2", new object[] {
                    in0,
                    in1,
                    in2,
                    in3,
                    in4}, callback, asyncState);
    }

    /// <remarks/>
    public string EndgetInspReg2(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }

    /// <remarks/>
    public void getInspReg2Async(string in0, string in1, string in2, string in3, string in4)
    {
        this.getInspReg2Async(in0, in1, in2, in3, in4, null);
    }

    /// <remarks/>
    public void getInspReg2Async(string in0, string in1, string in2, string in3, string in4, object userState)
    {
        if ((this.getInspReg2OperationCompleted == null))
        {
            this.getInspReg2OperationCompleted = new System.Threading.SendOrPostCallback(this.OngetInspReg2OperationCompleted);
        }
        this.InvokeAsync("getInspReg2", new object[] {
                    in0,
                    in1,
                    in2,
                    in3,
                    in4}, this.getInspReg2OperationCompleted, userState);
    }

    private void OngetInspReg2OperationCompleted(object arg)
    {
        if ((this.getInspReg2Completed != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getInspReg2Completed(this, new getInspReg2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.webservice.vems2.powerun.com", ResponseNamespace = "http://service.webservice.vems2.powerun.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("out", IsNullable = true)]
    public string getInspReg([System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in0, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in1, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] string in2, int in3)
    {
        object[] results = this.Invoke("getInspReg", new object[] {
                    in0,
                    in1,
                    in2,
                    in3});
        return ((string)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BegingetInspReg(string in0, string in1, string in2, int in3, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("getInspReg", new object[] {
                    in0,
                    in1,
                    in2,
                    in3}, callback, asyncState);
    }

    /// <remarks/>
    public string EndgetInspReg(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }

    /// <remarks/>
    public void getInspRegAsync(string in0, string in1, string in2, int in3)
    {
        this.getInspRegAsync(in0, in1, in2, in3, null);
    }

    /// <remarks/>
    public void getInspRegAsync(string in0, string in1, string in2, int in3, object userState)
    {
        if ((this.getInspRegOperationCompleted == null))
        {
            this.getInspRegOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetInspRegOperationCompleted);
        }
        this.InvokeAsync("getInspReg", new object[] {
                    in0,
                    in1,
                    in2,
                    in3}, this.getInspRegOperationCompleted, userState);
    }

    private void OngetInspRegOperationCompleted(object arg)
    {
        if ((this.getInspRegCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getInspRegCompleted(this, new getInspRegCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    public new void CancelAsync(object userState)
    {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void getInspReg2CompletedEventHandler(object sender, getInspReg2CompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getInspReg2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal getInspReg2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
    {
        this.results = results;
    }

    /// <remarks/>
    public string Result
    {
        get
        {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void getInspRegCompletedEventHandler(object sender, getInspRegCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getInspRegCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal getInspRegCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
    {
        this.results = results;
    }

    /// <remarks/>
    public string Result
    {
        get
        {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}