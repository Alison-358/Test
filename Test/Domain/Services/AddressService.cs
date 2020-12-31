using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class AddressService : ServiceBase<Address>, IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
            : base(addressRepository)
        {
            _addressRepository = addressRepository;
        }
    }
}
