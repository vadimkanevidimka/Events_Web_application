// EventsWebApplication.Tests/RepositoryTests.cs
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Events_Web_application.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Tests
{
    public class RepositoryTests
    {
        private DbContextOptions<EWADBContext> _dbContextOptions;
        private EWADBContext _context;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Event> _eventRepository;
        private GenericRepository<EventCategory> _categoryRepository;
        private GenericRepository<Participant> _participantRepository;
        private GenericRepository<Image> _imageRepository;

        public RepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EWADBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new EWADBContext(_dbContextOptions);
            _userRepository = new GenericRepository<User>(_context);
            _eventRepository = new GenericRepository<Event>(_context);
            _categoryRepository = new GenericRepository<EventCategory>(_context);
            _participantRepository = new GenericRepository<Participant>(_context);
            _imageRepository = new GenericRepository<Image>(_context);
        }

        [Fact]
        public async Task Add_User_Should_Add_User_To_Database()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", Password = "password123", Role = 1 };
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userRepository.Add(user, cancellationTokenSource);

            // Assert
            Assert.Equal(1, result);
            var resUser = await _userRepository.Get(user.Id, cancellationTokenSource.Token);
            Assert.Equal("test@example.com", resUser.Email);
        }

        [Fact]
        public async Task Add_Event_Should_Add_Event_To_Database()
        {
            // Arrange
            var eventEntity = new Event { Id = Guid.NewGuid(), Title = "Test Event", Description = "Description", EventDateTime = DateTime.Now, Location = "Location", MaxParticipants = 100, NameOfHost = "Host" };
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _eventRepository.Add(eventEntity, cancellationTokenSource);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Add_EventCategory_Should_Add_EventCategory_To_Database()
        {
            // Arrange
            var category = new EventCategory { Id = Guid.NewGuid(), Name = "Category1", Description = "Description" };
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _categoryRepository.Add(category, cancellationTokenSource);

            // Assert
            Assert.Equal(1, result);
            Assert.Equal("Category1", _context.Set<EventCategory>().First().Name);
        }

        [Fact]
        public async Task Add_Participant_Should_Add_Participant_To_Database()
        {
            // Arrange
            var participant = new Participant { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Now.AddYears(-30), RegistrationDate = DateTime.Now };
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _participantRepository.Add(participant, cancellationTokenSource);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Add_Image_Should_Add_Image_To_Database()
        {
            // Arrange
            var eventEntity = new Event { Id = Guid.NewGuid(), Title = "Test Event", Description = "Description", EventDateTime = DateTime.Now, Location = "Location", MaxParticipants = 100, NameOfHost = "Host" };
            var image = new Image { Id = Guid.NewGuid(), Base64URL = "Base64EncodedImage", EventId = eventEntity.Id };
            eventEntity.EventImage = image;
            var cancellationTokenSource = new CancellationTokenSource();
            await _eventRepository.Add(eventEntity, cancellationTokenSource);

            // Act
            var result = await _imageRepository.Get(image.Id, cancellationTokenSource.Token);

            // Assert
            Assert.Equal(image, result);
        }

        // More test cases for Update, Delete and Get methods

        [Fact]
        public async Task Update_User_Should_Modify_User_In_Database()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", Password = "password123", Role = 1 };
            _context.Add(user);
            await _context.SaveChangesAsync();
            user.Email = "updated@example.com";
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userRepository.Update(user, cancellationTokenSource);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Delete_User_Should_Remove_User_From_Database()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", Password = "password123", Role = 1 };
            _context.Add(user);
            await _context.SaveChangesAsync();
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userRepository.Delete(user.Id, cancellationTokenSource);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Get_User_Should_Return_Correct_User()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Email = "test@example.com", Password = "password123", Role = 1 };
            _context.Add(user);
            await _context.SaveChangesAsync();
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userRepository.Get(user.Id, cancellationTokenSource.Token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task GetAll_Users_Should_Return_All_Users()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "test1@example.com", Password = "password123", Role = 1, Participant = new Participant { FirstName = "test1", LastName ="1", DateOfBirth = DateTime.Now, RegistrationDate = DateTime.UtcNow } },
                new User { Id = Guid.NewGuid(), Email = "test2@example.com", Password = "password123", Role = 1, Participant = new Participant { FirstName = "test2", LastName ="2", DateOfBirth = DateTime.Now, RegistrationDate = DateTime.UtcNow }}
            };
            _context.AddRange(users);
            await _context.SaveChangesAsync();
            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await _userRepository.GetAll(cancellationTokenSource);

            // Assert
            Assert.NotNull(result);
            var a = result.ToList().Count;
            Assert.Equal(2, a);
        }
    }
}
