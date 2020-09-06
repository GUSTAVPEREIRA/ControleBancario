namespace ControleBancario.Services.IService
{
    using ControleBancario.Model;
    using System.Threading.Tasks;
    using ControleBancario.Model.DTO;

    public interface IUserService
    {
        Task<User> GetUserForPasswordAndUsername(string username, string password);

        Task<User> CreateUser(UserDTO userDTO);
    }
}