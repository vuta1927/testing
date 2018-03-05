using Demo.Dependency;
using Microsoft.Extensions.Primitives;

namespace Demo.Caching
{
    public interface ISignal : ISingletonDependency
    {
        IChangeToken GetToken(string key);

        void SignalToken(string key);
    }
}
