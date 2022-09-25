using MassTransit;
using Playground.ScatterGather.Contracts;

namespace Playground.ScatterGather.FixedIncome.Consumers
{
    public class FixedIncomeConciliationConsumer : IConsumer<CheckCustomerStatusInFixedIncome>
    {
        public async Task Consume(ConsumeContext<CheckCustomerStatusInFixedIncome> context)
        {
            await context.RespondAsync<CheckCustomerStatusResult>(new
            {
                CustomerCode = context.Message.CustomerCode,
                Product = "FixedIncome",
                TimeSpan = DateTime.Now,
                Status = "OK"
            });
        }
    }
}
