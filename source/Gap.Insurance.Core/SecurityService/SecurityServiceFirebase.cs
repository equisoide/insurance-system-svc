using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gap.Insurance.Core
{
    [ExcludeFromCodeCoverage]
    public class SecurityServiceFirebase<TLoggerCategory>
        : SecurityServiceBase<TLoggerCategory, InsuranceDbContext>
    {
        private readonly FirebaseConfig _config;

        public SecurityServiceFirebase(ApiServiceArgsEF<TLoggerCategory, InsuranceDbContext> args)
            : base(args) => _config = Config.ReadObject<FirebaseConfig>("Firebase");

        protected override async Task<ApiResponse<SignInDto, SignInStatus>> SignIn(SignInPayload payload)
        {
            var dto = new ApiResponse<SignInDto, SignInStatus>();

            var response = await CallFirebaseAuthRestApi("signInWithPassword", new
            {
                email = payload.Email,
                password = payload.Password,
                returnSecureToken = true
            });

            if (response is string)
            {
                if (response == "EMAIL_NOT_FOUND")
                    dto.StatusCode = SignInStatus.EmailNotFound;
                else if (response == "INVALID_PASSWORD")
                    dto.StatusCode = SignInStatus.PasswordInvalid;
                else if (response == "USER_DISABLED")
                    dto.StatusCode = SignInStatus.UserDisabled;
                else
                {
                    dto.Message = response;
                    dto.StatusCode = SignInStatus.BadRequest;
                }
            }
            else
                dto.Data = new SignInDto
                {
                    AuthToken = response.idToken,
                    Email = response.email
                };

            return dto;
        }

        private async Task<dynamic> CallFirebaseAuthRestApi(
            string action,
            dynamic payload)
        {
            var baseAddress = new Uri(_config.AuthApiBaseUrl);
            using var httpClient = new HttpClient { BaseAddress = baseAddress };

            var requestUri = string.Format(
                CultureInfo.InvariantCulture,
                _config.AuthApiRequestUrl,
                action,
                _config.ApiKey);

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
            var jsonRequest = JsonConvert.SerializeObject(payload);

            httpRequest.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.SendAsync(httpRequest);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            dynamic dynamicResponse = JObject.Parse(stringResponse);

            if (httpResponse.StatusCode == HttpStatusCode.OK)
                if (!string.IsNullOrEmpty((string)dynamicResponse.errorMessage))
                    return (string)dynamicResponse.errorMessage;
                else
                    return dynamicResponse;
            else if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                return dynamicResponse.error.message.ToString();
            else
                throw new HttpRequestException(ServiceResources.Get(
                    "Common.ErrorCallingService",
                    action,
                    httpResponse.StatusCode,
                    stringResponse
                ));
        }
    }
}
