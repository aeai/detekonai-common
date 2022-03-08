using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detekonai.Core.Common.Runtime.ThreadAgent
{
    public interface IThreadAgent
    {
        void ExecuteOnThread(Action action);
    }
}
