using GoogazonActivities.Texts;

namespace GoogazonActivities.CustomerService.CallCenter
{
    public class CustomerServiceCallCenterActivity : CustomerServiceActivity<CustomerServiceMessage>
    {
        public CustomerServiceCallCenterActivity(string customerId) : this(new CustomerServiceMessage(customerId, new ContactNeed())) { }

        public CustomerServiceCallCenterActivity(CustomerServiceMessage eventMessage) : base(eventMessage) { }
    }
}