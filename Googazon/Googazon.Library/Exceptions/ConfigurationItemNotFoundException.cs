using Googazon.Library.PrimitiveConcepts;
using System;

namespace Googazon.Library.Exceptions
{
    public class ConfigurationItemNotFoundException : Exception
    {
        public ConfigurationItemNotFoundException(Text key) : base($"{key} not found in configuration.") { }
    }
}