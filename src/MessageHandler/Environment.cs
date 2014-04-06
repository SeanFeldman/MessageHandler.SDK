using System;

namespace MessageHandler
{
    public static class Environment
    {
        public static Func<string> Current { get; set; }
    }
}