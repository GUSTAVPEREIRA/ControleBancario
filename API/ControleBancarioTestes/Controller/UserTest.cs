namespace ControleBancarioTestes.Controller
{
    using Moq;
    using Xunit;
    using System;
    using AutoMapper;
    using ControleBancario;
    using System.Threading.Tasks;
    using ControleBancario.Model;
    using ControleBancario.Model.DTO;
    using ControleBancario.MappingModel;
    using Microsoft.EntityFrameworkCore;
    using ControleBancario.Services.Service;
    using ControleBancario.Services.IService;

    public class UserTest
    {
        private IMapper _mapper;
        private IUserService _userService;
        private ApplicationContext _context;
        private ISettingsService _settingsService;


        private void UserServiceConfigureProvider()
        {
            _userService = new UserService(_context, _mapper, _settingsService);
        }

        private void SettingsServiceConfigureProvider()
        {
            _settingsService = new SettingsService(_context, _mapper);
        }

        private void Context()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase("ControleBancarioTestes").Options;
            var context = new ApplicationContext(options);
            _context = context;
        }

        private void MapperServiceConfigureProvider()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingSettings());
                cfg.AddProfile(new MappingUser());
            });

            _mapper = mockMapper.CreateMapper();
        }

        private void Initialize()
        {
            Context();
            MapperServiceConfigureProvider();
            SettingsServiceConfigureProvider();
            UserServiceConfigureProvider();
        }

        private async Task<Settings> CreateSetting()
        {
            SettingsDTO settings = new SettingsDTO
            {
                IsAdmin = true,
                IsCreateUser = true,
                IsManager = true,
                Name = "Administrador"
            };

            var setting = await _settingsService.CreateSettings(settings);
            return setting;
        }


        [Theory(DisplayName = "I want create a new user")]
        [InlineData("MANAGEMENT", "MANAGEMENT", "ADMIN", "Gustavo", "Pereira", "gugupereira123@hotmail.com")]
        [InlineData("ADMIN", "ADMIN", "ADMIN", "Gustavo", "Pereira", "gugupereira123@hotmail.com")]
        [InlineData("PROFESSOR", "PROFESSOR", "ADMIN", "Gustavo", "Pereira", "gugupereira123@hotmail.com")]
        public async Task CreateUser(string expectedUsername, string username, string password, string fname, string lname, string email)
        {
            //ARRANGE
            this.Initialize();
            UserDTO userDTO = new UserDTO(username, fname, lname, password, email);

            //ACT
            var user = await _userService.CreateUser(userDTO);

            //ASSERT
            Assert.Equal(expectedUsername, user.UserName);
        }

        [Theory(DisplayName = "I want update an user")]
        [InlineData(true, "teste3", "TESTE3", "ADMIN", "Gustavo", "Pereira1", "ADMIN", "gugupereira123@hotmail.com", true, false, false)]
        [InlineData(true, "teste3", "TESTE3", "ADMIN", "Gustavo", "Pereira2", "ADMIN", "gugupereira123@hotmail.com", true, true, true)]
        [InlineData(true, "teste3", "TESTE3", "ADMIN", "Gustavo", "Pereira3", "ADMIN", "gugupereira123@hotmail.com", false, false, true)]
        [InlineData(true, "Teste", "Teste2", "ADMIN", "Gustavo", "Pereira4", "ADMIN", "gugupereira123@hotmail.com", true, false, true)]
        [InlineData(false, "teste3", "TESTE3", "ADMIN", "Gustavo", "Pereira5", "ADMIN", "gugupereira123@hotmail.com", false, true, false)]
        [InlineData(false, "Teste", "Teste2", "ADMIN", "Gustavo", "Pereira6", "ADMIN", "gugupereira123@hotmail.com", true, true, false)]
        [InlineData(false, "Pereira", "Gustavo", "ADMIN", "Gustavo", "Pereira7", "ADMIN", "gugupereira123@hotmail.com", false, true, false)]
        [InlineData(false, "teste3", "TESTE3", "ADMIN", "Gustavo", "Pereira8", "ADMIN", "gugupereira123@hotmail.com", false, false, false)]
        public async Task UpdateUser(bool expectedAdmin, string expectedFname, string expectedLname, string fname, string lname, string username, string password, string email, bool withSettings, bool removeSettings, bool updatedInEnd)
        {
            //ARRANGE
            Initialize();

            var settings = await CreateSetting();
            var settings2 = await CreateSetting();
            int settingsID = 0;
            int settingsID2 = settings2.ID;            

            if (withSettings)
            {
                settingsID = settings.ID;                
            }

            UserDTO userDTO = new UserDTO(username, fname, lname, password, email)
            {
                SettingsID = settingsID
            };

            var user = await _userService.CreateUser(userDTO);
            userDTO.ID = user.ID;
            userDTO.FName = expectedFname;
            userDTO.LName = expectedLname;
            
            if (removeSettings)
            {
                userDTO.SettingsID = 0;
            }

            if (updatedInEnd)
            {
                userDTO.SettingsID = settingsID2;
            }

            //ACT
            await _userService.UpdateUser(userDTO);
            user = _userService.GetUserForID(userDTO.ID);

            //ASSERT
            Assert.Equal(expectedFname, user.FName);
            Assert.Equal(expectedLname, user.LName);

            if (expectedAdmin)
            {
                Assert.NotNull(user.Settings);
            }
            else
            {
                Assert.Null(user.Settings);
            }

        }

        [Fact(DisplayName = "I want create user, but i wish throw exception for invalid email!")]
        public async Task EmailException()
        {
            //ARRANGE
            string exceptionMessage = "Email, não é válido!";
            UserDTO userDTO = new UserDTO("ADMINISTRATOR", "Gustavo", "Pereira", "ADMIN", "WRONGEMAIL");
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.CreateUser(It.IsAny<UserDTO>())).Throws(new Exception(exceptionMessage));
            var repo = mock.Object;

            //ACT
            async Task actAsync() => await repo.CreateUser(userDTO);

            //ASSERT
            await Assert.ThrowsAsync<Exception>(actAsync);
            var ex = await Assert.ThrowsAsync<Exception>(() => actAsync());
            Assert.Equal(ex.Message, exceptionMessage);
        }

        [Fact(DisplayName = "I want a logic deleted an user")]
        public async Task ValidLogicDeletedUser()
        {
            //ARRANGE
            Initialize();
            UserDTO userDTO = new UserDTO("LOGICDELETEDTEST", "Gustavo", "Pereira", "ADMIN", "gugupereira123@hotmail.com");
            var user = await _userService.CreateUser(userDTO);
            int userID = user.ID;

            //ACT
            await _userService.LogicDeleted(userID);
            user = _userService.GetUserForID(userID);

            //ASSERT
            Assert.NotNull(user.DeletedAt);
        }

        [Fact(DisplayName = "I want an unset logic delete an user with deleteat is not null")]
        public async Task ValidUnsetLogicDeletedUser()
        {
            //ARRANGE
            Initialize();
            UserDTO userDTO = new UserDTO("UNSETLOGICDELETEDTEST", "Gustavo", "Pereira", "ADMIN", "gugupereira123@hotmail.com");
            var user = await _userService.CreateUser(userDTO);
            await _userService.LogicDeleted(user.ID);
            Assert.NotNull(user.DeletedAt);
            _context.TbUsers.Add(user).State = EntityState.Detached;

            //ACT
            await _userService.UnsetLogicDeleted(user.ID);
            user = _userService.GetUserForID(user.ID);

            //ASSERT
            Assert.Null(user.DeletedAt);
        }

        [Fact(DisplayName = "Deleted an user and wanted ensure user is deleted")]
        public async Task DeleteUser()
        {
            //ARRANGE
            Initialize();
            UserDTO userDTO = new UserDTO("DELETEDUSER", "Gustavo", "Pereira", "ADMIN", "gugupereira123@hotmail.com");
            var user = await _userService.CreateUser(userDTO);
            _context.TbUsers.Add(user).State = EntityState.Detached;
            int id = user.ID;
            //ACT
            await _userService.PhysicalDeleted(id);

            //ASSERT
            Assert.Null(_userService.GetUserForID(id));
        }
    }
}