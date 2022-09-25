using MassTransit;

namespace Playground.ScatterGather.Funds.Consumers
{
    public class FundsConciliationConsumerDefinition : ConsumerDefinition<FundsConciliationConsumer>
    {
        public FundsConciliationConsumerDefinition()
        {
            EndpointName = "conciliation.funds";
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<FundsConciliationConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
