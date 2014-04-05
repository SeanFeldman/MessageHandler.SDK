namespace MessageHandler
{
    public interface IScriptingEngine
    {
        string Execute(string script, object message, object channel = null, object environment = null, object global = null);
    }
}