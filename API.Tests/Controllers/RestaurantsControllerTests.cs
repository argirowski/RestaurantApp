using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace API.Tests.Controllers
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        public RestaurantsControllerTests(WebApplicationFactory<Program> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Singleton(typeof(IRestaurantsRepository), _ => _restaurantsRepositoryMock.Object));
                });
            });
        }

        [Fact]
        public async Task GetAllRestaurants_ShouldReturnOk()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/restaurants?pageNumber=1&pageSize=10");
            // Act
            var response = await client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetRestaurantById_ShouldReturnOk()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/restaurants/1");
            // Act
            var response = await client.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

    }
}
