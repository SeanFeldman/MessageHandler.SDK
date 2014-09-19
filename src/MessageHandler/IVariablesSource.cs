namespace MessageHandler
{
    public interface IVariablesSource
    {
        dynamic GetChannelVariables(string channelId);
        dynamic GetAccountVariables(string accountId);
        dynamic GetEnvironmentVariables(string environmentId);
    }
}