using System.Transactions;

namespace Inventory.Application.Abstractions.Persistence
{
    //در طراحی ما Transaction باید با Message Processing مرتبط باشد.

    //پس یک Interface جدید می‌سازیم:

    // این Interface می‌گوید:

    //کاری که به من بدهی را داخل یک Transaction اجرا می‌کنم.
    public interface ITransactionManager
    {
        Task ExecuteAsync(
            Func<CancellationToken, Task> action,
            CancellationToken cancellationToken = default);
    }
}
