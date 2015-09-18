using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using OrderAutomation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomation.Saga
{
    public class OrderSaga : Saga<OrderSagaData>,
        IAmStartedByMessages<PlaceOrderCommand>,
        IHandleMessages<CreateOrderCompleted>,
        IHandleMessages<CreateOrderDocumentsCompleted>,
        IHandleMessages<BookOrderAppointmentCompleted>,
        IHandleMessages<CreateOrderTasksCompleted>,
        IHandleMessages<SendOrderTransmissionsCompleted>
    {
        static ILog Logger = LogManager.GetLogger("");

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper.ConfigureMapping<PlaceOrderCommand>(m => m.SagaId)
                    .ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<CreateOrderCompleted>(m => m.SagaId)
                    .ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<CreateOrderDocumentsCompleted>(m => m.SagaId)
                    .ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<BookOrderAppointmentCompleted>(m => m.SagaId)
                    .ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<CreateOrderTasksCompleted>(m => m.SagaId)
                    .ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<SendOrderTransmissionsCompleted>(m => m.SagaId)
                    .ToSaga(s => s.SagaId);
        }

        public void Handle(PlaceOrderCommand command)
        {
            Data.SagaId = command.SagaId;
            Data.CaseId = command.CaseId;

            Logger.InfoFormat("PlaceOrder command received for case :{0}. Starting saga  {1}\n", command.CaseId, command.SagaId);

            Bus.Send(new CreateOrderCommand
            {
                SagaId = command.SagaId,
                CaseId = command.CaseId
            });
        }

        public void Handle(CreateOrderCompleted message)
        {
            Logger.Info(string.Format("Saga {0} forwarding message for Creating Order with CaseId {1} \n", message.SagaId, message.CaseId));

            Bus.Send(new CreateOrderDocumentsCommand
            {
                SagaId = message.SagaId,
                CaseId = message.CaseId
            });
        }

        public void Handle(CreateOrderDocumentsCompleted message)
        {
            Logger.Info(string.Format("Saga {0} forwarding message for Booking Appointment with CaseId {1} \n", message.SagaId, message.CaseId));

            Bus.Send(new BookOrderAppointmentCommand
            {
                SagaId = message.SagaId,
                CaseId = message.CaseId
            });
        }

        public void Handle(BookOrderAppointmentCompleted message)
        {
            Logger.Info(string.Format("Saga {0} forwarding message for Creating Tasks with CaseId {1}\n", message.SagaId, message.CaseId));

            Bus.Send(new CreateOrderTasksCommand
            {
                SagaId = message.SagaId,
                CaseId = message.CaseId
            });
        }

        public void Handle(CreateOrderTasksCompleted message)
        {
            Logger.Info(string.Format("Saga {0} forwarding message for Sending Transmissions with CaseId {1} \n", message.SagaId, message.CaseId));

            Bus.Send(new SendOrderTransmissionsCommand
            {
                SagaId = message.SagaId,
                CaseId = message.CaseId
            });
        }

        public void Handle(SendOrderTransmissionsCompleted message)
        {
            Logger.Info(string.Format("Saga {0} completed for CaseId {1} \n", message.SagaId, message.CaseId));

            ReplyToOriginator(new OrderPlacedMessage
            {
                SagaId = message.SagaId,
                CaseId = message.CaseId
            });

            MarkAsComplete();
        }
    }
}
