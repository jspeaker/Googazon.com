namespace GoogazonFunctions.Texts
{
    public class ResultsKey
    {
        private readonly string _value = "Results";
        public static implicit operator string(ResultsKey instance) => instance._value;
    }
}