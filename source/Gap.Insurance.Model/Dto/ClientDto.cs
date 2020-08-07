using System;

namespace Gap.Insurance.Model
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
