using System;

namespace Kosha.CustomerManager.Web.Persistence.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SeedAttribute(long version) : Attribute
{
    public long Version { get; } = version;
}