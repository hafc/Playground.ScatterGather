using MassTransit;
using Playground.ScatterGather.Contracts;

namespace Playground.ScatterGather.Conciliation.Consumers
{
    public class CustomerConciliationConsumer : IConsumer<ConciliateCustomer>
    {
        IRequestClient<CheckCustomerStatusInPensionFunds> _pensionFunds;
        IRequestClient<CheckCustomerStatusInFunds> _funds;
        IRequestClient<CheckCustomerStatusInFixedIncome> _fixedIncome;

        public CustomerConciliationConsumer(
            IRequestClient<CheckCustomerStatusInPensionFunds> pensionFunds, 
            IRequestClient<CheckCustomerStatusInFunds> funds, 
            IRequestClient<CheckCustomerStatusInFixedIncome> fixedIncome)
        {
            _pensionFunds = pensionFunds;
            _funds = funds;
            _fixedIncome = fixedIncome;
        }

        public async Task Consume(ConsumeContext<ConciliateCustomer> context)
        {
            var pensionFundsResults = _pensionFunds.GetResponse<CheckCustomerStatusResult>(new { CustomerCode = context.Message.CustomerCode });

            var FixedIncome = _fixedIncome.GetResponse<CheckCustomerStatusResult>(new { CustomerCode = context.Message.CustomerCode });

            var funds = _funds.GetResponse<CheckCustomerStatusResult>(new { CustomerCode = context.Message.CustomerCode });

            await Task.WhenAll(pensionFundsResults, FixedIncome, funds);

            Console.WriteLine($"Funds: {funds.Result.Message.Status}");
            Console.WriteLine($"PensionFunds: {pensionFundsResults.Result.Message.Status}");
            Console.WriteLine($"FixedIncome: {FixedIncome.Result.Message.Status}");
        }
    }
}
