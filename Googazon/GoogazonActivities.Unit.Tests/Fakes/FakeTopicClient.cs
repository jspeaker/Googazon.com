using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakeTopicClient : ITopicClient
    {
        public int CallCount;

        public Task SendAsync(Message message)
        {
            CallCount++;
            return Task.CompletedTask;
        }

        #region not implemented
        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }

        public void RegisterPlugin(ServiceBusPlugin serviceBusPlugin)
        {
            throw new NotImplementedException();
        }

        public void UnregisterPlugin(string serviceBusPluginName)
        {
            throw new NotImplementedException();
        }

        public string ClientId { get; }
        public bool IsClosedOrClosing { get; }
        public string Path { get; }
        public TimeSpan OperationTimeout { get; set; }
        public ServiceBusConnection ServiceBusConnection { get; }
        public bool OwnsConnection { get; }
        public IList<ServiceBusPlugin> RegisteredPlugins { get; }

        public Task SendAsync(IList<Message> messageList)
        {
            throw new NotImplementedException();
        }

        public Task<long> ScheduleMessageAsync(Message message, DateTimeOffset scheduleEnqueueTimeUtc)
        {
            throw new NotImplementedException();
        }

        public Task CancelScheduledMessageAsync(long sequenceNumber)
        {
            throw new NotImplementedException();
        }

        public string TopicName { get; }
        #endregion
    }
}