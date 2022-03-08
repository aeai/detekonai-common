using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detekonai.Core.Common.Runtime.ThreadAgent
{
    public class ImmediateThreadAgent : IThreadAgent
    {
        public void ExecuteOnThread(Action action)
        {
            action?.Invoke();
        }
    }
}
