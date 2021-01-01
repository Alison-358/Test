using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    //[Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressBusinessService _addressBusinessService;

        public AddressController(ILogger<AddressController> logger, IAddressBusinessService addressBusinessService)
        {
            _logger = logger;
            _addressBusinessService = addressBusinessService;
        }

        /// <summary>
        /// Find Addresses by filter
        /// </summary>
        /// <remarks>Returns a array of Address</remarks>
        /// <param name="filter">Filter of Address to return. Param opitional</param>
        /// <param name="addressId">Id of Address to return. Param opitional</param>
        /// <param name="orderBy">Use shortDirection to order query. Param opitional</param>
        /// <param name="shortDirection">For what you need to order query. Param opitional</param>
        /// <param name="isDisabled">Disabled Role or no, param opitional</param>
        /// <param name="page" name="pageSize">Use these parameters to return a list paged. 
        /// If use these, you must fill in both parameters, if you do not fill in or fill in only one, 
        /// you will receive a non-paged list. Params opitional</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid filter</response>
        /// <response code="404">Address not found</response>
        /// <response code="500">Internal exception</response>
        [HttpGet]
        [Route("/api/address")]
        [ProducesResponseType(statusCode: 200, type: typeof(Collection<AddressDto>))]
        public ActionResult<IEnumerable<object>> Get([FromQuery] string filter, [FromQuery] int? addressId, [FromQuery] int? page = null, [FromQuery] int? pageSize = null)
        {
            try
            {
                var addressDtos = _addressBusinessService.GetAddressPager(filter, addressId, pageSize, page);

                return Ok(addressDtos);
            }
            catch (ValidationException ve)
            {
                return StatusCode(400, ve);
            }
            catch (NotFoundException nf)
            {
                return StatusCode(400, nf);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex);
            }
        }

        /// <summary>
        /// Get Address by Address ID
        /// </summary>
        /// <remarks>Returns a true or false success</remarks>
        /// <param name="addressId">ID of Address to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation exception</response>
        /// <response code="404">Address not found</response>
        /// <response code="500">Internal exception</response>
        [HttpGet("{addressId}")]
        [Route("/api/address/{addressId}")]
        [ProducesResponseType(statusCode: 200, type: typeof(AddressDto))]
        public ActionResult<AddressDto> GetAddressById([FromRoute][Required] int addressId)
        {
            try
            {
                var address = _addressBusinessService.GetAddressByIdAsync(addressId);

                if (address.Result == null)
                    return StatusCode(204, new { });
                else
                    return StatusCode(200, address.Result);
            }
            catch (ValidationException ve)
            {
                return StatusCode(400, ve);
            }
            catch (NotFoundException nfe)
            {
                return StatusCode(404, nfe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <param name="body">Address object that needs to be added to the database</param>
        /// <response code="201">Created object</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Internal exception</response>
        [HttpPost]
        [Route("/api/address")]
        [Authorize("admin")]
        public async Task<IActionResult> AddAddress([FromBody][Required] AddressDto body)
        {
            try
            {
                if (body == null)
                {
                    return BadRequest();
                }

                var addressDB = await _addressBusinessService.AddAsync(body);

                if (addressDB.Id != 0)
                    return StatusCode(204);
                else
                    return StatusCode(201);
            }
            catch (ValidationException ve)
            {
                return base.StatusCode(400, ve);
            }
            catch (Exception ex)
            {
                return base.StatusCode(500, ex);
            }
        }

        /// <param name="body">Address object that needs to be updated to the database</param>
        /// <response code="201">Successful create operation</response>
        /// <response code="204">Successful update operation</response>
        /// <response code="400">Validation exception</response>
        /// <response code="404">Address not found</response>
        /// <response code="500">Internal exception</response>
        [HttpPut]
        [Route("/api/address")]
        [Authorize("admin")]
        public async Task<IActionResult> EditAddress([FromBody][Required] AddressDto body)
        {
            try
            {
                if (body == null || body.Id == 0)
                {
                    return BadRequest();
                }

                var addressDB = await _addressBusinessService.EditAsync(body);

                if (addressDB.Id != 0)
                    return StatusCode(204);
                else
                    return StatusCode(201);
            }
            catch (ValidationException ve)
            {
                return StatusCode(400, ve);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <param name="addressId">Address ID to delete</param>
        /// <response code="204">Successful operation</response>
        /// <response code="400">Validation exception</response>
        /// <response code="404">Address not found</response>
        /// <response code="500">Internal exception</response>
        [HttpDelete]
        [Route("/api/address/{addressId}")]
        [ProducesResponseType(statusCode: 204)]
        [Authorize("admin")]
        public IActionResult RemoveAddress([FromRoute][Required] int addressId)
        {
            try
            {
                if (addressId == 0)
                {
                    return BadRequest();
                }

                _addressBusinessService.RemoveById(addressId);

                return base.StatusCode(204);
            }
            catch (ValidationException ve)
            {
                return base.StatusCode(400, ve);
            }
            catch (NotFoundException nfe)
            {
                return base.StatusCode(404, nfe);
            }
            catch (Exception ex)
            {
                return base.StatusCode(500, ex);
            }
        }
    }
}
