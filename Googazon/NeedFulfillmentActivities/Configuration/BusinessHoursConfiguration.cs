using Googazon.Library.Exceptions;
using Googazon.Library.PrimitiveConcepts;
using NeedFulfillmentActivities.Models.BusinessHours;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeedFulfillmentActivities.Configuration
{
    public interface IBusinessHoursConfiguration {
        List<Hours> Hours(Text key);
    }

    public class BusinessHoursConfiguration : IBusinessHoursConfiguration
    {
        private string BusinessHoursConfig(Text key)
        {
            string businessHours = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(businessHours)) throw new ConfigurationItemNotFoundException(key);

            return businessHours;
        }

        public List<Hours> Hours(Text key)
        {
            List<Hours> hours = JsonConvert.DeserializeObject<List<Hours>>(BusinessHoursConfig(key));
            if (!hours.Any()) throw new ConfigurationItemNotValidException(key);

            return hours;
        }
    }
}