using NServiceBus;
using NServiceBus.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomation.Saga
{
    class Program
    {
        static void Main(string[] args)
        {
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.UseSerialization<XmlSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<RavenDBPersistence>();

            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                Console.Title = "Saga";
                Console.Clear();
                Console.WriteLine("Waiting for messages, Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
