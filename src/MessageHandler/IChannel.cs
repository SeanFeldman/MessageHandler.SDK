using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageHandler
{
    public interface IChannel
    {
        Task Push(object msg);
        Task Push(object msg, string subject);

        Task Push(IEnumerable msg);
        Task Push(IEnumerable msg, string subject);
    }
}