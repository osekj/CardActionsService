using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action1RuleTests
    {
        private Action1Rule _rule;

        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp()
        {
            _rule = new Action1Rule();
        }

        [TestCase(CardStatus.Active, ExpectedResult = true, TestName = "Return TRUE when status is Active")]
        [TestCase(CardStatus.Inactive, ExpectedResult = false, TestName = "Return FALSE when status is Inactive")]
        [TestCase(CardStatus.Blocked, ExpectedResult = false, TestName = "Return FALSE when status is Blocked")]
        public bool IsApplicable_Tests(CardStatus status)
        {
            var card = new CardDetails(CardNumber, CardType.Debit, status, false);
            return _rule.IsApplicable(card);
        }
    }
}
