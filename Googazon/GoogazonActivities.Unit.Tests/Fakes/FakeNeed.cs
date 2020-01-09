using System;
using System.Threading.Tasks;
using GoogazonActivities.Needs;
using Microsoft.Azure.EventHubs;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakeNeed : INeed
    {
        public int CallCountSpy;

        public Task<EventData> SendAsync()
        {
            CallCountSpy++;
            return Task.FromResult(new EventData(new ArraySegment<byte>()));
        }
    }
}