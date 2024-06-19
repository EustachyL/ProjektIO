using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Testing
{
    public class GroupTest
    {
        public GroupTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddGroupToDatabase()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new GroupsRepository();

            var newGroup = new Group
            {
                Name = "Group A",
                Educator = new User { UserLogin = "educator1" }
            };

            // Act
            repository.Insert(newGroup);
            context.SaveChanges();

            // Assert
            var addedGroup = context.Groups.FirstOrDefault(g => g == newGroup);
            Assert.NotNull(addedGroup);
        }

        [Fact]
        public void IsNameValid_ShouldReturnTrueForValidName()
        {
            // Arrange
            var group = new Group();

            // Act
            var result = group.IsNameValid("Group A");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNameValid_ShouldReturnFalseForInvalidName()
        {
            // Arrange
            var group = new Group();

            // Act
            var result = group.IsNameValid("1nvalid");

            // Assert
            Assert.False(result);
        }
    }
}
