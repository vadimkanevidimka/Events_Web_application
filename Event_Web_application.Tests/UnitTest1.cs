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
        UsersController controller;


        private readonly HttpClient _httpClient;

        [Fact]
        public async void GetAllUsers_ShouldReturnAllProducts()
        {
            optionsBuilder = new DbContextOptionsBuilder<EWADBContext>();
            DbContextOptions options = optionsBuilder.UseSqlite(TestConnection).Options;
            _context = new EWADBContext((DbContextOptions<EWADBContext>)options);
            UsersController controller = new UsersController(new UnitOfWork(_context));
            var testusers = _context.Users.ToList();
            var resultusers = controller.GetAll();

            Assert.Equal(testusers, resultusers);
        }
    }
}