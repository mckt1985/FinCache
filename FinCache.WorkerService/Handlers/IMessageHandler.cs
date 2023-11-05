using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Handlers
{
    public interface IMessageHandler
    {
        void Start();
        void Stop();
    }
}
