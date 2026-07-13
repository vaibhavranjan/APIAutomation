using BasicAPITests.Base;
using FluentAssertions;
using GraphQLParser;
using GraphQLProductApp.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAPITests.Tests
{
    public class PostMethodTests : IClassFixture<RestLibrary>
    {
        private readonly RestClient _client;
        string? token = String.Empty;
        public PostMethodTests(RestLibrary restLibrary)
        {
            _client = restLibrary.restClient;
            this.token = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET");
        }


        [Fact]
        public async Task PostAsync()
        {
            RestRequest restRequest = new RestRequest("Product/Create");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddJsonBody(new Product
            {
                Name = "Cabinet",
                Description = "PC Cabinet",
                Price = 300,
                ProductType = ProductType.PERIPHARALS
            });
            Product? product = await _client.PostAsync<Product>(restRequest);

            Assert.Equal("Cabinet", product?.Name);


        }
        // Post Method with File Upload
        [Fact]
        public async Task PostAsyncFileUploadTest()
        {
            RestRequest restRequest = new RestRequest("Product");
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddFile("myFile", @"C:\Vaibhav_Work\APIAutomation\Data\google.jpg", "multipart/form-data");

            var response = await _client.PostAsync(restRequest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);


        }
    }
}
