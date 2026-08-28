namespace Order.Domain.Orders;

public enum OrderStatus
{
    Pending,
    PendingPayment,
    AwaitingInventory,
    AwaitingPayment,
    AwaitingInvoice,
    Confirmed,
    Completed,
    Cancelled

}
