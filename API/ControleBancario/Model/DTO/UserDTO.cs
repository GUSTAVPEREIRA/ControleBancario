namespace ControleBancario.Model.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class UserDTO
    {
        public UserDTO()
        {

        }

        public UserDTO(string userName, string fName, string lName, string password, string email)
        {
            UserName = userName;
            FName = fName;
            LName = lName;
            Password = password;
            Email = email;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "O campo 'Username' é um campo obrigatório")]
        [MaxLength(30, ErrorMessage = "O campo 'Username' tem um tamanho máximo de 30 caracteres")]
        public string UserName { get; set; }

        [MaxLength(60, ErrorMessage = "O campo 'Fname' tem um tamanho máximo de 30 caracteres")]
        public string FName { get; set; }

        [MaxLength(60, ErrorMessage = "O campo 'LName' tem um tamanho máximo de 30 caracteres")]
        public string LName { get; set; }

        [Required(ErrorMessage = "O campo 'Password' é um campo obrigatório")]
        [MaxLength(30, ErrorMessage = "O campo 'Password' tem um tamanho máximo de 30 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é um campo obrigatório")]
        [MaxLength(30, ErrorMessage = "O campo 'Email' tem um tamanho máximo de 100 caracteres")]
        public string Email { get; set; }
    }
}