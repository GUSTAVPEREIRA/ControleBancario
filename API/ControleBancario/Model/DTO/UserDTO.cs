namespace ControleBancario.Model.DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
    }
}