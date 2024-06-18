using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QuestionApp.Controllers;
using QuestionApp.Dtos;
using QuestionApp.Entity;
using QuestionApp.Services;
using Xunit;

namespace QuestionApp.Tests
{
    public class QuestionControllerTests
    {
        private readonly Mock<IQuestionService> _mockQuestionService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly QuestionController _controller;

        public QuestionControllerTests()
        {
            _mockQuestionService = new Mock<IQuestionService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new QuestionController(_mockMapper.Object, _mockQuestionService.Object);
        }

        [Fact]
        public async Task CreateQuestion_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var questionDto = new QuestionDtos
            {
                QuestionText = "What is your name?",
                QuestionType = "Text"
            };

            var question = new Question
            {
                QuestionText = "What is your name?",
                QuestionType = "Text"
            };

            var createdQuestion = new Question
            {
                Id = Guid.NewGuid(),
                QuestionText = "What is your name?",
                QuestionType = "Text"
            };

            _mockMapper.Setup(m => m.Map<Question>(It.IsAny<QuestionDtos>())).Returns(question);
            _mockQuestionService.Setup(service => service.AddQuestion(It.IsAny<Question>()))
                                .Callback<Question>(q => q.Id = createdQuestion.Id);

            // Act
            var result = await _controller.CreateQuestion(questionDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetQuestionById), actionResult.ActionName);
            Assert.Equal(createdQuestion.Id, actionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateQuestion_ReturnsNoContentResult()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var questionDto = new QuestionDtos
            {
                QuestionText = "What is your favorite color?",
                QuestionType = "Dropdown"
            };

            var question = new Question
            {
                Id = questionId,
                QuestionText = "What is your favorite color?",
                QuestionType = "Dropdown"
            };

            _mockQuestionService.Setup(service => service.GetQuestionById(questionId)).ReturnsAsync(question);
            _mockMapper.Setup(m => m.Map(questionDto, question)).Returns(question);
            _mockQuestionService.Setup(service => service.UpdateQuestion(question)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateQuestion(questionId, questionDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetQuestions_ReturnsOkObjectResult_WithListOfQuestions()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { Id = Guid.NewGuid(), QuestionText = "What is your name?", QuestionType = "Text" },
                new Question { Id = Guid.NewGuid(), QuestionText = "What is your favorite color?", QuestionType = "Dropdown" }
            };

            var questionDtos = questions.Select(q => new QuestionDtos
            {
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType
            }).ToList();

            _mockQuestionService.Setup(service => service.GetAllQuestions()).ReturnsAsync(questions);
            _mockMapper.Setup(m => m.Map<IEnumerable<QuestionDtos>>(questions)).Returns(questionDtos);

            // Act
            var result = await _controller.GetQuestions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<QuestionDtos>>(okResult.Value);

            Assert.Equal(2, returnValue.Count());
        }
    }
}
