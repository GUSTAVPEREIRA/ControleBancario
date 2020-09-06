namespace ControleBancario.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ControleBancario.Model.DTO;
    using ControleBancario.Services.IService;
    using Microsoft.AspNetCore.Authorization;

    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cria um usuário
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "UserName": TESTE,
        ///        "Password": "TESTE"
        ///        "FName": "Teste"
        ///        "LName": "Teste"
        ///        "Email": "teste@mail.com"
        ///     }
        ///
        /// </remarks>                
        /// <response code="200">Retorna o usuário criado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        /// <returns>Retorna o usuário</returns>
        [Route("CreateUser")]
        [HttpPost]
        [Authorize]
        public ActionResult<dynamic> CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var user = _userService.CreateUser(userDTO);


                return new OkObjectResult(new
                {
                    Messa = "Usuário criado!",
                    User = user
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}