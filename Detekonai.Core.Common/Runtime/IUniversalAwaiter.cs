using System.Runtime.CompilerServices;

namespace Detekonai.Networking.Runtime.AsyncEvent
{
    public interface IUniversalAwaiter<T> : INotifyCompletion
    {
        public bool IsCompleted { get; }
        public bool IsInitialized { get; }
        public void Cancel();
        public T GetResult();
    }
}
