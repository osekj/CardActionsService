using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Infrastructure.Services;
using FluentAssertions;
using NUnit.Framework;

namespace CardActionsService.Infrastructure.UnitTests.Services
{
    [TestFixture]
    public class CardServiceTests
    {
        private const string ExistingUserId = "User1";
        private const string ExistingCardNumber = "Card11";

        private const string NonExistentUserId = "NonExistentUser";
        private const string NonExistentCardNumber = "NonExistentCardNumber";

        private CardService _cardService;

        [SetUp]
        public void SetUp()
        {
            _cardService = new CardService();
        }

        [Test]
        public async Task GetCardDetails_UserAndCardExist_ReturnCardDetails()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            var fakeExpectedCard = new CardDetails(
                CardNumber: ExistingCardNumber,
                CardType: CardType.Prepaid,
                CardStatus: CardStatus.Ordered,
                IsPinSet: false);

            // Act
            var result = await _cardService.GetCardDetails(ExistingUserId, ExistingCardNumber, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(fakeExpectedCard);
        }

        [TestCase(NonExistentUserId, ExistingCardNumber, TestName = "Return NULL when user does not exist")]
        [TestCase(ExistingUserId, NonExistentCardNumber, TestName = "Return NULL when card does not exist")]
        [TestCase(NonExistentUserId, NonExistentCardNumber, TestName = "Return NULL when both do not exist")]
        [Test]
        public async Task GetCardDetails_InvalidUserOrCard_ReturnNull(string userId, string cardNumber)
        {
            // Act
            var result = await _cardService.GetCardDetails(userId, cardNumber, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
