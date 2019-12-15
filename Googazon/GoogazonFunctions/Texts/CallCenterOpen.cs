namespace GoogazonFunctions.Texts
{
    public class CallCenterOpenNeed
    {
        private readonly string _value = "callcenteropen";
        public static implicit operator string(CallCenterOpenNeed instance) => instance._value;
    }
}