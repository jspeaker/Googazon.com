using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakeQueueClient : IQueueClient
    {
        public int CallCount;

        public Task SendAsync(Message message)
        {
            CallCount++;
            return Task.CompletedTask;
        }

        #region not implemented
        // ReSharper disable UnassignedGetOnlyAutoProperty

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
        public void RegisterMessageHandler(Func<Message, CancellationToken, Task> handler, Func<ExceptionReceivedEventArgs, Task> exceptionReceivedHandler)
        {
            throw new NotImplementedException();
        }

        public void RegisterMessageHandler(Func<Message, CancellationToken, Task> handler, MessageHandlerOptions messageHandlerOptions)
        {
            throw new NotImplementedException();
        }

        public Task CompleteAsync(string lockToken)
        {
            throw new NotImplementedException();
        }

        public Task AbandonAsync(string lockToken, IDictionary<string, object> propertiesToModify = null)
        {
            throw new NotImplementedException();
        }

        public Task DeadLetterAsync(string lockToken, IDictionary<string, object> propertiesToModify = null)
        {
            throw new NotImplementedException();
        }

        public Task DeadLetterAsync(string lockToken, string deadLetterReason, string deadLetterErrorDescription = null)
        {
            throw new NotImplementedException();
        }

        public int PrefetchCount { get; set; }
        public ReceiveMode ReceiveMode { get; }
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

        public void RegisterSessionHandler(Func<IMessageSession, Message, CancellationToken, Task> handler, Func<ExceptionReceivedEventArgs, Task> exceptionReceivedHandler)
        {
            throw new NotImplementedException();
        }

        public void RegisterSessionHandler(Func<IMessageSession, Message, CancellationToken, Task> handler, SessionHandlerOptions sessionHandlerOptions)
        {
            throw new NotImplementedException();
        }

        public string QueueName { get; }
        // ReSharper restore UnassignedGetOnlyAutoProperty

        #endregion
    }
}