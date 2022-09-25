using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Playground.ScatterGather.Contracts;

namespace Playground.ScatterGather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConciliationController : ControllerBase
    {
        IPublishEndpoint _publisher;
        IRequestClient<CheckCustomerStatusInPensionFunds> _pensionFunds;
        IRequestClient<CheckCustomerStatusInFunds> _funds;
        IRequestClient<CheckCustomerStatusInFixedIncome> _fixedIncome;

        public ConciliationController(
            IPublishEndpoint publisher, 
            IRequestClient<CheckCustomerStatusInPensionFunds> pensionFunds, 
            IRequestClient<CheckCustomerStatusInFunds> funds, 
            IRequestClient<CheckCustomerStatusInFixedIncome> fixedIncome)
        {
            _publisher = publisher;
            _pensionFunds = pensionFunds;
            _funds = funds;
            _fixedIncome = fixedIncome;
        }

        [HttpPost("customer/{customerCode}")]
        public async Task<IActionResult> StartConciliation(int customerCode)
        {
            var pensionFundsResults = _pensionFunds.GetResponse<CheckCustomerStatusResult>(new { CustomerCode = customerCode });
            var funds = _funds.GetResponse<CheckCustomerStatusResult>(new { CustomerCode = customerCode });
            var FixedIncome = _fixedIncome.GetResponse<CheckCustomerStatusResult>(new { CustomerCode = customerCode });

            await Task.WhenAll(pensionFundsResults, funds, FixedIncome);

            var customerStatus = $"Customer {customerCode} in Funds:{funds.Result.Message.Status} - PensionFunds: {pensionFundsResults.Result.Message.Status} - FixedIncome:{FixedIncome.Result.Message.Status}";

            return Ok(customerStatus);
        }

        [HttpPost("batch/customer/{customerCode}")]
        public async Task<IActionResult> ProcessConciliation(int customerCode)
        {
            await _publisher.Publish<ConciliateCustomer>(new
            {
                CustomerCode = customerCode
            });

            return Ok();
        }
    }
}
