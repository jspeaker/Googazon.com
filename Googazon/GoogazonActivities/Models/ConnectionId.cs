using Googazon.Library.PrimitiveConcepts;
using GoogazonActivities.Texts;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace GoogazonActivities.Models
{
    public class ConnectionId : Text
    {
        public ConnectionId(IHeaderDictionary headerDictionary) : 
            base(headerDictionary.FirstOrDefault(item => item.Key.Equals(new ConnectionIdHeaderName(), StringComparison.InvariantCultureIgnoreCase)).Value.FirstOrDefault() 
                 ?? string.Empty) { }
    }
}