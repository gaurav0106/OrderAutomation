using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomation.Contracts
{
    public class CreateOrderCommand : ICommand
    {
        public Guid SagaId { get; set; }

        public int CaseId { get; set; }
    }
}
