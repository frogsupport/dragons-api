using Dragons.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dragons.Api.Test.IntegrationTests
{
    public class DragonControllerTests
    {
        [Fact]
        public async void TestAuthentication()
        {
            // Given
            string baseUrl = "https://localhost:3000/api";

            var application = new WebApplicationFactory<Program>();
            
            var httpClient = application.CreateClient();

            // Create the authorization parameter
            var parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes("test:passwordtest"));

            // Set the authorization header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("DragonsApiAuthentication", parameter);

            // Set the base address
            application.Server.BaseAddress = new Uri(baseUrl);

            string url = baseUrl + "/dragons";

            // When
            var response = await httpClient.GetAsync(url);

            // Then
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/dragons")]
        [InlineData("/users")]
        public async Task TestGetEndpoints(string path)
        {
            // Given
            string baseUrl = "https://localhost:3000/api";

            var application = new WebApplicationFactory<Program>();

            var httpClient = application.CreateClient();

            var parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes("test:passwordtest"));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("DragonsApiAuthentication", parameter);

            // Reset the base address
            application.Server.BaseAddress = new Uri(baseUrl);

            string url = baseUrl + path;

            // When
            var response = await httpClient.GetAsync(url);

            // Then
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
            Assert.NotEmpty(await response.Content.ReadAsStringAsync());
        }
    }
}