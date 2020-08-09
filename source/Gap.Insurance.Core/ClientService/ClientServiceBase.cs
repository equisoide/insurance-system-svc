using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    public abstract class ClientServiceBase<TLoggerCategory, TDbContext>
        : ApiServiceEF<TLoggerCategory, InsuranceResources, TDbContext>, IClientService
            where TDbContext : DbContext
    {
        public ClientServiceBase(ApiServiceArgs<TLoggerCategory> args)
            : base(args) { }

        [ExcludeFromCodeCoverage]
        public ClientServiceBase(ApiServiceArgsEF<TLoggerCategory, TDbContext> args)
            : base(args) { }

        public async Task<ApiResponse<IEnumerable<ClientDto>, SearchClientStatus>> SearchClientAsync(SearchClientPayload payload)
        {
            StartLog();
            ApiResponse<IEnumerable<ClientDto>, SearchClientStatus> response;

            if (!Validate(payload, out string message, out string property))
                response = Error<SearchClientStatus>(message, property);
            else
            {
                var clients = payload.ClientId == 0
                    ? (await SearchClientByKeyWord(payload.Keyword)).OrderBy(c => c.Name)
                    : (await SearchClientById(payload.ClientId));

                response = new ApiResponse<IEnumerable<ClientDto>, SearchClientStatus>
                {
                    Data = Mapper.Map<IEnumerable<ClientDto>>(clients),
                    StatusCode = SearchClientStatus.Ok,
                    Success = true
                };
            }

            EndLog();
            return response;
        }

        protected abstract Task<IEnumerable<Client>> SearchClientById(int clientId);
        protected abstract Task<IEnumerable<Client>> SearchClientByKeyWord(string keyword);
    }
}
