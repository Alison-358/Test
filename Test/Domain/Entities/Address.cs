using Domain.Dto;
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

        public static implicit operator AddressDto(Address address)
        {
            return new Address()
            {
                Complement = address.Complement,
                Description = address.Description,
                Id = address.Id,
                LastUpdateDate = address.LastUpdateDate,
                LastUpdateUser = address.LastUpdateUser,
                Neighborhood = address.Neighborhood,
                Number = address.Number
            };
        }
    }
}
