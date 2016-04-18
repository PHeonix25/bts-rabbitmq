using System;

namespace BehindTheScenes.Core.Polly.Policies
{
    public interface IRetryPolicy
    {
        void ExecuteAction(Action action);
        T ExecuteFunc<T>(Func<T> func);
    }
}