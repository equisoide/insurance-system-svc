using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Celerik.NetCore.Services;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;
using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.Core
{
    [ExcludeFromCodeCoverage]
    public class ClientServiceEF<TLoggerCategory>
        : ClientServiceBase<TLoggerCategory, InsuranceDbContext>
    {
        public ClientServiceEF(ApiServiceArgsEF<TLoggerCategory, InsuranceDbContext> args)
            : base(args) { }

        protected override async Task<IEnumerable<Client>> SearchClient(string keyword)
            => await DbContext.Client
                .Where(c => c.Document.Contains(keyword)
                    || c.Name.Contains(keyword)
                    || c.Email.Contains(keyword)
                    || c.CellPhone.Contains(keyword))
                .ToListAsync();
    }
}
