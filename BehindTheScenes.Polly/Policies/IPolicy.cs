using System;

namespace BehindTheScenes.Polly.Policies
{
    public interface IPolicy
    {
        void ExecuteAction(Action action);
        T ExecuteFunc<T>(Func<T> func);
    }
}