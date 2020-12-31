using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Neighborhood { get; set; }
        public string Complement { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public static implicit operator Address(AddressDto addressDto)
        {
            if (addressDto != null)
            {
                return new Address()
                {
                    Complement = addressDto.Complement,
                    Description = addressDto.Description,
                    Id = addressDto.Id,
                    LastUpdateDate = addressDto.LastUpdateDate,
                    LastUpdateUser = addressDto.LastUpdateUser,
                    Neighborhood = addressDto.Neighborhood,
                    Number = addressDto.Number
                };
            }

            return null;
        }
    }
}
