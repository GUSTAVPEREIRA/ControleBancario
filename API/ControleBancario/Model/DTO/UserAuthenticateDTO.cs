namespace ControleBancario.Model.DTO
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class UserAuthenticateDTO
    {
        [Required(ErrorMessage = "O campo Username é um campo obrigatório!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo password é um campo obrigatório!")]        
        public string Password { get; set; }
    }
}