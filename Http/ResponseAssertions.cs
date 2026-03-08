using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
namespace kyleGCSharpPOC.Core.Http {
    public class ResponseAssertions {
        private readonly HttpResponseMessage _response;

        public ResponseAssertions(HttpResponseMessage response) {
            _response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public ResponseAssertions AssertOk() {
            return AssertStatusCode(200, "OK");
        }

        public ResponseAssertions AssertCreated() {
            return AssertStatusCode(201, "Created");
        }

        public ResponseAssertions AssertNoContent() {
            return AssertStatusCode(204, "No Content");
        }

        public ResponseAssertions AssertBadRequest() {
            return AssertStatusCode(400, "Bad Request");
        }

        public ResponseAssertions AssertUnauthorized() {
            return AssertStatusCode(401, "Unauthorized");
        }

        public ResponseAssertions AssertForbidden() {
            return AssertStatusCode(403, "Forbidden");
        }

        public ResponseAssertions AssertNotFound() {
            return AssertStatusCode(404, "Not Found");
        }

        public ResponseAssertions AssertInternalServerError() {
            return AssertStatusCode(500, "Internal Server Error");
        }

        public ResponseAssertions AssertStatusCode(int expectedStatusCode) {
            if ((int)_response.StatusCode != expectedStatusCode) {
                throw new AssertionException($"Expected status code {expectedStatusCode}, but got {(int)_response.StatusCode}");
            }
            return this;
        }

        public ResponseAssertions AssertStatusCode(int expectedStatusCode, string description) {
            if ((int)_response.StatusCode != expectedStatusCode) {
                throw new AssertionException($"Expected status code {expectedStatusCode} ({description}), but got {(int)_response.StatusCode}");
            }
            return this;
        }

        public ResponseAssertions AssertHeaderValue(string headerName, string expectedValue) {
            if (!_response.Headers.TryGetValues(headerName, out var values) && !_response.Content.Headers.TryGetValues(headerName, out values)) {
                throw new AssertionException($"Expected header '{headerName}' not found in response");
            }
            var actualValue = values.FirstOrDefault();
            if (actualValue != expectedValue) {
                throw new AssertionException($"Expected header '{headerName}' to be '{expectedValue}', but got '{actualValue}'");
            }
            return this;
        }

        public ResponseAssertions AssertHeaderExists(string headerName) {
            bool headerExists = _response.Headers.Contains(headerName) || _response.Content.Headers.Contains(headerName);
            if (!headerExists) {
                throw new AssertionException($"Expected header '{headerName}' not found in response");
            }
            return this;
        }

        public ResponseAssertions AssertBodyContains(string expectedText) {
            var body = _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!body.Contains(expectedText, StringComparison.OrdinalIgnoreCase)) {
                throw new AssertionException($"Expected response body to contain '{expectedText}'");
            }
            return this;
        }

        public ResponseAssertions AssertBodyEquals(string expectedBody) {
            var actualBody = _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (actualBody != expectedBody) {
                throw new AssertionException($"Expected response body to equal provided value");
            }
            return this;
        }

        public ResponseAssertions AssertJsonPropertyExists(string propertyPath) {
            var body = _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            using (JsonDocument doc = JsonDocument.Parse(body)) {
                var value = GetJsonPropertyValue(doc.RootElement, propertyPath);
                if (value == null) {
                    throw new AssertionException($"JSON property '{propertyPath}' not found");
                }
            }
            return this;
        }

        public HttpResponseMessage Response => _response;
        public string Body => _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        private static string? GetJsonPropertyValue(JsonElement element, string path) {
            var parts = path.Split('.');
            JsonElement current = element;
            foreach (var part in parts) {
                if (current.ValueKind == JsonValueKind.Object && current.TryGetProperty(part, out var property)) {
                    current = property;
                } else {
                    return null;
                }
            }
            return current.GetRawText();
        }
    }

    public class AssertionException : Exception {
        public AssertionException(string message) : base(message) { }
    }
}