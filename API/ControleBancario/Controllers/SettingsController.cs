namespace ControleBancario.Controllers
{
    using System;
    using ExtensionMethods;
    using System.Threading.Tasks;
    using ControleBancario.Model;
    using Microsoft.AspNetCore.Mvc;
    using ControleBancario.Model.DTO;
    using System.Collections.Generic;
    using ControleBancario.Services.IService;
    using Swashbuckle.AspNetCore.Annotations;
    using Microsoft.AspNetCore.Authorization;

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
        /// Cria uma configuração
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
        /// <response code="200" message="Setting">Retorna a configuração recém criada</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        [Route("Create")]
        [HttpPost]
        [Authorize]
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

        /// <summary>
        /// Atualiza as informações de uma configuração
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
        /// <response code="200" message="Setting">Retorna uma configuração atualizada</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>            
        [Route("Update")]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<dynamic>> UpdateSetting(SettingsDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(ModelState.GenerateValidation());
                }

                await _settingService.UpdateSettings(dto);
                var setting = _settingService.GetSettingsForID(dto.ID);

                return new OkObjectResult(new
                {
                    Message = "Configuração atualizada",
                    Setting = setting
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma configuração pelo ID
        /// </summary>        
        /// <param name="id"></param>
        /// <response code="200" message="Setting">Retorna uma configuração</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>                
        [Route("Get")]
        [HttpGet]
        [SwaggerResponse(200, "Configuração", typeof(Settings))]
        [SwaggerResponse(400, "Configuração não foi encontrada", typeof(BadRequestResult))]
        [Authorize]
        public ActionResult<dynamic> GetSetting(int id)
        {
            try
            {
                var setting = _settingService.GetSettingsForID(id);

                return new OkObjectResult(new
                {
                    Message = "Configuração encontrada",
                    Setting = setting
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma lista de configurações
        /// </summary>        
        /// <param name="filter"></param>
        /// <param name="isAdmin"></param>
        /// <param name="isManager"></param>
        /// <param name="isCreateUser"></param>
        /// <response code="200" message="Setting">Retorna uma configuração</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>    
        [Route("GetList")]
        [HttpGet]
        [SwaggerResponse(200, "Configuração", typeof(List<Settings>))]
        [Authorize]
        public ActionResult<dynamic> GetListSettings(string filter, bool isAdmin, bool isManager, bool isCreateUser)
        {
            try
            {
                var settings = _settingService.GetSettings(filter, isAdmin, isManager, isCreateUser);

                return new OkObjectResult(new
                {
                    Message = "Configuração encontrada",
                    Settings = settings
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Habilita uma configuração
        /// </summary>        
        /// <param name="id"></param>        
        /// <response code="200">Habilita uma configuração</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>    
        [Route("Enable")]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<dynamic>> EnableSetting(int id)
        {
            try
            {
                await _settingService.UnsetLogigDeleted(id);
                var setting = _settingService.GetSettingsForID(id);

                return new OkObjectResult(new
                {
                    Message = "Configuração foi habilitada",
                    Setting = setting
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Desabilita uma configuração
        /// </summary>        
        /// <param name="id"></param>        
        /// <response code="200">Desabilita uma configuração</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>   
        [Route("Disable")]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<dynamic>> DisableSetting(int id)
        {
            try
            {
                await _settingService.LogicDeleted(id);
                var setting = _settingService.GetSettingsForID(id);

                return new OkObjectResult(new
                {
                    Message = "Configuração foi desabilitada",
                    Setting = setting
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Deleta uma configuração
        /// </summary>        
        /// <param name="id"></param>        
        /// <response code="200">Deleta uma configuração</response>
        /// <response code="400">Retorna nulo e a mensagem do erro</response>   
        [Route("Delete")]
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<dynamic>> DeleteSetting(int id)
        {
            try
            {
                var setting = _settingService.GetSettingsForID(id);
                await _settingService.PhysicalDeleted(id);

                return new OkObjectResult(new
                {
                    Message = "Configuração foi deletada!",
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