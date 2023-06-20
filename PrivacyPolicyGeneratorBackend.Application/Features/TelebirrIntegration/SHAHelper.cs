using System.Text;
using System.Security.Cryptography;


namespace PrivacyPolicyGeneratorBackend.Application.Features.TelebirrIntegration
{
    public static class SHAHelper
    {

        public static string GetSign(string strData)
        {
            byte[] bytValue = Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                string sign = sb.ToString();
                return sign;
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }
        }


    }
}

