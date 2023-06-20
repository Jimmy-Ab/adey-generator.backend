using System;
namespace PrivacyPolicyGeneratorBackend.Application.Features.TelebirrIntegration
{
	public class TelebirrResponseDto
	{
        public int code { get; set; }
        public string newCode { get; set; }
        public object channel { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
        public long dateTime { get; set; }
        public object path { get; set; }
        public object extData { get; set; }
    }
    public class Data
    {
        public string toPayUrl { get; set; }
    }
}

