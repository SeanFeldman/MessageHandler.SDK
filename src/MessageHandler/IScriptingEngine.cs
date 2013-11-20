namespace MessageHandler
{
    public interface IScriptingEngine
    {
        bool Run(object message, string filter);
    }
}