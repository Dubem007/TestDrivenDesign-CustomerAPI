using CloudCustomers.Logic;
using CloudCustomers.Logic.Models;
using CloudCustomers.Models.Config;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class UserService_Test
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_Invoke_HttpGetRequest()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();

            var endpoint = "http://example.com";

            var _mockHttpHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);

            var _httpClient = new HttpClient(_mockHttpHandler.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });

            var sut = new UserService(_httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert
            _mockHttpHandler
                .Protected()
                .Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
           );
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();

            var endpoint = "http://example.com";

            var _mockHttpHandler = MockHttpMessageHandler<User>.SetupReturn404();

            var _httpClient = new HttpClient(_mockHttpHandler.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });

            var sut = new UserService(_httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count().Should().Be(0);  
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();

            var endpoint = "http://example.com";

            var _mockHttpHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);

            var _httpClient = new HttpClient(_mockHttpHandler.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });

            var sut = new UserService(_httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count().Should().Be(expectedResponse.Count());
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();

            var endpoint = "https://jsonplaceholder.typicode.com/users";

            var _mockHttpHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);

            var _httpClient = new HttpClient(_mockHttpHandler.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });

            var sut = new UserService(_httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            var uri = new Uri(endpoint);

            //Assert
            //Assert
            _mockHttpHandler
                .Protected()
                .Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get &&
                    req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
           );
        }
    }
}
