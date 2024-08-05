using Events_Web_application.Application.Services.DBServices;
using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Application.Services.Validation;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventsWebApplication.Tests
{
    public class DBServiceGenericTests
    {
        private DbContextOptions<EWADBContext> _dbContextOptions;
        private EWADBContext _context;
        private IDBService<User> _userService;

        public DBServiceGenericTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EWADBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new EWADBContext(_dbContextOptions);

            // Setup initial valid data
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "test1@example.com", Password = "password123", Role = 1, Participant = new Participant { FirstName = "test1", LastName ="1", DateOfBirth = DateTime.Now, RegistrationDate = DateTime.UtcNow } },
                new User { Id = Guid.NewGuid(), Email = "test2@example.com", Password = "password123", Role = 1, Participant = new Participant { FirstName = "test2", LastName ="2", DateOfBirth = DateTime.Now, RegistrationDate = DateTime.UtcNow }}
            };
            _context.AddRange(users);
            var eventEntity = new Event { Id = Guid.NewGuid(), Title = "Test Event", Description = "Description", EventDateTime = DateTime.Now, Location = "Location", MaxParticipants = 100, NameOfHost = "Host" };
            var image = new Image { Id = Guid.NewGuid(), Base64URL = "Base64EncodedImage", EventId = eventEntity.Id };
            eventEntity.EventImage = image;
            _context.Events.Add(eventEntity);
            _context.SaveChanges();

            _userService = new UsersService(_context);
        }

        [Fact]
        public async Task IsRecordExist_Should_Return_True_If_Record_Exists()
        {
            // Arrange
            var existingUser = await _context.Users.FirstAsync();
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userService.IsRecordExist(existingUser, cancellationTokenSource.Token);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsRecordExist_Should_Return_False_If_Record_Does_Not_Exist()
        {
            // Arrange
            var nonExistingUser = new User { Id = Guid.NewGuid(), Email = "nonexisting@example.com", Password = "password123", Role = 1 };
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userService.IsRecordExist(nonExistingUser, cancellationTokenSource.Token);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsRecordValid_Should_Return_True_If_Record_Is_Valid()
        {
            // Arrange
            var validUser = new User { Id = Guid.NewGuid(), Email = "valid@example.com", Password = "password123", Role = 1 };
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userService.IsRecordValid(validUser, cancellationTokenSource.Token);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsRecordValid_Should_Return_False_If_Record_Is_Invalid()
        {
            // Arrange
            var invalidUser = new User { Id = Guid.NewGuid(), Email = "invalid@example.com", Password = "1234567", Role = 1 }; //Less then 8 symbols
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userService.IsRecordValid(invalidUser, cancellationTokenSource.Token);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsRecordDublicate_Should_Return_True_If_Record_Is_Duplicate()
        {
            // Arrange
            var existingUser = await _context.Users.FirstAsync();
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userService.IsRecordDublicate(existingUser, cancellationTokenSource.Token);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsRecordDublicate_Should_Return_False_If_Record_Is_Not_Duplicate()
        {
            // Arrange
            var nonDuplicateUser = new User { Id = Guid.NewGuid(), Email = "unique@example.com", Password = "password123", Role = 1 };
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userService.IsRecordDublicate(nonDuplicateUser, cancellationTokenSource.Token);

            // Assert
            Assert.False(result);
        }
    }
}
