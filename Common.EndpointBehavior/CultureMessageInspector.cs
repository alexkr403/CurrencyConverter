using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace Common.EndpointBehavior
{
    public class CultureMessageInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        private const string HeaderKey = "culture";

        /// <summary>
        /// Set Culture for service instance using SOAP header
        /// </summary>
        public object AfterReceiveRequest(
            ref Message request, 
            IClientChannel channel, 
            InstanceContext instanceContext
            )
        {
            var headerIndex = request.Headers.FindHeader(
                HeaderKey, 
                string.Empty
                );

            if (headerIndex != -1)
            {
                var culture = request.Headers.GetHeader<string>(headerIndex);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }

            return 
                null;
        }

        public void BeforeSendReply(
            ref Message reply, 
            object correlationState
            )
        {
            //nothing do in the service before send reply to client
        }

        public void AfterReceiveReply(
            ref Message reply,
            object correlationState
            )
        {
            //nothing do in the client after receive reply from service
        }

        /// <summary>
        /// Before send request, set Culture using SOAP Header
        /// </summary>
        public object BeforeSendRequest(
            ref Message request,
            IClientChannel channel
            )
        {
            var header = MessageHeader.CreateHeader(
                HeaderKey, 
                string.Empty, 
                Thread.CurrentThread.CurrentCulture.Name
                );

            request.Headers.Add(header);
            return null;
        }
    }
}
