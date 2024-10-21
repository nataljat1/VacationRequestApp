using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using VacationRequestApi.Controllers;
using VacationRequestApi.Services;
using VacationRequestApi.Data.Models;
using VacationRequestApi.VacationRequestsDto;

namespace VacationRequestApi.Tests.Controllers
{
    public class VacationRequestsControllerTests
    {
        private readonly Mock<IVacationRequestService> _vacationRequestServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly VacationRequestsController _controller;

        public VacationRequestsControllerTests()
        {
            _vacationRequestServiceMock = new Mock<IVacationRequestService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new VacationRequestsController(_vacationRequestServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetVacationRequests_ShouldReturnOk_WhenRequestsExist()
        {
            // Arrange
            var vacationRequests = new List<VacationRequest>
            {
                new VacationRequest { Id = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), VacationDays = 5, Comment = "Holiday" }
            };

            var vacationRequestDtos = new List<VacationRequestDto>
            {
                new VacationRequestDto { Id = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), VacationDays = 5, Comment = "Holiday" }
            };

            _vacationRequestServiceMock.Setup(service => service.GetVacationRequests())
                .ReturnsAsync(vacationRequests);

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<VacationRequestDto>>(vacationRequests))
                .Returns(vacationRequestDtos);

            // Act
            var result = await _controller.GetVacationRequests();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(vacationRequestDtos);
        }

        [Fact]
        public async Task GetVacationRequests_ShouldReturnOk_WhenNoRequestsExist()
        {
            // Arrange
            _vacationRequestServiceMock.Setup(service => service.GetVacationRequests())
                .ReturnsAsync(new List<VacationRequest>());

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<VacationRequestDto>>(It.IsAny<IEnumerable<VacationRequest>>()))
                .Returns(new List<VacationRequestDto>());

            // Act
            var result = await _controller.GetVacationRequests();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            var returnedList = okResult.Value as IEnumerable<VacationRequestDto>;
            returnedList.Should().BeEmpty();
        }

        [Fact]
        public async Task PostVacationRequest_ShouldReturnCreated_WhenValidRequest()
        {
            // Arrange
            var createDto = new CreateVacationRequestDto
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                VacationDays = 5,
                Comment = "Holiday"
            };

            var vacationRequest = new VacationRequest
            {
                Id = 1,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                VacationDays = createDto.VacationDays,
                Comment = createDto.Comment
            };

            var vacationRequestDto = new VacationRequestDto
            {
                Id = vacationRequest.Id,
                StartDate = vacationRequest.StartDate,
                EndDate = vacationRequest.EndDate,
                VacationDays = vacationRequest.VacationDays,
                Comment = vacationRequest.Comment
            };

            _mapperMock.Setup(mapper => mapper.Map<VacationRequest>(createDto))
                .Returns(vacationRequest);

            _vacationRequestServiceMock.Setup(service => service.CreateVacationRequest(vacationRequest))
                .ReturnsAsync(vacationRequest);

            _mapperMock.Setup(mapper => mapper.Map<VacationRequestDto>(vacationRequest))
                .Returns(vacationRequestDto);

            // Act
            var result = await _controller.PostVacationRequest(createDto);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(201);
            createdResult.Value.Should().BeEquivalentTo(vacationRequestDto);
        }

        [Fact]
        public async Task PostVacationRequest_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            // Start date is not set
            _controller.ModelState.AddModelError("StartDate", "Start date is required.");

            var createDto = new CreateVacationRequestDto
            {
                EndDate = DateTime.Now.AddDays(5),
                VacationDays = 5,
                Comment = "Holiday"
            };

            // Act
            var result = await _controller.PostVacationRequest(createDto);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult!.StatusCode.Should().Be(400);
        }
    }
}
