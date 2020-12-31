using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces.Services;
using Service.Interfaces;
using Service.Interfaces.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AddressBusinessService : IAddressBusinessService
    {
        private readonly IAddressService _addressService;
        private readonly ISessionCurrent _sessionCurrent;
        private Address _address = new Address();
        private AddressDto _addressDto = new AddressDto();

        public AddressBusinessService(IAddressService addressService, ISessionCurrent sessionCurrent)
        {
            _addressService = addressService;
            _sessionCurrent = sessionCurrent;
        }

        private void SetDateAndUserValuesBySession(AddressDto addressDto)
        {
            addressDto.LastUpdateUser = "Sem sessão por enquanto";//_sessionCurrent.UserName();
            addressDto.LastUpdateDate = DateTime.Now;
        }

        public Task<AddressDto> AddAsync(AddressDto addressDto)
        {
            this.SetDateAndUserValuesBySession(addressDto);
            _address = addressDto;
            addressDto = _addressService.AddAsync(_address).Result;
            return Task.Run(() => addressDto);
        }

        public Task<AddressDto> GetAddressByIdAsync(int id)
        {
            _address = _addressService.GetById(id);
            _addressDto = _address;
            return Task.Run(() => _addressDto);
        }

        public object GetAddressPager(string filter, int? addressId, int? pageSize, int? page)
        {
            var maxRows = 0;

            string queryCount = $@"
                                DECLARE @name VARCHAR(100), @id INT, @isDisabled BIT

                                SET @name = '{filter}'
                                SET @id = '{addressId}'

                                IF @id = 0
                                    SET @id = null

                                SELECT * FROM (
                                SELECT ROW_NUMBER() OVER (ORDER BY Id Asc) AS row_number, * FROM
                                Address 
                                WHERE (@name IS NULL OR Complement LIKE '%'+@name+'%' OR Description LIKE '%'+@name+'%'
                                        OR Number LIKE '%'+@name+'%' OR Neighborhood LIKE '%'+@name+'%'
                                      ) AND
                                (@id IS NULL OR Id = @id))
                                NumberedTable";

            var listQueryUserCount = _addressService.SQLQueryStringList(queryCount).ToList();

            if (maxRows == 0)
                maxRows = listQueryUserCount.Count();

            var listDto = listQueryUserCount.Select(p => (AddressDto) p).ToList();

            var objParam = new
            {
                filter = filter,
                id = addressId
            };

            return Utils.Helpers.Paginate.Paged<AddressDto>.Pagination(listDto, maxRows, objParam, pageSize, page);
        }

        public void Remove(AddressDto addressDto)
        {
            _address = addressDto;
            _addressService.Remove(_address);
        }

        public void RemoveById(int addressDtoId)
        {
            _address = new Address() { Id = addressDtoId };
            _addressService.Remove(_address);
        }

        public Task<AddressDto> EditAsync(AddressDto addressDto)
        {
            this.SetDateAndUserValuesBySession(addressDto);
            _address = addressDto;
            _addressDto = _addressService.EditAsync(_address).Result;
            return Task.Run(() => addressDto); 
        }
    }
}
