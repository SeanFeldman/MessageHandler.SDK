using System;

namespace MessageHandler
{
    public static class Account
    {
        public static Func<string> Current { get; set; }
    }
}