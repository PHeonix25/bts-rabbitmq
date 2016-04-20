using BehindTheScenes.WebRequester;

using Microsoft.Practices.Unity;

namespace BehindTheScenes
{
    public class Bootstrapper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IWebRequester, SimpleWebRequester>(
                new InjectionConstructor("http://google.nl"));
        }
    }
}