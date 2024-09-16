using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Communication.Sniffer
{
    public class Message
    {
        public DateTime DateTime { get; }
        public string Type { get; }
        public string Content { get; }

        public Message(DateTime dateTime, string type, string content)
        {
            DateTime = dateTime;
            Type = type;
            Content = content;
        }
    }
}
