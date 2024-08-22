using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMS.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyMicroservices.SMS.PayamakServiceIR.Providers;
public class PayamakServiceIRProvider : BaseSMSProvider
{
    private readonly string _userName;
    private readonly string _password;
    private readonly string _apiAddress;
    private readonly HttpClient _httpClient;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="apiAddress"></param>
    public PayamakServiceIRProvider(string userName, string password, string apiAddress = default, HttpClient httpClient = default)
    {
        userName.ThrowIfNull(nameof(userName));
        password.ThrowIfNull(nameof(password));
        _userName = userName;
        _password = password;
        _apiAddress = apiAddress;
        if (httpClient is null)
            httpClient = new HttpClient();
        _httpClient = httpClient;

        if (_apiAddress is null)
            _apiAddress = "https://ippanel.com/api/select";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="multipleTextMessageRequest"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    protected override async Task<List<string>> ApiSendAsync(MultipleTextMessageRequest multipleTextMessageRequest)
    {
        var requestContent = @$"{{""op"" : ""send"",
                ""uname"" : ""{_userName}"",
                ""pass"":  ""{_password}"",
                ""message"" : ""{multipleTextMessageRequest.Text}"",
                ""from"": ""{multipleTextMessageRequest.Senders.First()}"",
                ""to"" : [{string.Join(",", multipleTextMessageRequest.ToNumbers?.ToArray()
            .Select(n => $"\"{n}\""))}]}}";
        var content = new StringContent(requestContent, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_apiAddress, content);
        var json = await response.Content.ReadAsStringAsync();
        if (json.StartsWith("[0,"))
            return new List<string>()
            {
                json
            };
        else
            throw new System.Exception($"Send sms error : {json}");
    }
}