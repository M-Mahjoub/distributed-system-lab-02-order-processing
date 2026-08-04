namespace Order.Domain.Orders;

public enum OrderStatus
{
    Pending,
    AwaitingInventory,
    AwaitingPayment,
    AwaitingInvoice,
    Completed,
    Cancelled

}
