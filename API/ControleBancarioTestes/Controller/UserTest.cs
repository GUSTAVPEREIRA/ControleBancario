namespace ControleBancarioTestes.Controller
{
    using Xunit;
    using AutoMapper;
    using ControleBancario;
    using System.Threading.Tasks;
    using ControleBancario.Model;
    using ControleBancario.Model.DTO;
    using Microsoft.EntityFrameworkCore;
    using ControleBancario.Services.Service;
    using ControleBancario.Services.IService;
    using Moq;
    using ControleBancario.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using ControleBancario.MappingModel;

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


        [Theory(DisplayName = "I want create a new user")]
        [InlineData("MANAGEMENT", "MANAGEMENT", "ADMIN", "Gustavo", "Pereira", "gugupereira123@hotmail.com")]
        [InlineData("ADMIN", "ADMIN", "ADMIN", "Gustavo", "Pereira", "gugupereira123@hotmail.com")]
        [InlineData("PROFESSOR", "PROFESSOR", "ADMIN", "Gustavo", "Pereira", "gugupereira123@hotmail.com")]
        public async Task CreateUser(string expectedUsername, string username, string password, string fname, string lname, string email)
        {
            //ARRANGE
            Context();
            MapperServiceConfigureProvider();
            SettingsServiceConfigureProvider();
            UserServiceConfigureProvider();
            
            UserDTO dto = new UserDTO(username, fname, lname, password, email);

            //ACT
            var user = await _userService.CreateUser(dto);

            //ASSERT
            Assert.Equal(expectedUsername, user.UserName);
        }
    }
}