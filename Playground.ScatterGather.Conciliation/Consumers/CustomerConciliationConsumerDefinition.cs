using MassTransit;

namespace Playground.ScatterGather.Conciliation.Consumers
{
    public class CustomerConciliationConsumerDefinition : ConsumerDefinition<CustomerConciliationConsumer>
    {
        public CustomerConciliationConsumerDefinition()
        {
            EndpointName = "customer.conciliation";
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CustomerConciliationConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
