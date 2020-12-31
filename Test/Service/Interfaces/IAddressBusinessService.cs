using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAddressBusinessService
    {
        object GetAddressPager(string filter, int? addressId, int? pageSize, int? page);
        Task<AddressDto> GetAddressByIdAsync(int id);
        Task<AddressDto> EditAsync(AddressDto addressDto);
        Task<AddressDto> AddAsync(AddressDto addressDto);
        void RemoveById(int addressDtoId);
        void Remove(AddressDto addressDtoDto);
    }
}
