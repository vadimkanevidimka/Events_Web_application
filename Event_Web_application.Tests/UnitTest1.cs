using Events_Web_application.Controllers;
using Events_Web_application_DataBase;
using Events_Web_application_DataBase.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace Event_Web_application.Tests
{
    public class UnitTestUserController
    {
        static string TestConnection = "Data Source = TestEWADB.db";
        static DbContextOptionsBuilder optionsBuilder;
        static EWADBContext _context;
        IUnitOfWork _unitOfWork;
        UsersController _controller;
        
        public UnitTestUserController() 
        {
            optionsBuilder = new DbContextOptionsBuilder<EWADBContext>();
            DbContextOptions options = optionsBuilder.UseSqlite(TestConnection).Options;
            _context = new EWADBContext((DbContextOptions<EWADBContext>)options);
            _unitOfWork = new UnitOfWork(_context);
            _controller = new UsersController(_unitOfWork);
        }


        private readonly HttpClient _httpClient;

        [Fact]
        public async void GetAllUsers_ShouldReturnAllProducts()
        {
            var testuser = _controller.GetAll();
            var resultusers = _context.Users.ToList();
            Assert.Equal(resultusers, testuser);
        }

        [Fact]  
        public async void AddNewUser_ShouldReturnTheNumberOfUser()
        {
            var newuser = new User
            {
                Email = "Email",
                Password = "password",
                Role = 2,
                Participant = new Participant
                {
                    DateOfBirth = DateTime.Now,
                    Email = "Email",
                    FirstName = "Name",
                    LastName = "LastName",
                    RegistrationDate = DateTime.Now,
                },
            };
            var testuserid = _controller.AddUser(newuser.Email, newuser.Password).Id;
            var resultusers = _controller.Get(testuserid);
            Assert.Equal(resultusers.Email, newuser.Email);
        }
    }
}