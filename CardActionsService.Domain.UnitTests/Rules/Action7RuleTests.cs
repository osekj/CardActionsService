using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action7RuleTests
    {
        private Action7Rule _rule;

        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp() => _rule = new Action7Rule();

        [TestCase(CardStatus.Active, false, ExpectedResult = true, TestName = "Returns TRUE when Active and PIN NOT set")]
        [TestCase(CardStatus.Blocked, true, ExpectedResult = true, TestName = "Returns TRUE when Blocked and PIN is set")]
        [TestCase(CardStatus.Active, true, ExpectedResult = false, TestName = "Returns FALSE when Active and PIN is set")]
        [TestCase(CardStatus.Blocked, false, ExpectedResult = false, TestName = "Returns FALSE when Blocked and PIN NOT set")]
        [TestCase(CardStatus.Closed, false, ExpectedResult = false, TestName = "Returns FALSE when status is Closed (default case)")]
        public bool IsApplicable_Tests(CardStatus status, bool pinSet)
        {
            var card = new CardDetails(CardNumber, CardType.Debit, status, pinSet);
            return _rule.IsApplicable(card);
        }
    }
}
