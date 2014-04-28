namespace MessageHandler
{
    public interface IScriptingEngine
    {
        string Execute(string script, object message = null, object channel = null, object environment = null, object account = null);
    }
}