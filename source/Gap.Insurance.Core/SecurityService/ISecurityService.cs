using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public interface ISecurityService
    {
        Task<ApiResponse<SignInDto, SignInStatus>> SignInAsync(SignInPayload payload);
    }
}
