using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Orders.Rules
{
    public class CannotConfirmEmptyOrderRule : IBusinessRule
    {
        public Error Error => throw new NotImplementedException();

        public bool IsBroken()
        {
            throw new NotImplementedException();
        }
    }
}
