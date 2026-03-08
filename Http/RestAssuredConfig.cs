using System;
using System.Collections.Generic;

namespace kyleGCSharpPOC.Core.Http
{
    /// <summary>
    /// Configuration class for REST API client settings.
    /// Provides centralized management for base URLs, default headers, timeouts, and other HTTP configurations.
    /// </summary>
    public class RestAssuredConfig
    {
        /// <summary>
        /// Gets or sets the base URL for all API requests.
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the request timeout in milliseconds.
        /// Default is 30000 (30 seconds).
        /// </summary>
        public int TimeoutMs { get; set; } = 30000;

        /// <summary>
        /// Gets or sets a dictionary of default headers to be included in all requests.
        /// </summary>
        public Dictionary<string, string> DefaultHeaders { get; set; } = new();

        /// <summary>
        /// Gets or sets the default content type for request bodies.
        /// Default is "application/json".
        /// </summary>
        public string DefaultContentType { get; set; } = "application/json";

        /// <summary>
        /// Gets or sets a value indicating whether to automatically follow redirects.
        /// Default is true.
        /// </summary>
        public bool FollowRedirects { get; set; } = true;

        /// <summary>
        /// Gets or sets the maximum number of redirects to follow.
        /// Default is 5.
        /// </summary>
        public int MaxRedirects { get; set; } = 5;

        /// <summary>
        /// Gets or sets a value indicating whether SSL certificate validation should be disabled.
        /// Should only be used in development/testing environments.
        /// Default is false.
        /// </summary>
        public bool DisableSslValidation { get; set; } = false;

        /// <summary>
        /// Gets or sets the proxy URL for routing requests through a proxy server.
        /// </summary>
        public string? ProxyUrl { get; set; }

        /// <summary>
        /// Gets or sets the proxy port.
        /// </summary>
        public int? ProxyPort { get; set; }

        /// <summary>
        /// Creates a new instance of RestAssuredConfig with default settings.
        /// </summary>
        public RestAssuredConfig()
        {
        }

        /// <summary>
        /// Creates a new instance of RestAssuredConfig with a specified base URL.
        /// </summary>
        /// <param name="baseUrl">The base URL for API requests.</param>
        public RestAssuredConfig(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Adds a default header that will be included in all requests.
        /// </summary>
        /// <param name="key">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns>This configuration instance for method chaining.</returns>
        public RestAssuredConfig AddDefaultHeader(string key, string value)
        {
            DefaultHeaders[key] = value;
            return this;
        }

        /// <summary>
        /// Sets the request timeout.
        /// </summary>
        /// <param name="timeoutMs">Timeout in milliseconds.</param>
        /// <returns>This configuration instance for method chaining.</returns>
        public RestAssuredConfig WithTimeout(int timeoutMs)
        {
            TimeoutMs = timeoutMs;
            return this;
        }

        /// <summary>
        /// Validates the configuration settings.
        /// </summary>
        /// <returns>True if valid; otherwise throws an exception.</returns>
        /// <exception cref="InvalidOperationException">Thrown when required configuration is missing.</exception>
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
            {
                throw new InvalidOperationException("BaseUrl is required for REST API configuration.");
            }

            if (TimeoutMs <= 0)
            {
                throw new InvalidOperationException("TimeoutMs must be greater than zero.");
            }

            return true;
        }
    }
}