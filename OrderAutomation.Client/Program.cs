using NServiceBus;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomation.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.UseSerialization<XmlSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                SendOrder(bus);
            }
        }

        static void SendOrder(IBus bus)
        {
            Console.Title = "Client";
            Console.Clear();
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            int caseId = 101;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }

                PlaceOrderCommand placeOrder = new PlaceOrderCommand
                {
                    SagaId = Guid.NewGuid(),
                    CaseId = caseId++
                };

                bus.Send(placeOrder);

                Console.WriteLine("Sent a new PlaceOrder message for CaseId:{0} with UniqueId: {1}", placeOrder.CaseId, placeOrder.SagaId.ToString("N"));
            }

        }
    }
}
