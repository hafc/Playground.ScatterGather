using MassTransit;
using Playground.ScatterGather.Contracts;

namespace Playground.ScatterGather.Funds.Consumers
{
    public class FundsConciliationConsumer : IConsumer<CheckCustomerStatusInFunds>
    {
        public async Task Consume(ConsumeContext<CheckCustomerStatusInFunds> context)
        {
            await context.RespondAsync<CheckCustomerStatusResult>(new
            {
                CustomerCode = context.Message.CustomerCode,
                Product = "Funds",
                TimeSpan = DateTime.Now,
                Status = "Error in position"
            });
        }
    }
}
