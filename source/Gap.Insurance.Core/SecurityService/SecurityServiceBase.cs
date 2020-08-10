using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public abstract class SecurityServiceBase<TLoggerCategory, TDbContext>
        : ApiServiceEF<TLoggerCategory, InsuranceResources, TDbContext>, ISecurityService
            where TDbContext : DbContext
    {
        public SecurityServiceBase(ApiServiceArgs<TLoggerCategory> args)
            : base(args) { }

        [ExcludeFromCodeCoverage]
        public SecurityServiceBase(ApiServiceArgsEF<TLoggerCategory, TDbContext> args)
            : base(args) { }

        public async Task<ApiResponse<SignInDto, SignInStatus>> SignInAsync(SignInPayload payload)
        {
            StartLog();
            ApiResponse<SignInDto, SignInStatus> response;

            if (!Validate(payload, out string message, out string property))
                response = Error<SignInStatus>(message, property);
            else
            {
                response = await SignIn(payload);
                ProcessResponse(response, SignInStatus.Ok);

                if (response.StatusCode == SignInStatus.Ok)
                {
                    response.Message = null;
                    response.MessageType = null;
                }
            }

            EndLog();
            return response;
        }

        protected abstract Task<ApiResponse<SignInDto, SignInStatus>> SignIn(SignInPayload payload);
    }
}
