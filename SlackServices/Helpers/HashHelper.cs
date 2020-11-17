//using System.Security.Cryptography;
//using System.Text;

//namespace SlackServices.Helpers
//{
//    public class HashHelper
//    {
//        public static string HmacSHA256(string key, string data)
//        {
//            string hash;
//            UTF8Encoding encoder = new UTF8Encoding();
//            byte[] code = encoder.GetBytes(key);
//            using (HMACSHA256 hmac = new HMACSHA256(code))
//            {
//                byte[] hmBytes = hmac.ComputeHash(encoder.GetBytes(data));
//                hash = ToHexString(hmBytes);
//            }
//            return hash;
//        }

//        public static string ToHexString(byte[] array)
//        {
//            StringBuilder hex = new StringBuilder(array.Length * 2);
//            foreach (byte b in array)
//            {
//                hex.AppendFormat("{0:x2}", b);
//            }
//            return hex.ToString();
//        }
//    }
//}
