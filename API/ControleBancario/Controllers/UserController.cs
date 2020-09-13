namespace ControleBancario.Controllers
{
    using System;
    using ExtensionMethods;
    using ControleBancario.Model;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ControleBancario.Model.DTO;
    using System.Collections.Generic;
    using ControleBancario.Services.IService;
    using Microsoft.AspNetCore.Authorization;
    using Swashbuckle.AspNetCore.Annotations;
    using System.ComponentModel.DataAnnotations;

    [Route("api/[controller]")]
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
        /// </remarks>                
        /// <param name="userDTO"></param>
        /// <response code="200">Retorna o usuário criado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        /// <returns>Retorna o usuário</returns>
        [Route("Create")]
        [HttpPost]
        [Authorize]
        [SwaggerResponse(200, "Usuário criado!", typeof(User))]
        public async Task<ActionResult<dynamic>> CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(ModelState.GenerateValidation());
                }

                User user = await _userService.CreateUser(userDTO);
                user.SetPassword("");

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

        /// <summary>
        /// Retorna um usuário pelo ID
        /// </summary>               
        /// <response code="200">Retorna o usuário</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>      
        /// <param name="id">Código do usuário</param>
        /// <returns>Retorna o usuário</returns>
        [Route("Get")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(200, "Usuário encontrado", typeof(User))]        
        public ActionResult<dynamic> GetUser([FromQuery, Required] int id)
        {
            try
            {
                var user = _userService.GetUserForID(id);

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado!");
                }

                user.SetPassword("");

                return new OkObjectResult(new
                {
                    Message = "Usuário encontrado",
                    Usuario = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um usuário
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
        /// </remarks>                
        /// <response code="200">Retorna o usuário atualizado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        /// <returns>Retorna o usuário</returns>
        [Route("Update")]
        [HttpPut]
        [Authorize]
        [SwaggerResponse(200, "Usuário atualizado", typeof(User))]
        public async Task<ActionResult<dynamic>> UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.UpdateUser(userDTO);
                var user = _userService.GetUserForID(userDTO.ID);
                user.SetPassword("");

                return new OkObjectResult(new
                {
                    Message = "Usuário atualizado",
                    Usuario = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Desabilita um usuário
        /// </summary>        
        /// <response code="200">Retorna o usuário desabilitado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>       
        /// <param name="id">Código do usuário</param>
        /// <returns>Retorna o usuário</returns>
        [Route("Disable")]
        [HttpDelete]
        [Authorize]
        [SwaggerResponse(200, "Usuário foi desabilitado!", typeof(User))]
        public async Task<ActionResult<dynamic>> DisableUser([FromQuery, Required] int id)
        {
            try
            {
                await _userService.LogicDeleted(id);
                var user = _userService.GetUserForID(id);

                return new OkObjectResult(new
                {
                    Message = "Usuário foi desabilitado!",
                    Usuario = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Habilitado um usuário
        /// </summary>        
        /// <response code="200">Retorna o usuário habilitado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>  
        /// <param name="id">Código do usuário</param>
        /// <returns>Retorna o usuário</returns>
        [Route("Enable")]
        [HttpPut]
        [Authorize]
        [SwaggerResponse(200, "Usuário foi habilitado!", typeof(User))]
        public async Task<ActionResult<dynamic>> EnableUser([FromQuery, Required] int id)
        {
            try
            {
                await _userService.UnsetLogicDeleted(id);
                var user = _userService.GetUserForID(id);

                return new OkObjectResult(new
                {
                    Message = "Usuário foi habilitado!",
                    Usuario = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deleta um usuário
        /// </summary>        
        /// <response code="200">Delete um usuário</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>    
        /// <param name="id">Código do usuário</param>
        /// <returns>Retorna o usuário</returns>
        [Route("Delete")]
        [HttpDelete]
        [Authorize]
        [SwaggerResponse(200, "Usuário {Gustavo} {Pereira} foi deletado!")]
        public async Task<ActionResult<dynamic>> DeletedUser([FromQuery, Required] int id)
        {
            try
            {
                var user = _userService.GetUserForID(id);
                await _userService.PhysicalDeleted(id);

                return new OkObjectResult(new
                {
                    Message = $"Usuário {user.FName} {user.LName} foi deletado!",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma lista de usuários
        /// </summary>        
        /// <response code="200">Retorna lista de usuários</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>    
        /// <param name="filter">Filtro sendo username, fname, lname, email</param>
        /// <returns>Retorna o usuário</returns>
        [Route("GetList")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(200, "Lista de usuários", typeof(List<User>))]
        public async Task<ActionResult<dynamic>> GetUserList([FromQuery] string filter)
        {
            try
            {
                var userList = await _userService.GetListUsers(filter);

                return new OkObjectResult(new
                {
                    Lista = userList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}