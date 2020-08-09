using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public class SecurityServiceMock<TLoggerCategory>
        : SecurityServiceBase<TLoggerCategory, DbContext>
    {
        public SecurityServiceMock(ApiServiceArgs<TLoggerCategory> args)
            : base(args) { }

        protected override Task<ApiResponse<SignInDto, SignInStatus>> SignIn(SignInPayload payload)
        {
            throw new System.NotImplementedException();
        }
    }
}
