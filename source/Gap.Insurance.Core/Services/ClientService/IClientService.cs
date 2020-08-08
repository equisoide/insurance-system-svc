using System.Collections.Generic;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public interface IClientService
    {
        Task<ApiResponse<IEnumerable<ClientDto>, SearchClientStatus>> SearchClientAsync(SearchClientPayload payload);
    }
}
