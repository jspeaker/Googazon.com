namespace GoogazonFunctions.Texts
{
    public class RapidsKey
    {
        private const string Value = "rapids";
        public static implicit operator string(RapidsKey instance) => Value;
    }
}