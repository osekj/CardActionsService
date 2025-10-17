using CardActionsService.Api.Controllers;
using CardActionsService.Api.DTOs.Requests;
using CardActionsService.Api.DTOs.Responses;
using CardActionsService.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CardActionsService.Api.UnitTests.Controllers
{
    [TestFixture]
    public class CardActionsControllerTests
    {
        private Mock<ILogger<CardActionsController>> _mockLogger;
        private Mock<IAllowedActionsService> _mockAllowedActionsService;
        private CardActionsController _cardActionsController;

        private static IEnumerable<TestCaseData> AllowedActionsTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> { "ACTION1", "ACTION2" })
                    .SetName("Return list of allowed actions when multiple apply");
                yield return new TestCaseData(new List<string>())
                    .SetName("Return an empty list when no rules apply");
            }
        }

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<CardActionsController>>();
            _mockAllowedActionsService = new Mock<IAllowedActionsService>();

            _cardActionsController = new CardActionsController(_mockLogger.Object, _mockAllowedActionsService.Object);
        }

        [Test]
        [TestCaseSource(nameof(AllowedActionsTestCases))]
        public async Task GetAllowedActions_ValidInput_ReturnOkStatusWithAllowedActions(List<string> expectedActions)
        {
            // Arrange
            var fakeRequestDto = new GetAllowedActionRequest
            {
                UserId = "User1",
                CardNumber = "Card12"
            };

            // Arrange
            _mockAllowedActionsService
                .Setup(s => s.GetAllowedActionsAsync(fakeRequestDto.UserId, fakeRequestDto.CardNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedActions);

            // Act
            var result = await _cardActionsController.GetAllowedActions(fakeRequestDto, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<GetAllowedActionsResponse>()
                .Which.AllowedActions.Should().BeEquivalentTo(expectedActions);

            _mockAllowedActionsService.Verify(s => s.GetAllowedActionsAsync(fakeRequestDto.UserId, fakeRequestDto.CardNumber, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
