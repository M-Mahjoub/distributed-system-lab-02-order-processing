using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Features.Orders.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Controllers
{
    public static class DependencyInjection
    {
        public static IServiceCollection PeresentationDI(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(
                typeof(CreateOrderCommandHandler).Assembly
                ));

            return serviceDescriptors;
        }
    }
}
