using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action4RuleTests
    {
        private Action4Rule _rule;

        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp() => _rule = new Action4Rule();

        [Test]
        public void IsApplicable_ShouldAlwaysReturnTrue()
        {
            var card = new CardDetails(CardNumber, CardType.Debit, CardStatus.Inactive, false);

            _rule.IsApplicable(card).Should().BeTrue();
        }
    }
}
