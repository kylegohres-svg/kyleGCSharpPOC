public class ApiClient {
    private string baseUrl;
    private Dictionary<string, string> headers = new Dictionary<string, string>();
    private int timeout = 100;
    private bool followRedirects = true;
    private IWebProxy proxy;

    public ApiClient Given() {
        // Implementation logic
        return this;
    }

    public ApiClient BaseUrl(string url) {
        this.baseUrl = url;
        return this;
    }

    public ApiClient Header(string key, string value) {
        headers[key] = value;
        return this;
    }

    public ApiClient Headers(Dictionary<string, string> headers) {
        foreach (var header in headers)
            this.headers[header.Key] = header.Value;
        return this;
    }

    public ApiClient BearerAuth(string token) {
        Header("Authorization", "Bearer " + token);
        return this;
    }

    public ApiClient JsonContentType() {
        Header("Content-Type", "application/json");
        return this;
    }

    public ApiClient FormContentType() {
        Header("Content-Type", "application/x-www-form-urlencoded");
        return this;
    }

    public ApiClient PlainTextContentType() {
        Header("Content-Type", "text/plain");
        return this;
    }

    public ApiClient ContentType(string type) {
        Header("Content-Type", type);
        return this;
    }

    public ApiClient QueryParam(string key, string value) {
        // Implementation for adding a query parameter
        return this;
    }

    public ApiClient QueryParams(Dictionary<string, string> parameters) {
        foreach (var param in parameters)
            QueryParam(param.Key, param.Value);
        return this;
    }

    public ApiClient Body(object body) {
        // Implementation logic for setting body content
        return this;
    }

    public ApiClient Timeout(int seconds) {
        this.timeout = seconds;
        return this;
    }

    public ApiClient FollowRedirects(bool follow) {
        this.followRedirects = follow;
        return this;
    }

    public ApiClient Proxy(IWebProxy proxy) {
        this.proxy = proxy;
        return this;
    }

    public ApiClient When() {
        // Response should be handled here
        return this;
    }

    public HttpResponseMessage Get(string endpoint) {
        // HTTP GET request logic
    }

    public HttpResponseMessage Post(string endpoint) {
        // HTTP POST request logic
    }

    public HttpResponseMessage Put(string endpoint) {
        // HTTP PUT request logic
    }

    public HttpResponseMessage Patch(string endpoint) {
        // HTTP PATCH request logic
    }

    public HttpResponseMessage Delete(string endpoint) {
        // HTTP DELETE request logic
    }

    public HttpResponseMessage Head(string endpoint) {
        // HTTP HEAD request logic
    }

    public HttpResponseMessage Options(string endpoint) {
        // HTTP OPTIONS request logic
    }

    public HttpResponseMessage Execute() {
        // Helper method to execute HTTP request
    }
}