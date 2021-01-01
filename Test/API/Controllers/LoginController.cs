using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthenticationBusinessService _authenticationBusinessService;

        public LoginController(ILogger<LoginController> logger, IAuthenticationBusinessService authenticationBusinessService)
        {
            _logger = logger;
            _authenticationBusinessService = authenticationBusinessService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/login")]
        public object PostAsync([FromBody][Required] UserLoginDto body)
        {
            try
            {
                if (body == null)
                    return BadRequest();

                var userCredentials = _authenticationBusinessService.Login(body);

                return base.StatusCode(200, userCredentials);
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
    }
}
