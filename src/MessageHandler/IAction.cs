using System.Threading.Tasks;

namespace MessageHandler
{
    public interface IAction<T>
    {
        Task Action(T t);

        Task Complete();
    }
}
