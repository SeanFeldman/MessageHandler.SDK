namespace MessageHandler
{
    public interface IMessageFilter
    {
        bool Run(string filter, object message, object channel = null, object environment = null, object global = null);
    }
}