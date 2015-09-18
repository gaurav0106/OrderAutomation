using NServiceBus;
using NServiceBus.Logging;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomationSaga.CreateTasks
{
    public class CreateOrderTasksHandler : IHandleMessages<CreateOrderTasksCommand>
    {
        IBus bus;

        ILog logger;

        public CreateOrderTasksHandler(IBus bus)
        {
            this.bus = bus;
            logger = LogManager.GetLogger("");
        }

        public void Handle(CreateOrderTasksCommand message)
        {
            logger.Info(string.Format("Create Order Tasks Completed for CaseId:{0}, publishing the event back to saga: {1} \n", message.CaseId, message.SagaId));

            bus.Reply<CreateOrderTasksCompleted>(evt =>
            {
                evt.SagaId = message.SagaId;
                evt.CaseId = message.CaseId;
            });
        }

    }
}
