namespace GoogazonFunctions.Texts
{
    public class CustomerServiceRiverConnectionStringKey
    {
        private readonly string _value = "CustomerServiceRiverConnectionString";
        public static implicit operator string(CustomerServiceRiverConnectionStringKey instance) => instance._value;
    }
}