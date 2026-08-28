using ShoppingAgent.Application.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.Abstractions
{
    public interface ITokenCounter
    {
        int CountTokens(ChatMessageDto message);
    }
}
