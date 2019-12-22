using Googazon.Library.PrimitiveConcepts;

namespace GoogazonActivities.Texts
{
    public class TimeoutMessage : Text
    {
        public TimeoutMessage() : base("The result did not become available within the timeout period.") { }
    }
}