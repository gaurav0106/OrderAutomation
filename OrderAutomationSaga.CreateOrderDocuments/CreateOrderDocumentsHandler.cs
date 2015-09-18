using NServiceBus;
using NServiceBus.Logging;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomationSaga.CreateOrderDocuments
{
    public class CreateOrderDocumentsHandler : IHandleMessages<CreateOrderDocumentsCommand>
    {
        IBus bus;

        ILog logger;

        public CreateOrderDocumentsHandler(IBus bus)
        {
            this.bus = bus;
            logger = LogManager.GetLogger("");
        }

        public void Handle(CreateOrderDocumentsCommand message)
        {
            logger.Info(string.Format("Create Order Documents Completed for CaseId:{0}, publishing the event back to saga: {1} \n", message.CaseId, message.SagaId));

            bus.Reply<CreateOrderDocumentsCompleted>(evt =>
            {
                evt.SagaId = message.SagaId;
                evt.CaseId = message.CaseId;
            });
        }

    }
}
