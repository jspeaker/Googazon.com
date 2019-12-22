using System.Reflection;
using Googazon.Library.PrimitiveConcepts;

namespace NeedFulfillmentActivities.Texts
{
    public class AssemblyName : Text
    {
        public AssemblyName() : base(Assembly.GetExecutingAssembly().FullName) { }
    }
}