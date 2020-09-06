namespace ControleBancario.Services.Service
{    
    using System;
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using ControleBancario.Model;
    using ControleBancario.Model.DTO;
    using Microsoft.EntityFrameworkCore;
    using ControleBancario.Services.IService;

    public class UserService : IUserService
    {        
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public UserService(ApplicationContext context, IMapper mapper)
        {            
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(UserDTO userDTO)
        {
            if (ExistUsernameInUser(userDTO.UserName))
            {
                throw new Exception("Este username já existe!");
            }

            var user = _mapper.Map<UserDTO, User>(userDTO, new User(userDTO.UserName, userDTO.Password));
            await _context.TbUsers.AddAsync(user);

            _context.SaveChanges();

            return user;
        }

        public async Task<User> GetUserForPasswordAndUsername(string username, string password)
        {
            //Feito pois existe uma regra de hash no password
            User userAux = new User(username, password);
          
            User user = await _context.TbUsers
                .Where(w => w.UserName.Equals(userAux.UserName) && w.Password.Equals(userAux.Password))
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return user;
        }

        public User GetUserForID(int id)
        {
            var user = _context.TbUsers.Where(w => w.ID == id).FirstOrDefault();
            return user;
        }

        private bool ExistUsernameInUser(string username)
        {
            bool find = _context.TbUsers.Where(w => w.UserName.Equals(username)).Any();
            return find;
        }

    }
}