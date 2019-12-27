using Googazon.Library.PrimitiveConcepts;
using System;

namespace Googazon.Library.Exceptions
{
    public class ConfigurationItemParsingException : Exception
    {
        public ConfigurationItemParsingException(Text key) : base($"{key} found in configuration but value cannot be parsed.") { }
    }
}