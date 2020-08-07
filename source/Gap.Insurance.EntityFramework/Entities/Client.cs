using System;
using System.Collections.Generic;

namespace Gap.Insurance.EntityFramework
{
    public partial class Client
    {
        public Client()
        {
            Policy = new HashSet<Policy>();
        }

        public int ClientId { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Policy> Policy { get; set; }
    }
}
