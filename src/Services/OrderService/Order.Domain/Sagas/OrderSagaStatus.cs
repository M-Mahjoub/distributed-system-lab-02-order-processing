namespace Order.Domain.Sagas;

public enum OrderSagaStatus
{
    Started = 1,

    InventoryReservationPending = 2,

    InventoryReserved = 3,

    PaymentPending = 4,

    PaymentCompleted = 5,

    Compensating = 6,

    CompensationCompleted = 7,

    Failed = 8
}