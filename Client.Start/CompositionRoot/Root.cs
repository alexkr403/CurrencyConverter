using Client.CurrencyConverter.Plugin;
using Ninject;

namespace Client.Start.CompositionRoot
{
    public class Workstation : StandardKernel
    {
        public void Init()
        {
            Bind<CurrencyConverterViewModel>()
                .To<CurrencyConverterViewModel>()
                .InSingletonScope()
                ;

            Bind<CurrencyConverterModel>()
                .To<CurrencyConverterModel>()
                .InSingletonScope()
                ;

            Bind<ICurrencyConverterService, CurrencyConverterServiceClient>()
                .To<CurrencyConverterServiceClient>()
                .InSingletonScope()
                ;
        }
    }
}
