using System;

namespace MessageHandler
{
    public interface IStandingQuery<T> : IStandingQuery<T, T>
    {
    }

    public interface IStandingQuery<TIn, TOut>
    {
        IObservable<TOut> Compose(IObservable<TIn> interval);
    }
}