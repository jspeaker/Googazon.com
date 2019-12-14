namespace GoogazonFunctions.Texts
{
    public class CallCenterOpenNeed
    {
        private const string Value = "callcenteropen";
        public static implicit operator string(CallCenterOpenNeed instance) => Value;
    }
}