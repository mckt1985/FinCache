using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Models.Amqp
{
    public class MessageTypeAttribute : Attribute
    {
        public string Name { get; private set; }

        public MessageTypeAttribute(string name)
        {
            Name = name;
        }        
    }
}
