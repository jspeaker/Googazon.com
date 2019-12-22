using GoogazonActivities.Texts;

namespace GoogazonActivities.CustomerService.Contact
{
    public class CustomerServiceContactActivity : CustomerServiceActivity<CustomerServiceMessage>
    {
        public CustomerServiceContactActivity(string customerId) : this(new CustomerServiceMessage(customerId, new ContactNeed())) { }

        public CustomerServiceContactActivity(CustomerServiceMessage eventMessage) : base(eventMessage) { }
    }
}