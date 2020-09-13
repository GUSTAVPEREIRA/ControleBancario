namespace ControleBancario.Services.Service
{
    using System;
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using ControleBancario.Model;
    using ControleBancario.Model.DTO;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using ControleBancario.Services.IService;

    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ISettingsService _settingsService;

        public UserService(ApplicationContext context, IMapper mapper, ISettingsService settingsService)
        {
            _context = context;
            _mapper = mapper;
            _settingsService = settingsService;
        }

        public async Task<User> CreateUser(UserDTO userDTO)
        {
            if (ExistUsernameInUser(userDTO.UserName))
            {
                throw new Exception("Este username já existe!");
            }

            var user = _mapper.Map<UserDTO, User>(userDTO, new User(userDTO.UserName, userDTO.Password));

            if (userDTO.SettingsID != 0)
            {
                _settingsService.GetSettingsForID(userDTO.SettingsID);
            }

            await _context.TbUsers.AddAsync(user);

            _context.SaveChanges();

            return user;
        }

        public async Task<User> GetUserForPasswordAndUsername(UserAuthenticateDTO userAuthenticateDTO)
        {
            //Feito pois existe uma regra de hash no password
            var userAux = _mapper.Map<UserAuthenticateDTO, User>(userAuthenticateDTO);

            User user = await _context.TbUsers
                .Where(w => w.UserName.Equals(userAux.UserName) && w.Password.Equals(userAux.Password))
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return user;
        }

        public User GetUserForID(int id)
        {
            var user = _context.TbUsers.Where(w => w.ID == id).Include(i => i.Settings).AsNoTracking().FirstOrDefault();

            return user;
        }

        public User GetActivatedUserForId(int id)
        {
            var user = _context.TbUsers.Where(w => w.ID == id).Include(i => i.Settings).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            if (user.DeletedAt != null)
            {
                throw new Exception($"{user.FName} {user.LName} foi desativado na data {user.DeletedAt}");
            }

            return user;
        }

        private bool ExistUsernameInUser(string username)
        {
            bool find = _context.TbUsers.Where(w => w.UserName.Equals(username)).Any();
            return find;
        }

        public async Task UpdateUser(UserDTO userDTO)
        {
            var user = this.GetActivatedUserForId(userDTO.ID);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            user.Update();

            if (userDTO.SettingsID != 0 && _settingsService.GetSettingsForID(userDTO.SettingsID) != null)
            {
                user.SettingsID = userDTO.SettingsID;
            }

            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                user.SetPassword(userDTO.Password);
            }

            user.SetEmail(userDTO.Email);
            user.FName = userDTO.FName;
            user.LName = userDTO.LName;

            _context.TbUsers.Add(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task LogicDeleted(int id)
        {
            var user = this.GetActivatedUserForId(id);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            user.SetLogicDeleted();

            _context.TbUsers.Add(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task UnsetLogicDeleted(int id)
        {
            var user = this.GetUserForID(id);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            user.UnsetLogicDeleted();
            _context.TbUsers.Add(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task PhysicalDeleted(int id)
        {
            var user = this.GetUserForID(id);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            _context.TbUsers.Add(user).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetListUsers(string filter)
        {
            var userList = await _context.TbUsers.ToListAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                userList = userList.Where(w => w.Email.Contains(filter) || w.LName.Contains(filter) || w.FName.Contains(filter) || w.UserName.Contains(filter)).ToList();
            }

            userList.ForEach(user =>
            {
                user.SetPassword("");
            });

            return userList;
        }
    }
}