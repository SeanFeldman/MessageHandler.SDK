namespace MessageHandler
{
    public interface IChannel
    {
        void Push(object msg);
    }

    public interface IStream
    {
        void Start();
        void Stop();
    }
}