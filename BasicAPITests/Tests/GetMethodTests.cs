using BasicAPITests.Base;
using FluentAssertions;
using GraphQLProductApp.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;

namespace BasicAPITests.Tests
{
    public class GetMethodTests : IClassFixture<RestLibrary>
    {
        private readonly RestClient _client;
        string? token = String.Empty;
        public GetMethodTests(RestLibrary restLibrary) 
        {
            _client = restLibrary.restClient;

            this.token = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET");
        }
        [Fact]
        public async Task GetAsyncTest()
        {                      
            RestRequest restRequest = new RestRequest("Product/GetProductById/1");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");
            RestResponse restResponse = await _client.GetAsync(restRequest);
            restResponse.Content.Should().NotBeNull();
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Fact]
        public async Task GetAsyncWithUrlSegment()
        {
            RestRequest restRequest = new RestRequest("Product/GetProductById/{id}");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddUrlSegment("id", 2);
            var restResponse = await _client.GetAsync(restRequest);
            
        }

        [Fact]
        public async Task GetAsyncWithQueryParameter()
        {
            RestRequest restRequest = new RestRequest("Product/GetProductByIdAndName");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddQueryParameter("id", 2);
            restRequest.AddQueryParameter("name", "Monitor");
            var restResponse = await _client.GetAsync(restRequest);
            Product? product = JsonSerializer.Deserialize<Product>(restResponse.Content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Assert.Equal("Monitor", product?.Name);
        

        }

    }
}
