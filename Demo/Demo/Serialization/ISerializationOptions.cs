using System;
using Demo.Dependency;

namespace Demo.Serialization
{
    /// <summary>
    /// Represents the options for serialization
    /// </summary>
    public interface ISerializationOptions : ISingletonDependency
    {
        /// <summary>
        /// Gets whether a property on the given type should be serialized
        /// </summary>
        bool ShouldSerializeProperty(Type type, string propertyName);

        /// <summary>
        /// Gets the flag used for serialization
        /// </summary>
        SerializationOptionsFlags Flags { get; }
    }
}