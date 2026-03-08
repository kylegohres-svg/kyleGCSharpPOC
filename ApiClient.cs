using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _client;

    public ApiClient(string baseUrl)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public void SetDefaultRequestHeaders(string name, string value)
    {
        _client.DefaultRequestHeaders.Add(name, value);
    }

    public async Task<T> GetAsync<T>(string uri)
    {
        var response = await _client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }

    public async Task<T> PostAsync<T, U>(string uri, U data)
    {
        var response = await _client.PostAsJsonAsync(uri, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }

    public async Task<T> PutAsync<T, U>(string uri, U data)
    {
        var response = await _client.PutAsJsonAsync(uri, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }

    public async Task DeleteAsync(string uri)
    {
        var response = await _client.DeleteAsync(uri);
        response.EnsureSuccessStatusCode();
    }

    public async Task<T> PatchAsync<T, U>(string uri, U data)
    {
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
        {
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json")
        };
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }

    public async Task<HttpResponseMessage> HeadAsync(string uri)
    {
        var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
        return response;
    }

    public async Task<HttpResponseMessage> OptionsAsync(string uri)
    {
        var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Options, uri));
        return response;
    }

    public void SetTimeout(TimeSpan timeout)
    {
        _client.Timeout = timeout;
    }

    public void SetProxy(IWebProxy proxy)
    {
        var handler = new HttpClientHandler { Proxy = proxy };
        _client = new HttpClient(handler) { BaseAddress = _client.BaseAddress };
    }
}