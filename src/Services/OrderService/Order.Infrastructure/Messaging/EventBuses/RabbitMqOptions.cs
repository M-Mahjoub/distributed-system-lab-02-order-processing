using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.EventBuses
{
    public class RabbitMqOptions
    {
        public string HostAddress { get; set; }
        public string Usename { get; set; }
        public string Password { get; set; }
    }
}
