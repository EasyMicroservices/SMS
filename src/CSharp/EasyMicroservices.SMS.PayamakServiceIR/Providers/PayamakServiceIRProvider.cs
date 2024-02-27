using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMS.Providers;
using PayamakServiceIR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMicroservices.SMS.PayamakServiceIR.Providers;
public class PayamakServiceIRProvider : BaseSMSProvider
{
    private readonly string _userName;
    private readonly string _password;
    private readonly string _apiAddress;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="apiAddress"></param>
    public PayamakServiceIRProvider(string userName, string password, string apiAddress = default)
    {
        userName.ThrowIfNull(nameof(userName));
        password.ThrowIfNull(nameof(password));
        _userName = userName;
        _password = password;
        _apiAddress = apiAddress;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="multipleTextMessageRequest"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    protected override async Task<List<string>> ApiSendAsync(MultipleTextMessageRequest multipleTextMessageRequest)
    {
        var sendServiceClient = new SendServiceClient();
        var result = await sendServiceClient.SendSMSAsync(new SendSMSRequest()
        {
            userName = _userName,
            password = _password,
            fromNumber = multipleTextMessageRequest.Senders.First(),
            toNumbers = multipleTextMessageRequest.ToNumbers.ToArray(),

        });
        if (result.SendSMSResult == 0)
        {
            return new List<string>()
            {
                result.recId.ToString()
            }.Concat(result.recId.Select(x => x.ToString()))
            .ToList();
        }
        else
        {
            throw new System.Exception($"Send sms error : {string.Join(",", result.status.Select(x => x.ToString()))}");
        }
    }
}
