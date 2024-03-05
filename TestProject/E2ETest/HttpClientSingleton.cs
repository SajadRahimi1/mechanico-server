namespace TestProject.E2ETest;

public sealed class HttpClientSingleton
{
    private static readonly string _baseUrl = "http://localhost:5279";
    private static HttpClient _httpClient;

    private static HttpClientSingleton _instance=null;

    public static HttpClientSingleton Instance
    {
        get { return _instance ??= new HttpClientSingleton(); }
    }

    private HttpClientSingleton()
    {
        _httpClient = new HttpClient();
    }

    public Task<HttpResponseMessage> PostAsync(string url,HttpContent body) => _httpClient.PostAsync($"{_baseUrl}/{url}",body);
    public Task<HttpResponseMessage> GetAsync(string url) => _httpClient.GetAsync($"{_baseUrl}/{url}");
}