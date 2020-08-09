using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gap.Insurance.EntityFramework
{
    [ExcludeFromCodeCoverage]
    public partial class Client
    {
        public Client()
        {
            ClientPolicy = new HashSet<ClientPolicy>();
        }

        public int ClientId { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<ClientPolicy> ClientPolicy { get; set; }
    }
}
