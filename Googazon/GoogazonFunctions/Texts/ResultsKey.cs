namespace GoogazonFunctions.Texts
{
    public class ResultsKey
    {
        private const string Value = "Results";
        public static implicit operator string(ResultsKey instance) => Value;
    }
}