using MassTransit;
using Playground.ScatterGather.Contracts;

namespace Playground.ScatterGather.PensionFunds.Consumers
{
    public class PensionFundsConciliationConsumer : IConsumer<CheckCustomerStatusInPensionFunds>
    {
        public async Task Consume(ConsumeContext<CheckCustomerStatusInPensionFunds> context)
        {
            await context.RespondAsync<CheckCustomerStatusResult>(new
            {
                CustomerCode = context.Message.CustomerCode,
                Product = "PensionFunds",
                TimeSpan = DateTime.Now,
                Status = "OK"
            });
        }
    }
}
