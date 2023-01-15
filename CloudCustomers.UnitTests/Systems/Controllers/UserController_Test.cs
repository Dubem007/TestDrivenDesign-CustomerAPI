using CloudCustomers.API.Controllers;
using CloudCustomers.Interface;
using CloudCustomers.Logic.Models;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers
{
    public class UserController_Test
    {
        [Fact]
        public async Task Get_OnSuccess_StatusCode_200()
        {
            var _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(s => s.GetAllUsers())
                .ReturnsAsync(new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Name = "Jhon",
                        Email = "some.email@example.com",
                        Address = new Address()
                        {
                            Street = "Second Avenue",
                            City = "San Jose",
                            ZipCode = "50723"
                        }
                    }
                });

            var sut = new UserController(_mockUserService.Object);

            var result = (OkObjectResult)await sut.GetUsers();

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnNotFound_StatusCode_404()
        {
            var _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(s => s.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UserController(_mockUserService.Object);

            var result = await sut.GetUsers();

            result.Should().BeOfType<NotFoundResult>();

            var objectResult = (NotFoundResult)result;

            objectResult.StatusCode.Should().Be(404);
        }

        /// <returns>
        ///     -> Should verify that the invocation occurs at least once
        /// </returns>
        [Fact]
        public async Task Get_OnSuccess_Invoke_UsrsService()
        {
            var _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(s => s.GetAllUsers())
                .ReturnsAsync(UserFixture.GetTestUsers());

            var sut = new UserController(_mockUserService.Object);

            var result = await sut.GetUsers();

            _mockUserService.Verify(s => s.GetAllUsers(), Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_ListOf_Users()
        {
            var _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(s => s.GetAllUsers())
                .ReturnsAsync(UserFixture.GetTestUsers());

            var sut = new UserController(_mockUserService.Object);

            var result = await sut.GetUsers();

            result.Should().BeOfType<OkObjectResult>();

            var objectResult = (OkObjectResult)result;

            objectResult.Value.Should().BeOfType<List<User>>();
        }
    }
}
