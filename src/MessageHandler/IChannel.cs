using System.Threading.Tasks;

namespace MessageHandler
{
    public interface IChannel
    {
        Task Push(object msg);
    }
}