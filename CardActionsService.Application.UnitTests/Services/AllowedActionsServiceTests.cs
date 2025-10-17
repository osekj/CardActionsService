using CardActionsService.Application.Exceptions;
using CardActionsService.Application.Interfaces;
using CardActionsService.Application.Services;
using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CardActionsService.Application.UnitTests.Services
{
    [TestFixture]
    public class AllowedActionsServiceTests
    {
        private Mock<ILogger<AllowedActionsService>> _mockLogger;
        private Mock<ICardService> _mockCardService;
        private Mock<ICardActionRulesService> _mockCardActionRulesService;
        private AllowedActionsService _allowedActionsService;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<AllowedActionsService>>();
            _mockCardService = new Mock<ICardService>();
            _mockCardActionRulesService = new Mock<ICardActionRulesService>();

            _allowedActionsService = new AllowedActionsService(_mockLogger.Object, _mockCardService.Object, _mockCardActionRulesService.Object);
        }

        [Test]
        public async Task GetAllowedActionsAsync_CardExists_ReturnAllowedActions()
        {
            // Arrange
            string userId = "User1";
            string cardNumber = "Card12";

            var fakeCardDetails = new CardDetails(
                cardNumber,
                CardType.Credit,
                CardStatus.Active,
                true);

            var expectedActions = new List<string> { "ACTION1", "ACTION2" };

            _mockCardService
                .Setup(s => s.GetCardDetails(userId, cardNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCardDetails);

            _mockCardActionRulesService
                .Setup(s => s.GetAllowedActions(fakeCardDetails))
                .Returns(expectedActions);

            // Act
            var result = await _allowedActionsService.GetAllowedActionsAsync(userId, cardNumber, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedActions);

            _mockCardService.Verify(s => s.GetCardDetails(userId, cardNumber, It.IsAny<CancellationToken>()), Times.Once);
            _mockCardActionRulesService.Verify(s => s.GetAllowedActions(fakeCardDetails), Times.Once);
        }

        [Test]
        public async Task GetAllowedActionsAsync_CardDoesNotExist_ThrowCardNotFoundException()
        {
            // Arrange
            var userId = "User4";
            var cardNumber = "NonExistentCard";

            _mockCardService
                .Setup(s => s.GetCardDetails(userId, cardNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync((CardDetails)null);

            // Act
            var act = () => _allowedActionsService.GetAllowedActionsAsync(userId, cardNumber, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<CardNotFoundException>();

            _mockCardService.Verify(s => s.GetCardDetails(userId, cardNumber, It.IsAny<CancellationToken>()), Times.Once);
            _mockCardActionRulesService.Verify(s => s.GetAllowedActions(It.IsAny<CardDetails>()), Times.Never);
        }
    }
}
