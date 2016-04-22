using System;
using System.Net;

using BehindTheScenes.WebRequester;

using Microsoft.Practices.Unity;

using Polly;

namespace BehindTheScenes
{
    public class Bootstrapper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var retryPolicy = Policy.Handle<WebException>().WaitAndRetry(3,
                (i => TimeSpan.FromSeconds(Math.Pow(i, 2))),
                (exception, timespan) =>
                    Console.WriteLine($"{exception.GetType()} thrown, will retry in {timespan}"));
            
            Container.RegisterType<IWebRequester, WebRequesterWithPolly>(
                new InjectionConstructor("http://google.nl", retryPolicy));
        }
    }
}