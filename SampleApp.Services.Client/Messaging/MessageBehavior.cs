using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace SampleApp.Services.Client
{
    public class MessageBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        #region BehaviorExtensionElement

        public override Type BehaviorType
        {
            get { return typeof(MessageBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new MessageBehavior();
        }

        #endregion

        #region IEndpointBehavior

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new MessageHeaderInspector());
        }

        #endregion
    }
}
