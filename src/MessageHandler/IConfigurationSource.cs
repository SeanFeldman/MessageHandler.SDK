using System.Threading.Tasks;

namespace MessageHandler
{
    public interface IConfigurationSource
    {
        Task<T> GetConfiguration<T>() where T : class, new();
    }
}