using Microsoft.AspNetCore.Http;
using SlackServices.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SlackServices
{
    public class RequestVerificationService : IRequestVerificationService
    {
        public bool IsUrlVerificationRequest(dynamic data, out string challenge)
        {
            challenge = null;

            dynamic type = data?.type;
            if (type != null && type == "url_verification")
            {
                challenge = data.challenge;
                return true;
            }

            return false;
        }

        public bool IsVerifiedSlackRequest(HttpRequest request, string requestBody, string slackSigningSecret)
        {
            request.Headers.TryGetValue("X-Slack-Signature", out var slackSignature);
            request.Headers.TryGetValue("X-Slack-Request-Timestamp", out var requestTimestamp);

            return IsVerifiedSlackRequest(requestTimestamp, requestBody, slackSignature, slackSigningSecret);
        }

        public bool IsVerifiedSlackRequest(string requestTimestamp, string requestBody,
            string slackSignature, string slackSigningSecret)
        {
            if (string.IsNullOrEmpty(requestTimestamp))
                return false;

            if (string.IsNullOrEmpty(slackSignature))
                return false;

            if (!IsRequestLessThan5MinutesOld(requestTimestamp))
                return false;

            if (!HasValidSignature(requestTimestamp, requestBody, slackSignature, slackSigningSecret))
                return false;

            return true;
        }

        private bool HasValidSignature(string requestTimestamp, string requestBody,
            string slackSignature, string slackSigningSecret)
        {
            var baseString = ConcatenateBaseString(requestTimestamp, requestBody);
            var hashedSignature = $"v0={CreateHmacSHA256Hash(slackSigningSecret, baseString)}";

            var comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashedSignature, slackSignature) == 0;
        }

        private bool IsRequestLessThan5MinutesOld(string timestampString)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds() - long.Parse(timestampString);
            var fiveMinutes = 60 * 5;

            // If request timestamp is more than 5 minutes old reject it
            if (timestamp > fiveMinutes)
                return false;

            return true;
        }

        private string ConcatenateBaseString(string timestamp, string requestBody)
        {
            const string version = "v0";
            return $"{version}:{timestamp}:{requestBody}";
        }

        private static string CreateHmacSHA256Hash(string key, string data)
        {
            string hash;
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] code = encoder.GetBytes(key);
            using (HMACSHA256 hmac = new HMACSHA256(code))
            {
                byte[] hmBytes = hmac.ComputeHash(encoder.GetBytes(data));
                hash = ToHexString(hmBytes);
            }
            return hash;
        }

        private static string ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}
