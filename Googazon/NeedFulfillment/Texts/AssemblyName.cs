using System.Reflection;

namespace NeedFulfillment.Texts
{
    public class AssemblyName
    {
        private readonly string _fullName = Assembly.GetExecutingAssembly().FullName;
        public static implicit operator string(AssemblyName instance) => instance._fullName;
    }
}