using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Common.EndpointBehavior
{
    public class CultureEndpointBehavior : IEndpointBehavior
    {
        private readonly CultureMessageInspector _cultureMessageInspector;

        public CultureEndpointBehavior(CultureMessageInspector cultureMessageInspector)
        {
            _cultureMessageInspector = cultureMessageInspector ?? throw new ArgumentNullException(nameof(cultureMessageInspector));
        }

        public void AddBindingParameters(
            ServiceEndpoint endpoint, 
            BindingParameterCollection bindingParameters
            )
        {
        }

        public void ApplyClientBehavior(
            ServiceEndpoint endpoint,
            ClientRuntime clientRuntime
            )
        {
            clientRuntime.MessageInspectors.Add(_cultureMessageInspector);
        }

        public void ApplyDispatchBehavior(
            ServiceEndpoint endpoint, 
            EndpointDispatcher endpointDispatcher
            )
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(_cultureMessageInspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
