using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Infrastructure.Tools
{
    public static class AgentTools
    {
        [Description("Returns the current UTC date and time.")]
        public static string GetCurrentTime()
        {
            Console.WriteLine("🔥 GetCurrentTimeTool CALLED!");

            return DateTimeOffset.UtcNow.ToString("O");
        }
    }
}
