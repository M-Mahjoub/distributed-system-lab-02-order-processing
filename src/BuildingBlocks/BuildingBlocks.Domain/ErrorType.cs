using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public enum ErrorType
    {
        None,
        Validation,
        Business,
        Conflict,
        NotFound,
        Unauthorized,
        Forbidden,
        Failure
    }
}
