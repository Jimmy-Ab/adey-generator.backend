using System;
namespace PrivacyPolicyGeneratorBackend.Application.Features.TelebirrIntegration
{
    public class TelebirrCommand
    {
        public string? appId { get; set; }
        public string? nonce { get; set; }
        public string? notifyUrl { get; set; }
        public string? outTradeNo { get; set; }
        public string? receiveName { get; set; }
        public string? returnUrl { get; set; }
        public string? shortCode { get; set; }
        public string? subject { get; set; }
        public string? timeoutExpress { get; set; }
        public string? timestamp { get; set; }
        public string? totalAmount { get; set; }
    }
}

