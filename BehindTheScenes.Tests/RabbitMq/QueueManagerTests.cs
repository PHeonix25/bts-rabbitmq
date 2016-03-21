using BehindTheScenes.RabbitMq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace BehindTheScenes.Tests.RabbitMq
{
    [TestClass]
    public class QueueManagerTests
    {
        private IFixture _fixture;
        private string _routingKey;
        private QueueManager _sut;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _routingKey = _fixture.Create<string>();
        }

        [TestMethod]
        public void GetReceivingChannel_FirstCall_ShouldCallFactory()
        {
            var factoryMock = _fixture.Freeze<Mock<IChannelFactory>>();

            _sut = _fixture.Create<QueueManager>();

            _sut.GetReceivingChannel(_routingKey);

            factoryMock.Verify(f => f.GetChannel(), Times.Once());
        }

        [TestMethod]
        public void GetReceivingChannel_SecondCall_ShouldNotCallFactory()
        {
            var factoryMock = _fixture.Freeze<Mock<IChannelFactory>>();

            _sut = _fixture.Create<QueueManager>();

            _sut.GetReceivingChannel(_routingKey);
            _sut.GetReceivingChannel(_routingKey);

            factoryMock.Verify(f => f.GetChannel(), Times.Once());
        }

        [TestMethod]
        public void CreateSendingChannel_FirstCall_ShouldCallFactory()
        {
            var factoryMock = _fixture.Freeze<Mock<IChannelFactory>>();

            _sut = _fixture.Create<QueueManager>();

            _sut.CreateSendingChannel();

            factoryMock.Verify(f => f.GetChannel(), Times.Once());
        }
    }
}