using NServiceBus;
using NServiceBus.Logging;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomation.Client
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlacedMessage>
    {
        private static readonly ILog Logger = LogManager.GetLogger("");

        public void Handle(OrderPlacedMessage message)
        {
            Logger.InfoFormat("Order Created for CaseId {0}! SagaId: {1} is completed", message.CaseId, message.SagaId);
        }
    }
}
