using System;

namespace MessageHandler
{
    public static class Handler
    {
        public static Func<string> Current { get; set; }
    }
}