using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action12RuleTests
    {
        private Action12Rule _rule;
        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp() => _rule = new Action12Rule();

        [TestCase(CardStatus.Ordered, ExpectedResult = true, TestName = "Returns TRUE for Ordered")]
        [TestCase(CardStatus.Active, ExpectedResult = true, TestName = "Returns TRUE for Active")]
        [TestCase(CardStatus.Restricted, ExpectedResult = false, TestName = "Returns FALSE for Restricted")]
        [TestCase(CardStatus.Expired, ExpectedResult = false, TestName = "Returns FALSE for Expired")]
        public bool IsApplicable_Tests(CardStatus status)
        {
            var card = new CardDetails(CardNumber, CardType.Debit, status, false);
            return _rule.IsApplicable(card);
        }
    }
}
