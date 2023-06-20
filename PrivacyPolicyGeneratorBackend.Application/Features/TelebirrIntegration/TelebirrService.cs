namespace PrivacyPolicyGeneratorBackend.Application.Features.TelebirrIntegration
{
    public static class TelebirrService
    {

        private static Random random = new Random();

        public static string GenerateStringA(string AppId,string AppKey,string Nonce, string NotifyUrl, string outTradeNo, string ReceiverName, string ReturnUrl, string ShortCode, string Subject, string TimeoutExpress, string Timestamp, string TotalAmount)
        {
            string SignString = $"appId={AppId}&appKey={AppKey}&nonce={Nonce}&notifyUrl={NotifyUrl}&outTradeNo={outTradeNo}&receiveName={ReceiverName}&returnUrl={ReturnUrl}&shortCode={ShortCode}&subject={Subject}&timeoutExpress={TimeoutExpress}&timestamp={Timestamp}&totalAmount={TotalAmount}";
            return  SignString;
        }
        
        public static string NonceString(int length)
        {
            //const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            const string chars = "abcdefghijklmnopqrstuvqxyz0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string OutTradeNumberString(int length)
        {
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string chars = "0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

