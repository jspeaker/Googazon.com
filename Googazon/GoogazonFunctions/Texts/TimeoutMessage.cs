using Googazon.Library.PrimitiveConcepts;

namespace GoogazonFunctions.Texts
{
    public class TimeoutMessage : Text
    {
        public TimeoutMessage() : base("The result did not become available within the timeout period.") { }
    }
}