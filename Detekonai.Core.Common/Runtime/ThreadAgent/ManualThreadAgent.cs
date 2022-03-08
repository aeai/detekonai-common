using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detekonai.Core.Common.Runtime.ThreadAgent
{
    public class ManualThreadAgent : IThreadAgent
    {
        private readonly ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();
        public void ExecuteOnThread(Action action)
        {
            actions.Enqueue(action);
        }

        public void ProcessOne() 
        {
            if(actions.TryDequeue(out Action act))
            {
                act?.Invoke();
            }
        }

        public void ProcessAll() 
        {
            while(!actions.IsEmpty)
            {
                ProcessOne();
            }
        }
    }
}
