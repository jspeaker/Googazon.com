namespace Googazon.Library.PrimitiveConcepts
{
    public abstract class Text
    {
        private readonly string _value;
        protected Text(string value) => _value = value;
        public static implicit operator string(Text instance) => instance._value;
    }
}