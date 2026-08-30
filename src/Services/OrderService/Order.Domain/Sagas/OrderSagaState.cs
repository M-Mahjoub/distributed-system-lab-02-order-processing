namespace Order.Domain.Sagas
{
    //چون Order شروع‌کننده‌ی Business Process است. Saga State را در Order Service قرار بدهیم
    public sealed class OrderSagaState
    {
        public Guid Id { get; private set; }

        public Guid OrderId { get; private set; }

        public OrderSagaStatus Status { get; private set; }

        public bool InventoryReserved { get; private set; }

        public bool PaymentSucceeded { get; private set; }

        public bool PaymentFailed { get; private set; }

        public bool InventoryReleased { get; private set; }

        public bool OrderCancelled { get; private set; }

        private OrderSagaState()
        {
        }

        public OrderSagaState(Guid orderId)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Status = OrderSagaStatus.Started;
        }

        public void MarkInventoryReserved()
        {
            InventoryReserved = true;
            Status = OrderSagaStatus.InventoryReserved;
        }

        public void MarkPaymentSucceeded()
        {
            PaymentSucceeded = true;
            Status = OrderSagaStatus.Completed;
        }

        public void MarkPaymentFailed()
        {
            PaymentFailed = true;
            //Status = OrderSagaStatus.Compensating;
            UpdateCompensationStatus();
        }

        public void MarkInventoryReleased()
        {
            InventoryReleased = true;

            UpdateCompensationStatus();
        }

        public void MarkOrderCancelled()
        {
            OrderCancelled = true;

            UpdateCompensationStatus();
        }

        private void UpdateCompensationStatus()
        {
            if (InventoryReleased && OrderCancelled)
            {
                Status = OrderSagaStatus.CompensationCompleted;
            }
        }
    }
}
