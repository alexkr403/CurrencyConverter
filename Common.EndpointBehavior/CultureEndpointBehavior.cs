using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Common.EndpointBehavior
{
    public class CultureEndpointBehavior : IEndpointBehavior
    {
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
            var inspector = new CultureMessageInspector();
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(
            ServiceEndpoint endpoint, 
            EndpointDispatcher endpointDispatcher
            )
        {
            var inspector = new CultureMessageInspector();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
