namespace ControleBancario.Controllers
{    
    using Microsoft.AspNetCore.Mvc;

    public class AuthController : ControllerBase
    {
        ///// <summary>
        ///// Autentica o usuário
        ///// </summary>
        ///// <remarks>
        ///// Sample request:
        /////
        /////     POST
        /////     {
        /////        "Username": ADMIN,
        /////        "Password": "ADMIN"
        /////     }
        /////
        ///// </remarks>                
        ///// <response code="200">Retorna o bearer token, é o usuário autenticado</response>
        ///// <response code="400">Retorna nulo e a mensagem do erro</response>            
        ///// <returns>Retorna o token, mais o usuário!</returns>
        //[Route("Authenticate")]
        //[HttpPost]
        //public ActionResult<dynamic> Authenticate([FromBody] UserAuthenticateDTO user)
        //{
        //    try
        //    {
        //        User auxUser = new User(user.Username, user.Password);

        //        User validUser = _userService.GetUserForAuthenticate(auxUser.Username, auxUser.Password);

        //        if (validUser == null)
        //        {
        //            return NotFound(new { Message = "Usuário não encontrado!" });
        //        }

        //        var token = _tokenService.GenerateToken(validUser);
        //        validUser.SetPassword("");

        //        return new OkObjectResult(new
        //        {
        //            BearerToken = token,
        //            User = validUser
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}