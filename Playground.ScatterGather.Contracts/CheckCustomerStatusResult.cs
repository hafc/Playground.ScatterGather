namespace Playground.ScatterGather.Contracts
{
    public interface CheckCustomerStatusResult
    {
        public int CustomerCode { get; set; }
        public string Product { get; set; }
        public DateTime TimeSpan { get; set; }
        public string Status { get; set; }
    }
}
