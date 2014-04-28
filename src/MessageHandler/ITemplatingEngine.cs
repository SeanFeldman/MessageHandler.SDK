namespace MessageHandler
{
    public interface ITemplatingEngine
    {
        string Apply(string template, object message = null, object channel = null, object environment = null, object account = null);
    }
}