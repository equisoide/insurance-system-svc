using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Resources;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "The payload is validated in the call to Validate(payload)")]
    public class ClientServiceMock<TLoggerCategory>
        : ClientServiceBase<TLoggerCategory, DbContext>
    {
        public ClientServiceMock(ApiServiceArgs<TLoggerCategory> args)
            : base(args) { }

        protected override Task<IEnumerable<Client>> SearchClientById(int clientId)
        {
            var clients = MockData.Clients.Where(c => c.ClientId == clientId);
            return Task.FromResult(clients);
        }

        protected override Task<IEnumerable<Client>> SearchClientByKeyWord(string keyword)
        {
            keyword = keyword.ToLower();

            var clients = MockData.Clients
                .Where(c => c.Document.ToLower().Contains(keyword)
                    || c.Name.ToLower().Contains(keyword)
                    || c.Email.ToLower().Contains(keyword)
                    || c.CellPhone.ToLower().Contains(keyword));

            return Task.FromResult(clients);
        }
    }
}
