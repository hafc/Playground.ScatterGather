using MassTransit;

namespace Playground.ScatterGather.FixedIncome.Consumers
{
    public class FixedIncomeConciliationConsumerDefinition : ConsumerDefinition<FixedIncomeConciliationConsumer>
    {
        public FixedIncomeConciliationConsumerDefinition()
        {
            EndpointName = "conciliation.fixedIncome";
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<FixedIncomeConciliationConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
