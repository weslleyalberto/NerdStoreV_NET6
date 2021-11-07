using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggragateId { get; protected set; }
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }

   
}
