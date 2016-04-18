using System;

namespace BehindTheScenes.Core.Polly.Policies
{
    public static class RetryConstants
    {
        public static readonly TimeSpan[] GenericRetryDelays =
        {
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(10) 
        };
    }
}