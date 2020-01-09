using System.Dynamic;
using GoogazonActivities.Results;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakeResult : IResult
    {
        private readonly dynamic _result;

        public FakeResult(dynamic result) => _result = result;

        public ExpandoObject Item(string id) => _result;
    }
}