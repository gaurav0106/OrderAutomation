using NServiceBus;
using NServiceBus.Logging;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomationSaga.SendTransmissions
{
    public class SendOrderTransmissionsHandler : IHandleMessages<SendOrderTransmissionsCommand>
    {
        IBus bus;

        ILog logger;

        public SendOrderTransmissionsHandler(IBus bus)
        {
            this.bus = bus;
            logger = LogManager.GetLogger("");
        }

        public void Handle(SendOrderTransmissionsCommand message)
        {
            logger.Info(string.Format("Send Order Transmissions Completed for CaseId:{0}, publishing the event back to saga: {1} \n", message.CaseId, message.SagaId));

            bus.Reply<SendOrderTransmissionsCompleted>(evt =>
            {
                evt.SagaId = message.SagaId;
                evt.CaseId = message.CaseId;
            });
        }

    }
}
