namespace ControleBancario.Services.Service
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ControleBancario.Model;
    using ControleBancario.Model.DTO;
    using Microsoft.EntityFrameworkCore;
    using ControleBancario.Services.IService;

    public class UserService : IUserService
    {        
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {            
            _context = context;
        }

        public void CreateUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserForPasswordAndUsername(string username, string password)
        {
            //Feito pois existe uma regra de hash no password
            User userAux = new User(username, password);

            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("O username é um campo obrigatório!");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("O password é um campo obrigatório!");
            }

            User user = await _context.TbUsers
                .Where(w => w.UserName.Equals(userAux.UserName) && w.Password.Equals(userAux.Password))
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return user;
        }
    }
}