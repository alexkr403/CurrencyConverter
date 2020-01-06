using System.ServiceModel.Description;
using Client.CurrencyConverter.Plugin;
using Common.EndpointBehavior;
using Ninject;

namespace Client.Start.CompositionRoot
{
    public class Workstation : StandardKernel
    {
        public void Init()
        {
            Bind<CurrencyConverterViewModel>()
                .To<CurrencyConverterViewModel>()
                //not singleton
                ;

            Bind<CurrencyConverterModel>()
                .To<CurrencyConverterModel>()
                .InSingletonScope()
                ;

            Bind<ICurrencyConverterService, CurrencyConverterServiceClient>()
                .To<CurrencyConverterServiceClient>()
                .InSingletonScope()
                ;

            Bind<IEndpointBehavior>()
                .To<CultureEndpointBehavior>()
                .InSingletonScope()
                ;

            Bind<CultureMessageInspector>()
                .ToSelf()
                .InSingletonScope()
                ;
        }
    }
}
