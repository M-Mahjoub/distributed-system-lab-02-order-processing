using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Sagas
{
    public enum OrderSagaStatus
    {
        Started,

        ReservingInventory,

        InventoryReserved,

        ProcessingPayment,

        Completed,

        Compensating,

        CompensationCompleted,

        Failed
    }
}
