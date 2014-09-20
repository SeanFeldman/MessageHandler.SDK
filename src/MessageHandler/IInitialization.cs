using System.Threading.Tasks;

namespace MessageHandler
{
    public interface IInitialization
    {
        void Init(IContainer container);
    }
}