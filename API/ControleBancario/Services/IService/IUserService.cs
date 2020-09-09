namespace ControleBancario.Services.IService
{
    using ControleBancario.Model;
    using System.Threading.Tasks;
    using ControleBancario.Model.DTO;
    using System.Collections.Generic;

    public interface IUserService
    {
        Task<User> GetUserForPasswordAndUsername(UserAuthenticateDTO userAuthenticateDTO);
        Task<User> CreateUser(UserDTO userDTO);
        User GetUserForID(int id);
        Task UpdateUser(UserDTO userDTO);
        User GetActivatedUserForId(int id);
        Task LogicDeleted(int id);
        Task UnsetLogicDeleted(int id);
        Task PhysicalDeleted(int id);
        Task<List<User>> GetListUsers(string filter);
    }
}