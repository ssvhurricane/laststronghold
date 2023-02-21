using System.Collections;

namespace Config
{
    public interface IRegistry
    {
        // TODO:
    }

    public interface IRegistryList : IEnumerable
    {
        // TODO:
    }

    public interface IRegistryData
    {
        string Id { get; }
    }
}