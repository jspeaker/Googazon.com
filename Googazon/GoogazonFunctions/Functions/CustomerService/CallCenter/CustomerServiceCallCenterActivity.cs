namespace GoogazonFunctions.Functions.CustomerService.CallCenter
{
    public class CustomerServiceCallCenterActivity : CustomerServiceActivity<CustomerServiceMessage>
    {
        public CustomerServiceCallCenterActivity(string customerId) : this(new CustomerServiceMessage(customerId)) { }

        public CustomerServiceCallCenterActivity(CustomerServiceMessage eventMessage) : base(eventMessage) { }
    }
}