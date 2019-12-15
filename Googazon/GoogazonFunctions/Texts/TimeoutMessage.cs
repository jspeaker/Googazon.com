namespace GoogazonFunctions.Texts
{
    public class TimeoutMessage
    {
        private readonly string _value = "The result did not become available within the timeout period.";
        public static implicit operator string(TimeoutMessage instance) => instance._value;
    }
}