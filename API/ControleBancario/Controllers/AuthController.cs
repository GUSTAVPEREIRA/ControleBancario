﻿namespace ControleBancario.Controllers
{
    using System;
    using ExtensionMethods;
    using System.Threading.Tasks;
    using ControleBancario.Model;
    using Microsoft.AspNetCore.Mvc;
    using ControleBancario.Model.DTO;
    using ControleBancario.Services.IService;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Autentica o usuário
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Username": ADMIN,
        ///        "Password": "ADMIN"
        ///     }
        ///
        /// </remarks>                
        /// <response code="200">Retorna o bearer token, é o usuário autenticado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        /// <returns>Retorna o token, mais o usuário!</returns>
        [Route("Authenticate")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UserAuthenticateDTO userAuthenticateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"{ModelState.GenerateValidation()}");
                }

                User validUser = await _userService.GetUserForPasswordAndUsername(userAuthenticateDTO);

                if (validUser == null)
                {
                    return NotFound(new { Message = "Usuário não encontrado!" });
                }

                if (validUser.DeletedAt != null)
                {
                    return BadRequest(new 
                    { 
                        Message = $"O usuário {validUser.FName} {validUser.LName}, foi bloqueado na data {validUser.DeletedAt}!"
                    });
                }


                var token = _tokenService.GenerateToken(validUser);
                validUser.SetPassword("");

                return new OkObjectResult(new
                {
                    BearerToken = token,
                    User = validUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}