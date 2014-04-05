namespace MessageHandler
{
    public interface ITemplatingEngine
    {
        string Apply(string template, object message, object channel = null, object environment = null, object global = null);
    }
}