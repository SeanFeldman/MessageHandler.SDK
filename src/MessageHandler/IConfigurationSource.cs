namespace MessageHandler
{
    public interface IConfigurationSource
    {
        T GetConfiguration<T>() where T : class, new();
    }
}