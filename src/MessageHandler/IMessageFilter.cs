namespace MessageHandler
{
    public interface IMessageFilter
    {
        bool Run(string filter, object message = null, object channel = null, object environment = null, object global = null);
    }
}