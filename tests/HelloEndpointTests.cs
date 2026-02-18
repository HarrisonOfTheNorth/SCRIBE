using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HelloWorldApi.Tests
{
    public class HelloEndpointTests : IAsyncLifetime
    {
        private HttpClient _client = null!;
        private WebApplicationFactory<Program> _factory = null!;

        public async Task InitializeAsync()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            _client?.Dispose();
            _factory?.Dispose();
            await Task.CompletedTask;
        }

        [Fact]
        public async Task PostHello_WithValidRequest_ReturnsOkWithMessage()
        {
            // Act
            var response = await _client.PostAsync("/test/hello", null);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }

        [Fact]
        public async Task PostHello_WithValidRequest_ReturnsCorrectJsonStructure()
        {
            // Act
            var response = await _client.PostAsync("/test/hello", null);
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(responseString);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(json.TryGetProperty("message", out var messageElement));
            Assert.Equal("hello world", messageElement.GetString());
        }

        [Fact]
        public async Task PostHello_WithEmptyBody_ReturnsOkWithMessage()
        {
            // Arrange
            var content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/test/hello", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(responseString);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(json.TryGetProperty("message", out var messageElement));
            Assert.Equal("hello world", messageElement.GetString());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public async Task PostHello_MultipleConsecutiveCalls_AllReturnSameMessage(int _)
        {
            // Act
            var response = await _client.PostAsync("/test/hello", null);
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(responseString);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(json.TryGetProperty("message", out var messageElement));
            Assert.Equal("hello world", messageElement.GetString());
        }

        [Fact]
        public async Task PostHello_ResponseHasCorrectContentType()
        {
            // Act
            var response = await _client.PostAsync("/test/hello", null);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content.Headers.ContentType);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task PostHello_ResponseBodyIsValidJson()
        {
            // Act
            var response = await _client.PostAsync("/test/hello", null);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("{\"message\":\"hello world\"}", responseString);
        }

        [Fact]
        public async Task PostHello_RequestHeaders_ContentTypeNotRequired()
        {
            // Arrange - explicitly create request without Content-Type header
            var request = new HttpRequestMessage(HttpMethod.Post, "/test/hello");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostHello_VerifyNoErrorInResponse()
        {
            // Act
            var response = await _client.PostAsync("/test/hello", null);
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(responseString);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(json.TryGetProperty("error", out _));
        }

        [Fact]
        public async Task PostHello_ResponseMessageFieldIsCorrect()
        {
            // Act
            var response = await _client.PostAsync("/test/hello", null);
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(responseString);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(json.TryGetProperty("message", out var messageElement));
            Assert.Equal("hello world", messageElement.GetString());
        }

        [Fact]
        public async Task PostHello_EmptyRequest_ReturnsOk()
        {
            // Act
            var response = await _client.PostAsync("/test/hello", new StringContent(""));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostHello_ResponseIsConsistent()
        {
            // Act - Call endpoint multiple times
            var response1 = await _client.PostAsync("/test/hello", null);
            var responseString1 = await response1.Content.ReadAsStringAsync();

            var response2 = await _client.PostAsync("/test/hello", null);
            var responseString2 = await response2.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(responseString1, responseString2);
        }
    }
}
