
using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Configuration;
using PrivacyPolicyGeneratorBackend.SharedKernel.Wrapper;

namespace PrivacyPolicyGeneratorBackend.Application.Features.TelebirrIntegration
{
    public class TelebirrPaymentCommand : IRequest<Result<TelebirrResponseDto>>
    {
        public string? notifyUrl { get; set; }
        public string? returnUrl { get; set; }
        public string? subject { get; set; }
        public string? totalAmount { get; set; }
    }

    public class TelebirrPaymentCommandHandler : IRequestHandler<TelebirrPaymentCommand, Result<TelebirrResponseDto>>
    {
        HttpClient _client = new HttpClient();

        private readonly IConfiguration Configuration;

        public TelebirrPaymentCommandHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public async Task<Result<TelebirrResponseDto>> Handle(TelebirrPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {

                string appId = Configuration["Telebirr:APP_ID"];
                string appKey = Configuration["Telebirr:APP_KEY"];
                string nonce = TelebirrService.NonceString(32);
                string outTradeNo = TelebirrService.OutTradeNumberString(21);
                string publicKey = Configuration["Telebirr:PUBLIC_KEY"];
                string receiveName = Configuration["Telebirr:RECEIVER_NAME"];
                string shortCode = Configuration["Telebirr:SHORT_CODE"];
                string timeoutExpress = Configuration["Telebirr:TIMEOUT"];
                string timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                string url = Configuration["Telebirr:URL"];

                var ussdJson = new TelebirrCommand
                {
                    appId = appId,
                    nonce = nonce,
                    notifyUrl = request.notifyUrl,
                    outTradeNo = outTradeNo,
                    receiveName = receiveName,
                    returnUrl = request.returnUrl,
                    shortCode = shortCode,
                    subject = request.subject,
                    timeoutExpress = timeoutExpress,
                    timestamp = timestamp,
                    totalAmount = request.totalAmount
                };

                var ussdString = JsonSerializer.Serialize(ussdJson);

                var stringA = TelebirrService.GenerateStringA(appId, appKey, nonce, request.notifyUrl, outTradeNo, receiveName, request.returnUrl, shortCode, request.subject, timeoutExpress, timestamp, request.totalAmount);

                string sign = SHAHelper.GetSign(stringA);

                string ussd = RSAHelper.EncryptionByPublicKey(ussdString, publicKey);

                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"appid\":");
                sb.Append($"\"{appId}\"");
                sb.Append(",");
                sb.Append("\"sign\":");
                sb.Append($"\"{sign}\"");
                sb.Append(",");
                sb.Append("\"ussd\":");
                sb.Append($"\"{ussd}\"");
                sb.Append("}");

                _client.DefaultRequestHeaders.Accept.Clear();

                _client.DefaultRequestHeaders.Add("Accept", "application/json;charset=utf-8");

                var content = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");

                var res = await _client.PostAsync(url, content);
                string responseBody = await res.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<TelebirrResponseDto>(responseBody);

                return await Result<TelebirrResponseDto>.SuccessAsync(response, response.message);
            }
            catch (Exception ex)
            {
                throw new Exception("TelebirrPaymentCommandHandler() fail,error:" + ex.Message);
            }
        }
    }
}

