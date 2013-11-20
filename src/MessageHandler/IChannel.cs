namespace MessageHandler
{
    public interface IChannel
    {
        void Push(object msg);
    }
}