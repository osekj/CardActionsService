using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Rules;
using NUnit.Framework;

namespace CardActionsService.Domain.UnitTests.Rules
{
    [TestFixture]
    public class Action5RuleTests
    {
        private Action5Rule _rule;

        private const string CardNumber = "Card123456789";

        [SetUp]
        public void SetUp() => _rule = new Action5Rule();

        [TestCase(CardType.Credit, ExpectedResult = true, TestName = "Returns TRUE when type is Credit")]
        [TestCase(CardType.Debit, ExpectedResult = false, TestName = "Returns FALSE when type is Debit")]
        [TestCase(CardType.Prepaid, ExpectedResult = false, TestName = "Returns FALSE when type is Prepaid")]
        public bool IsApplicable_Tests(CardType type)
        {
            var card = new CardDetails(CardNumber, type, CardStatus.Active, false);
            return _rule.IsApplicable(card);
        }
    }
}
