namespace ControleBancario.Model.DTO
{
    using System.ComponentModel.DataAnnotations;
    public class ConfigurationDTO
    {
        [MaxLength(100, ErrorMessage = "O campo 'Email' tem o tamanho máximo de 100 caracteres!")]
        [Required(ErrorMessage = "O campo 'Email' é um campo obrigatório!")]
        public string Email { get; set; }
    }
}