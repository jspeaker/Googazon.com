using Googazon.Library.PrimitiveConcepts;
using System;

namespace Googazon.Library.Exceptions
{
    public class ConfigurationItemNotValidException : Exception
    {
        public ConfigurationItemNotValidException(Text key) : base($"{key} found in configuration but value is not valid.") { }
    }
}