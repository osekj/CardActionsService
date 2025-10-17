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
    public class CardActionRulesServiceTests
    {
        private Mock<ILogger<CardActionRulesService>> _mockLogger;
        private CardActionRulesService _cardActionRulesService;

        private const string CardNumber = "Card1";

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<CardActionRulesService>>();
        }

        [Test]
        public void GetAllowedActions_SomeRulesAreApplicable_ReturnApplicableActions()
        {
            // Arrange
            var fakeCardDetails = new CardDetails(
                CardNumber,
                CardType.Credit,
                CardStatus.Active,
                true);

            var applicableRule = new Mock<IActionRule>();
            applicableRule.Setup(r => r.ActionName).Returns("ApplicableAction");
            applicableRule.Setup(r => r.IsApplicable(It.IsAny<CardDetails>())).Returns(true);

            var nonApplicableRule = new Mock<IActionRule>();
            nonApplicableRule.Setup(r => r.ActionName).Returns("NonApplicableAction");
            nonApplicableRule.Setup(r => r.IsApplicable(It.IsAny<CardDetails>())).Returns(false);

            var rules = new List<IActionRule> { applicableRule.Object, nonApplicableRule.Object };
            _cardActionRulesService = new CardActionRulesService(_mockLogger.Object, rules);

            var expectedActions = new[] { applicableRule.Object.ActionName };

            // Act
            var result = _cardActionRulesService.GetAllowedActions(fakeCardDetails);

            // Assert
            result.Should().BeEquivalentTo(expectedActions);

            applicableRule.Verify(r => r.IsApplicable(fakeCardDetails), Times.Once);
            nonApplicableRule.Verify(r => r.IsApplicable(fakeCardDetails), Times.Once);
        }

        [Test]
        public void GetAllowedActions_NoRulesApplicable_ReturnEmptyList()
        {
            // Arrange
            var fakeCardDetails = new CardDetails(CardNumber,
                CardType.Debit,
                CardStatus.Expired,
                false);

            var nonApplicableRule1 = new Mock<IActionRule>();
            nonApplicableRule1.Setup(r => r.IsApplicable(It.IsAny<CardDetails>())).Returns(false);
            var nonApplicableRule2 = new Mock<IActionRule>();
            nonApplicableRule2.Setup(r => r.IsApplicable(It.IsAny<CardDetails>())).Returns(false);

            var rules = new List<IActionRule> { nonApplicableRule1.Object, nonApplicableRule2.Object };
            _cardActionRulesService = new CardActionRulesService(_mockLogger.Object, rules);

            // Act
            var result = _cardActionRulesService.GetAllowedActions(fakeCardDetails);

            // Assert
            result.Should().BeEmpty();

            nonApplicableRule1.Verify(r => r.IsApplicable(fakeCardDetails), Times.Once);
            nonApplicableRule2.Verify(r => r.IsApplicable(fakeCardDetails), Times.Once);
        }
    }
}
