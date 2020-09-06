namespace ControleBancario.Model.DTO
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class UserAuthenticateDTO
    {
        [Required(ErrorMessage = "O campo 'Username' é um campo obrigatório!")]
        [MaxLength(30, ErrorMessage = "O campo 'Username' tem um tamanho máximo de 30 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo 'Password' é um campo obrigatório!")]
        [MaxLength(30, ErrorMessage = "O campo 'Password' tem um tamanho máximo de 30 caracteres")]
        public string Password { get; set; }
    }
}