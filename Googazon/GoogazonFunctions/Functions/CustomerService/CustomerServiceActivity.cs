using GoogazonFunctions.Functions.Results;
using GoogazonFunctions.Models;
using GoogazonFunctions.Needs;
using System.Threading.Tasks;
using GoogazonFunctions.Results;

namespace GoogazonFunctions.Functions.CustomerService
{
    public abstract class CustomerServiceActivity<TMessage> where TMessage : class, IEventMessage
    {
        private readonly IEventMessage _eventMessage;
        private readonly INeed _need;

        protected CustomerServiceActivity(TMessage eventMessage) : this(eventMessage, new Need(eventMessage)) { }

        private CustomerServiceActivity(IEventMessage eventMessage, INeed need)
        {
            _eventMessage = eventMessage;
            _need = need;
        }

        public async Task<dynamic> ValueAsync()
        {
            await _need.SendAsync();
            return new Result().Item(_eventMessage.UniqueIdentifier());
        }
    }
}