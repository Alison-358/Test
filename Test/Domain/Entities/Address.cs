using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Neighborhood { get; set; }
        public string Complement { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
