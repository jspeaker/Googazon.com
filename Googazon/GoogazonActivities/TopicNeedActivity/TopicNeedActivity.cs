using GoogazonActivities.Models;
using GoogazonActivities.Needs;
using GoogazonActivities.Results;
using System.Threading.Tasks;

namespace GoogazonActivities.TopicNeedActivity
{
    public interface INeedActivity
    {
        Task<dynamic> ValueAsync();
        Task ExpressNeed();
    }

    public class TopicNeedActivity : INeedActivity
    {
        private readonly IEventMessage _eventMessage;
        private readonly INeed _need;
        private readonly IResult _result;
        public TopicNeedActivity(string connectionId, string resourceIdentifier, string topic, string need) : 
            this(new TopicNeedMessage(connectionId, resourceIdentifier, topic, need)) { }

        private TopicNeedActivity(IEventMessage eventMessage) : this(eventMessage, new Need(eventMessage), new Result()) { }

        private TopicNeedActivity(IEventMessage eventMessage, INeed need, IResult result)
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

        public async Task ExpressNeed() => await _need.SendAsync();
    }
}