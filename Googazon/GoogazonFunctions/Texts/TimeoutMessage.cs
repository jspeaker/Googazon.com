namespace GoogazonFunctions.Texts
{
    public class TimeoutMessage
    {
        private const string Value = "The result did not become available within the timeout period.";
        public static implicit operator string(TimeoutMessage instance) => Value;
    }
}