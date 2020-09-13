namespace ControleBancario.Controllers
{
    using System;
    using ExtensionMethods;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ControleBancario.Model.DTO;
    using ControleBancario.Services.IService;

    [Route("api/[controller]")]
    [ApiController]
    public class FirstInstallConfigurationController : ControllerBase
    {
        private readonly IUserService _userService;

        public FirstInstallConfigurationController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Realiza as configurações iniciais do sistema e cria um usuário
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {        
        ///        "Email": "teste@mail.com"
        ///     }
        ///
        /// </remarks>                
        /// <response code="200">Retorna o usuário criado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        /// <returns>Retorna o usuário</returns>

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> FirstIntall([FromBody] ConfigurationDTO configuration)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    throw new Exception(ModelState.GenerateValidation());
                }

                UserDTO userDTO = new UserDTO("ADMIN", "ADMINISTRADOR", "SISTEMA", "ADMIN", configuration.Email);
                var user = await _userService.CreateUser(userDTO);

                return new OkObjectResult(new
                {
                    Mensagem = "Configurações iniciais parâmetrizadas!",
                    Usuario = user
                });
            }
            catch (Exception ex)
            {

                string message = ex.Message;

                if (message.Equals("Este username já existe!"))
                {
                    message = "As configurações iniciais, já foram realizadas!";
                }

                return BadRequest(message);
            }
        }
    }
}