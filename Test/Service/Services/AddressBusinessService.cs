using Domain.Dto;
using Domain.Interfaces.Services;
using Service.Interfaces;
using Service.Interfaces.Generics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AddressBusinessService : IAddressBusinessService
    {
        private readonly IAddressService _addressService;
        private readonly ISessionCurrent _sessionCurrent;

        public AddressBusinessService(IAddressService addressService, ISessionCurrent sessionCurrent)
        {
            _addressService = addressService;
            _sessionCurrent = sessionCurrent;
        }

        public Task<AddressDto> AddAsync(AddressDto addressDto)
        {
            throw new NotImplementedException();
        }

        public Task<AddressDto> GetAddressByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public object GetAddressPager(string filter, int? addressId, string shortDirection, bool? isDisabled, string orderBy, int? pageSize, int? page)
        {
            throw new NotImplementedException();
        }

        public void Remove(AddressDto addressDtoDto)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int addressDtoId)
        {
            throw new NotImplementedException();
        }

        public Task<AddressDto> UpdateAsync(AddressDto addressDto)
        {
            throw new NotImplementedException();
        }
    }
}
