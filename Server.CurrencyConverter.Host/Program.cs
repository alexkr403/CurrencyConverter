using System;
using System.ServiceModel;

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

                    Console.WriteLine("The service is ready");
                    Console.WriteLine("Press <ENTER> to terminate service");
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
