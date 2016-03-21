using System;

using BehindTheScenes.RabbitMq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using Xunit;

namespace BehindTheScenes.Tests.RabbitMq
{
    public class QueueManagerTests
    {
        private readonly IFixture _fixture;
        private readonly string _routingKey;
        private QueueManager _sut;

        public QueueManagerTests()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            _routingKey = _fixture.Create<string>();
        }

        [Fact]
        public void GetReceivingChannel_FirstCall_ShouldCallFactory()
        {
            var factoryMock = _fixture.Freeze<Mock<IChannelFactory>>();

            _sut = _fixture.Create<QueueManager>();

            _sut.GetReceivingChannel(_routingKey);

            factoryMock.Verify(f => f.GetChannel(), Times.Once());
        }

        [Fact]
        public void GetReceivingChannel_SecondCall_ShouldNotCallFactory()
        {
            var factoryMock = _fixture.Freeze<Mock<IChannelFactory>>();

            _sut = _fixture.Create<QueueManager>();

            _sut.GetReceivingChannel(_routingKey);
            _sut.GetReceivingChannel(_routingKey);

            factoryMock.Verify(f => f.GetChannel(), Times.Once());
        }

        [Theory, InlineData(null), InlineData(default(string))]
        public void GetReceivingChannel_WithInvalidRoutingKeys_ShouldFail(string routingKey)
        {
            var factoryMock = _fixture.Freeze<Mock<IChannelFactory>>();

            _sut = _fixture.Create<QueueManager>();

            Action act = () => _sut.GetReceivingChannel(routingKey);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateSendingChannel_FirstCall_ShouldCallFactory()
        {
            var factoryMock = _fixture.Freeze<Mock<IChannelFactory>>();

            _sut = _fixture.Create<QueueManager>();

            _sut.CreateSendingChannel();

            factoryMock.Verify(f => f.GetChannel(), Times.Once());
        }
    }
}