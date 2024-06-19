using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Testing
{
    public class ClassRoomTest
    {
        public ClassRoomTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddClassRoomToDatabase()
        {
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
            var addedClassRoom = context.ClassRoom.FirstOrDefault(c => c== newClassRoom);
            Assert.NotNull(addedClassRoom);

        }
    }
}