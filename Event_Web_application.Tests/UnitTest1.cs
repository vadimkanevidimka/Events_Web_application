using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Controllers;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

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

        [Fact]
        public async void GetAllUsers_ShouldReturnAllProducts()
        {
            var testuser = await _controller.GetAll();
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
                    FirstName = "Name",
                    LastName = "LastName",
                    RegistrationDate = DateTime.Now,
                },
            };
            var testuserid = await _controller.AddUser(newuser.Email, newuser.Password);
            var resultusers = await _controller.Get(testuserid);
            Assert.Equal(resultusers.Email, newuser.Email);
        }
    }
}