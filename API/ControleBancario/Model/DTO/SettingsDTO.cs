namespace ControleBancario.Model.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class SettingsDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é um campo obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo 'Nome' é um campo obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'IsAdmin' é um campo obrigatório")]
        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "O campo 'IsManager' é um campo obrigatório")]
        public bool IsManager { get; set; }

        [Required(ErrorMessage = "O campo 'IsCreateUser' é um campo obrigatório")]
        public bool IsCreateUser { get; set; }

        public SettingsDTO()
        {

        }
    }
}