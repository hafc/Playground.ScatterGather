using MassTransit;
using Playground.ScatterGather.Conciliation.Consumers;
using Playground.ScatterGather.Contracts;
using Playground.ScatterGather.FixedIncome.Consumers;
using Playground.ScatterGather.Funds.Consumers;
using Playground.ScatterGather.PensionFunds.Consumers;

namespace Playground.ScatterGather.API
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((ctx, busConfigurator) =>
                {
                    busConfigurator.Host("amqp://guest:guest@localhost:5672");
                    busConfigurator.ConfigureEndpoints(ctx);
                });

                bus.AddConsumer(typeof(CustomerConciliationConsumer), typeof(CustomerConciliationConsumerDefinition));
                bus.AddConsumer(typeof(FixedIncomeConciliationConsumer), typeof(FixedIncomeConciliationConsumerDefinition));
                bus.AddConsumer(typeof(FundsConciliationConsumer), typeof(FundsConciliationConsumerDefinition));
                bus.AddConsumer(typeof(PensionFundsConciliationConsumer), typeof(PensionFundsConciliationConsumerDefinition));

                bus.AddRequestClient<CheckCustomerStatusInPensionFunds>(RequestTimeout.After(s:20));
                bus.AddRequestClient<CheckCustomerStatusInFunds>(RequestTimeout.After(s: 1));
                bus.AddRequestClient<CheckCustomerStatusInFixedIncome>(RequestTimeout.After(s: 20));
            });
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
    
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
