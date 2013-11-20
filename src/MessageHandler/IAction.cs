namespace MessageHandler
{
    public interface IAction<T>
    {
        void Action(T t);
    }
}
