namespace ControleBancario.Services.IService
{
    using ControleBancario.Model;
    using System.Threading.Tasks;
    using ControleBancario.Model.DTO;

    public interface IUserService
    {
        Task<User> GetUserForPasswordAndUsername(string username, string password);

        void CreateUser(UserDTO userDTO);
    }
}