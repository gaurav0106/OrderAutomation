using NServiceBus;
using NServiceBus.Logging;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomationSaga.CreateOrder
{
    public class CreateOrderHandler : IHandleMessages<CreateOrderCommand>
    {
        IBus bus;

        ILog logger;

        public CreateOrderHandler(IBus bus)
        {
            this.bus = bus;
            logger = LogManager.GetLogger("");
        }

        public void Handle(CreateOrderCommand message)
        {
            try
            {
                logger.Info(string.Format("Create Order Command received for CaseId:{0}, processing....\n", message.CaseId));

                //throw new Exception("Trying exception Handling");
                //System.Threading.Thread.Sleep(10000);

                logger.Info(string.Format("Create Order Completed for CaseId:{0}, replying back to saga: {1} \n", message.CaseId, message.SagaId));

                bus.Reply<CreateOrderCompleted>(evt =>
                {
                    evt.SagaId = message.SagaId;
                    evt.CaseId = message.CaseId;
                });
            }
            catch (Exception ex)
            {
                logger.Error("error occured" + ex.ToString());
                throw;
            }
        }
    }
}
