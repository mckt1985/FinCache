using System;
using System.Collections.Generic;
using System.Text;

namespace FinCache.API.Models.Amqp
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