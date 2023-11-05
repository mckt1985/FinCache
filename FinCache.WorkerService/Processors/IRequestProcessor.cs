using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Processors
{
    public interface IRequestProcessor
    {
        void Start();
        void Stop();
    }
}
