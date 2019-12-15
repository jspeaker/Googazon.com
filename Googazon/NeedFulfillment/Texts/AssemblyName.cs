using Googazon.Library.PrimitiveConcepts;
using System.Reflection;

namespace NeedFulfillment.Texts
{
    public class AssemblyName : Text
    {
        public AssemblyName() : base(Assembly.GetExecutingAssembly().FullName) { }
    }
}