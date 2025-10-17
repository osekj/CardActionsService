using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action11RuleTests
    {
        private Action11Rule _rule;
        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp() => _rule = new Action11Rule();

        [TestCase(CardStatus.Inactive, ExpectedResult = true, TestName = "Returns TRUE for Inactive")]
        [TestCase(CardStatus.Active, ExpectedResult = true, TestName = "Returns TRUE for Active")]
        [TestCase(CardStatus.Ordered, ExpectedResult = false, TestName = "Returns FALSE for Ordered")]
        [TestCase(CardStatus.Blocked, ExpectedResult = false, TestName = "Returns FALSE for Blocked")]
        public bool IsApplicable_Tests(CardStatus status)
        {
            var card = new CardDetails(CardNumber, CardType.Debit, status, false);
            return _rule.IsApplicable(card);
        }
    }
}
