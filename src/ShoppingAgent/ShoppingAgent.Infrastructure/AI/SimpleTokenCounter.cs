using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Infrastructure.AI
{
    // محاسبه Token
    public sealed class SimpleTokenCounter : ITokenCounter
    {
        public int CountTokens(ChatMessageDto message)
        {
            var text =
                string.Join(
                    " ",
                    message.Contents
                        .OfType<TextChatContent>()
                        .Select(x => x.Text));

            if (string.IsNullOrWhiteSpace(text))
                return 0;

            // تقریب ساده:
            // هر 4 کاراکتر ≈ یک token
            return Math.Max(
                1,
                (int)Math.Ceiling(text.Length / 4.0));
        }
    }
}
