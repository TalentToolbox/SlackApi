using Microsoft.AspNetCore.Http;

namespace SlackServices.Interfaces
{
    public interface IRequestVerificationService
    {
        bool IsUrlVerificationRequest(dynamic data, out string challenge);
        bool IsVerifiedSlackRequest(HttpRequest request, string requestBody, string slackSigningSecret);
        bool IsVerifiedSlackRequest(string requestTimestamp, string requestBody, string slackSignature, string slackSigningSecret);
    }
}
