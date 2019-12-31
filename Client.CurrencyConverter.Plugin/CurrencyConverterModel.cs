using System;
using System.ServiceModel;
using Common.EndpointBehavior;
using Common.Language;
using Server.CurrencyConverter;

namespace Client.CurrencyConverter.Plugin
{
    public class CurrencyConverterModel
    {
        private readonly CurrencyConverterServiceClient _currencyConverterServiceProxy;

        public CurrencyConverterModel(CurrencyConverterServiceClient currencyConverterServiceProxy)
        {
            _currencyConverterServiceProxy = currencyConverterServiceProxy ?? throw new ArgumentNullException(nameof(currencyConverterServiceProxy));
            _currencyConverterServiceProxy.ChannelFactory.Endpoint.Behaviors.Add(new CultureEndpointBehavior());
        }

        public NumberPresentationResult GetNumberPresentation(string value)
        {
            try
            {
                var currencyConverterServiceProxy = _currencyConverterServiceProxy.ChannelFactory.CreateChannel();

                return
                    currencyConverterServiceProxy.GetNumberPresentation(value);
            }
            catch (TimeoutException)
            {
                //should log

                var numberPresentationResult = new NumberPresentationResult()
                {
                    Success = false,
                    ErrorMessage = Language.TimeoutWaiting,
                };

                return
                    numberPresentationResult;
            }
            catch (CommunicationException)
            {
                //should log

                var numberPresentationResult = new NumberPresentationResult()
                {
                    Success = false,
                    ErrorMessage = Language.UnableConnect,
                };

                return
                    numberPresentationResult;
            }
            catch (Exception ex)
            {
                //should log

                var numberPresentationResult = new NumberPresentationResult()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };

                return
                    numberPresentationResult;
            }
        }
    }
}
