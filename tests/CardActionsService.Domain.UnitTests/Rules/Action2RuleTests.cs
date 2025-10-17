using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action2RuleTests
    {
        private Action2Rule _rule;

        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp() => _rule = new Action2Rule();

        [TestCase(CardStatus.Inactive, ExpectedResult = true, TestName = "Return TRUE when status is Inactive")]
        [TestCase(CardStatus.Active, ExpectedResult = false, TestName = "Return FALSE when status is Active")]
        [TestCase(CardStatus.Ordered, ExpectedResult = false, TestName = "Return FALSE when status is Ordered")]
        public bool IsApplicable_Tests(CardStatus status)
        {
            var card = new CardDetails(CardNumber, CardType.Debit, status, false);
            return _rule.IsApplicable(card);
        }
    }
}
