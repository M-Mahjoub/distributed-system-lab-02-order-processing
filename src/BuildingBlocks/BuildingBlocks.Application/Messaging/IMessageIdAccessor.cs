using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Application.Messaging
{
    public interface IMessageIdAccessor<in TMessage>
    {
        Guid GetId(TMessage message);
    }
}
