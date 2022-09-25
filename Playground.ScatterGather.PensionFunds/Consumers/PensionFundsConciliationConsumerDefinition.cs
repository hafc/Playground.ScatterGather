using MassTransit;

namespace Playground.ScatterGather.PensionFunds.Consumers
{
    public class PensionFundsConciliationConsumerDefinition : ConsumerDefinition<PensionFundsConciliationConsumer>
    {
        public PensionFundsConciliationConsumerDefinition()
        {
            EndpointName = "conciliation.pensionfunds";
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PensionFundsConciliationConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
