using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action8RuleTests
    {
        private Action8Rule _rule;

        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp() => _rule = new Action8Rule();

        [TestCase(CardStatus.Ordered, ExpectedResult = true, TestName = "Returns TRUE when status is Ordered")]
        [TestCase(CardStatus.Blocked, ExpectedResult = true, TestName = "Returns TRUE when status is Blocked")]
        [TestCase(CardStatus.Expired, ExpectedResult = false, TestName = "Returns FALSE when status is Expired")]
        [TestCase(CardStatus.Closed, ExpectedResult = false, TestName = "Returns FALSE when status is Closed")]
        public bool IsApplicable_Tests(CardStatus status)
        {
            var card = new CardDetails(CardNumber, CardType.Debit, status, false);
            return _rule.IsApplicable(card);
        }
    }
}
