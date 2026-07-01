using FluentAssertions;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;

namespace BasicAPITests
{
    public class BasicTests
    {
        private RestClientOptions restClientOptions { get; }
        public BasicTests( ) 
        {
            this.restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true

            };
        }
        [Fact]
        public async Task GetAsyncTest()
        {                      
            RestClient restClient = new RestClient(restClientOptions);
            string? token = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET");
            RestRequest restRequest = new RestRequest("Product/GetProductById/1");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");
            RestResponse restResponse = await restClient.GetAsync(restRequest);
            restResponse.Content.Should().NotBeNull();
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Fact]
        public async Task GetAsyncWithUrlSegment()
        {
            RestClient restClient = new RestClient(restClientOptions);
            string? token = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET");
            RestRequest restRequest = new RestRequest("Product/GetProductById/{id}");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddUrlSegment("id", 2);
            var restResponse = await restClient.GetAsync(restRequest);
            
        }

        [Fact]
        public async Task GetAsyncWithQueryParameter()
        {
            RestClient restClient = new RestClient(restClientOptions);
            string? token = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET");
            RestRequest restRequest = new RestRequest("Product/GetProductByIdAndName");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddQueryParameter("id", 2);
            restRequest.AddQueryParameter("name", "Monitor");
            var restResponse = await restClient.GetAsync(restRequest);
            Product? product = JsonSerializer.Deserialize<Product>(restResponse.Content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.Equal("Monitor", product?.Name);
        

        }

    }
}
