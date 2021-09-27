using System.Collections;

namespace Detekonai.Networking.Runtime.AsyncEvent
{
    public class UniversalAwaitable<T> : IEnumerator
    {
        private readonly IUniversalAwaiter<T> awaiter;
 
        public UniversalAwaitable(IUniversalAwaiter<T> awaiter)
		{
            this.awaiter = awaiter;
        }

        public IUniversalAwaiter<T> GetAwaiter()
        {
            return awaiter;
        }
        
        public object Current 
        {
            get 
            {
                return null;
            }
        }

        public bool MoveNext()
        {
            if(!awaiter.IsInitialized)
            {
                awaiter.OnCompleted(null);
            }

            return !awaiter.IsCompleted;
        }

        public T GetResult()
        {
            return awaiter.GetResult();
        }

        public void CancelRequest() 
        {
            awaiter.Cancel();
        }

        public void Reset()
        {
        }
    }
}
