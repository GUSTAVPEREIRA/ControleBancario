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
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingService;

        public SettingsController(ISettingsService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// Autentica o usuário
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "Name": "Administrador",
        ///         "IsAdmin": true,
        ///         "IsManager": true,
        ///         "IsCreateUser": true
        ///     }
        /// </remarks>                
        /// <param name="dto"></param>
        /// <response code="200" message="Setting">Retorna o bearer token, é o usuário autenticado</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        /// <returns>Retorna o token, mais o usuário!</returns>
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> CreateSetting(SettingsDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(ModelState.GenerateValidation());
                }

                var setting = await _settingService.CreateSettings(dto);

                return new OkObjectResult(new
                {
                    Message = "Configuração criada",
                    Setting = setting
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }
    }
}
