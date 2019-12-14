using System.Reflection;

namespace NeedFulfillment.Texts
{
    internal class AssemblyName
    {
        private static readonly string FullName = Assembly.GetExecutingAssembly().FullName;
        public static implicit operator string(AssemblyName instance) => FullName;
    }
}