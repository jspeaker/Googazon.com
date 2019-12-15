namespace GoogazonFunctions.Texts
{
    public class RapidsKey
    {
        private readonly string _value = "rapids";
        public static implicit operator string(RapidsKey instance) => instance._value;
    }
}