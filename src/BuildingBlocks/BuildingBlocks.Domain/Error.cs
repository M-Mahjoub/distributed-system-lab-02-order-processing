using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public record Error(string Code, ErrorType Type)
    {
        public static readonly Error None =
                                     new(
                                         string.Empty,
                                         ErrorType.None);
    }
}
