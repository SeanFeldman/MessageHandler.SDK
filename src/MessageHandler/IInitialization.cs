using System.Threading.Tasks;

namespace MessageHandler
{
    public interface IInitialization
    {
        Task Init(IContainer container);
    }
}