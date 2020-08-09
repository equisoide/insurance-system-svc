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
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "The payload is validated in the call to Validate(payload)")]
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
                var clients = (await SearchClient(payload.Keyword)).OrderBy(c => c.Name);

                if (!clients.Any())
                    response = Error(SearchClientStatus.NoSearchResults);
                else
                    response = Ok<IEnumerable<ClientDto>, SearchClientStatus>(clients, SearchClientStatus.Ok);
            }

            EndLog();
            return response;
        }

        protected abstract Task<IEnumerable<Client>> SearchClient(string keyword);
    }
}
