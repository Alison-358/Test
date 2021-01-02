using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dto
{
    public class AddressDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Number is required.", AllowEmptyStrings = false)]
        [Display(Name = "Number")]
        [StringLength(200, ErrorMessage = "Number can't be less than 1 and greater than 50.", MinimumLength = 1)]
        public string Number { get; set; }

        [Required(ErrorMessage = "Description is required.", AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        [StringLength(200, ErrorMessage = "Description can't be less than 1 and greater than 200.", MinimumLength = 1)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Neighborhood is required.", AllowEmptyStrings = false)]
        [Display(Name = "Neighborhood")]
        [StringLength(150, ErrorMessage = "Neighborhood can't be less than 1 and greater than 150.", MinimumLength = 1)]
        public string Neighborhood { get; set; }

        [Display(Name = "Complement")]
        [StringLength(200, ErrorMessage = "Complement can't be greater than 200.")]
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
