using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace EdukuJez.Tests
{
    public class UnitTest1
    {
        private readonly BaseContext _context;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            _context = new BaseContext(options);
        }

        [Fact]
        public void AddNewEntry_ShouldAddClassRoomToDatabase()
        {
            var BaseContext = new BaseContext();
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new ClassRoomsRepository();

            var newClassRoom = new ClassRoom
            {
                Number = "101",
                Desc = "Math Classroom"
            };

            // Act
            repository.AddNewEntry(newClassRoom);

            // Assert
            var addedClassRoom = context.ClassRoom.FirstOrDefault(c => c.Number == "101");
            Assert.NotNull(addedClassRoom);
            Assert.Equal("Math Classroom", addedClassRoom.Desc);

        }
    }
}