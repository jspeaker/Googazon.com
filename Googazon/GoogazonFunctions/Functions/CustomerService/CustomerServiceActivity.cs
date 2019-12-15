using GoogazonFunctions.Models;
using GoogazonFunctions.Needs;
using GoogazonFunctions.Results;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.CustomerService
{
    public abstract class CustomerServiceActivity<TMessage> where TMessage : class, IEventMessage
    {
        private readonly IEventMessage _eventMessage;
        private readonly INeed _need;
        private readonly IResult _result;

        protected CustomerServiceActivity(TMessage eventMessage) : this(eventMessage, new Need(eventMessage), new Result()) { }

        private CustomerServiceActivity(IEventMessage eventMessage, INeed need, IResult result)
        {
            _eventMessage = eventMessage;
            _need = need;
            _result = result;
        }

        public async Task<dynamic> ValueAsync()
        {
            await _need.SendAsync();
            return _result.Item(_eventMessage.UniqueIdentifier());
        }
    }
}