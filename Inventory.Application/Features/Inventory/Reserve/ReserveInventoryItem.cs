using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Inventory.Reserve
{
    public sealed record ReserveInventoryItem(
    Guid ProductId,
    int Quantity);
}
