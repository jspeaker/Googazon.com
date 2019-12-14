using GoogazonFunctions.Texts;

namespace GoogazonFunctions.Functions.CustomerService.CallCenter
{
    public class CustomerServiceCallCenterActivity : CustomerServiceActivity<CustomerServiceMessage>
    {
        public CustomerServiceCallCenterActivity(string customerId) : this(new CustomerServiceMessage(customerId, new CallCenterOpenNeed())) { }

        public CustomerServiceCallCenterActivity(CustomerServiceMessage eventMessage) : base(eventMessage) { }
    }
}