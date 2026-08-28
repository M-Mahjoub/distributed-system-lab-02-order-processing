using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Errors;
using Order.Domain.Orders.Events;
using Order.Domain.Orders.Rules;

namespace Order.Domain.Orders
{
    public class Order : AggregateRoot<OrderId>
    {
        private readonly List<OrderItem> _items = new();

        //فقط برای EF Core.
        private Order()
        {

        }

        //Constructor اصلی
        private Order(OrderId orderId, CustomerId customerId) : base(orderId)
        {
            CustomerId = customerId;
            Status = OrderStatus.Pending;
        }

        //چون این کد نباید امکان‌پذیر باشد: order.Items.Add(...)
        public IReadOnlyCollection<OrderItem> OrderItems => _items.AsReadOnly();
        public CustomerId CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }

        //تمام Ruleهای اولیه همینجا بررسی می‌شوند
        public static Result<Order> Create(OrderId orderId, CustomerId customerId)
        {
            var order = new Order(
                orderId,
                customerId
                );

            order.Raise(new OrderCreatedDomainEvent { OrderId = orderId, CustomerId = customerId });

            return Result.Success(order);
        }

        //چون Aggregate نباید از بیرون یک Entity نیمه‌ساخته تحویل بگیرد. اینطوری ننوشتیم به این خاطر هست: public void AddItem(OrderItem item)
        public Result AddItem(ProductSnapshot product, int quantity)
        {
            var existingItem = OrderItems.FirstOrDefault(c => c.ProductId == product.ProductId);
            if (existingItem is not null)
            {
                var resultItem = existingItem.IncreaseQuantity(quantity);

                if (!resultItem.IsSuccess)
                    return Result.Failure(resultItem.Error);

                Raise(new OrderItemQuantityChangedDomainEvent(
                    product,
                    quantity));

                return Result.Success();
            }

            var orderItem = OrderItem.Create(product, quantity);

            _items.Add(orderItem);

            Raise(new OrderItemAddedDomainEvent(
                product,
                quantity));

            return Result.Success();
        }

        public Result Confirm()
        {
            //var result = EnsureCanConfirm();

            if (Status != OrderStatus.PendingPayment)
            {
                return Result.Failure(
                    OrderErrors.InvalidStatus);
            }

            Status = OrderStatus.Confirmed;

            //Raise(new OrderConfirmedDomainEvent(Id));

            return Result.Success();
        }

        public Result Cancel()
        {
            if (Status != OrderStatus.PendingPayment)
            {
                return Result.Failure(
                    OrderErrors.InvalidStatus);
            }

            Status = OrderStatus.Cancelled;

            return Result.Success();
        }


        //private Result EnsureCanConfirm()
        //{
        //    return Ensure(
        //        new CannotConfirmEmptyOrderRule(_items),
        //        new OrderMustBePendingRule(Status),
        //        new CustomerMustBeActiveRule(CustomerStatus),
        //        new OrderMustHaveValidCurrencyRule(Currency));
        //}

        //RemoveItem
        //Confirm
        //Cancel
        //MarkPaymentSucceeded
        //MarkPaymentFailed


    }
}
