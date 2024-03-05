using System.Net;
using System.Text;
using Mechanico_Api.Dtos;
using Newtonsoft.Json;

namespace TestProject.E2ETest;

public class TestUsersApi
{
    private readonly HttpClientSingleton _httpClient;

    public TestUsersApi()
    {
        _httpClient = HttpClientSingleton.Instance;
    }

    [Fact]
    public async void TestSendCodeWithFailedValidation()
    {
        var sendCodeDto = new SendCodeDto { phoneNumber = "sss" };
        var result = await _httpClient.PostAsync("User/send-code", new StringContent(JsonConvert.SerializeObject(sendCodeDto),Encoding.UTF8,"application/json"));
        
        Assert.Equal(HttpStatusCode.BadRequest,result.StatusCode);
    }
}