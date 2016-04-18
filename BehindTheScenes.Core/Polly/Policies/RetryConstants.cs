using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindTheScenes.Core.Polly.Policies
{
    public static class RetryConstants
    {
        public static readonly TimeSpan[] GenericRetryDelays =
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(3)
        };
    }
}
