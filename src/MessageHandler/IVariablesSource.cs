using System.Threading.Tasks;

namespace MessageHandler
{
    public interface IVariablesSource
    {
        Task<dynamic> GetChannelVariables(string channelId);
        Task<dynamic> GetAccountVariables(string accountId);
        Task<dynamic> GetEnvironmentVariables(string environmentId);
    }
}