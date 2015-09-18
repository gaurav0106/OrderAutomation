using NServiceBus;
using NServiceBus.Logging;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomationSaga.BookAppointment
{
    public class BookOrderAppointmentHandler : IHandleMessages<BookOrderAppointmentCommand>
    {
        IBus bus;

        ILog logger;

        public BookOrderAppointmentHandler(IBus bus)
        {
            this.bus = bus;
            logger = LogManager.GetLogger("");
        }

        public void Handle(BookOrderAppointmentCommand message)
        {
            try
            {
                logger.Info(string.Format("Book Order Appointment Completed for CaseId:{0}, publishing the event back to saga: {1} \n", message.CaseId, message.SagaId));

                bus.Reply<BookOrderAppointmentCompleted>(evt =>
                {
                    evt.SagaId = message.SagaId;
                    evt.CaseId = message.CaseId;
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
