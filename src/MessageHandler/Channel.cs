using System;

namespace MessageHandler
{
    public static class Channel
    {
        public static Func<string> Current { get; set; }
    }
}