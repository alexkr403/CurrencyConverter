using System;
using System.ServiceModel;
using Common.Language;

namespace Server.CurrencyConverter.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var serviceHost = new ServiceHost(typeof(CurrencyConverterService)))
                {
                    serviceHost.Open();

                    Console.WriteLine(Language.ServiceReady);
                    Console.WriteLine(Language.PressEnter);
                    Console.ReadLine();

                    serviceHost.Close(new TimeSpan(100));
                }
            }
            catch (TimeoutException timeProblem)
            {
                Console.WriteLine(timeProblem.Message);
                Console.ReadLine();
            }
            catch (CommunicationException commProblem)
            {
                Console.WriteLine(commProblem.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
