using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace EdukuJez.Tests
{
    public class UserParentTest
    {
        private readonly BaseContext _context;

        public UserParentTest()
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
            var repository = new UserParentsRepository();


            var NewStudent = new User
            {
                UserName = "Adam",
                UserSurname = "Kowalski",
                UserLogin = "ABBB",
                UserPassword = "ABBB"
            };

            var NewParent = new User
            {
                UserName = "Jan",
                UserSurname = "NieKowalski",
                UserLogin = "AAA",
                UserPassword = "AAA"
            };


            var NewUserParentRelation = new UserParent
            {
             StudentId = 0,
             ParentId = 0,
             Student = NewStudent,
             Parent = NewParent
            };

            // Act
            repository.AddNewEntry(NewUserParentRelation);

            // Assert
            var added = context.UserParents.FirstOrDefault(c => c.Id == 1);
            Assert.NotNull(added);
        }
    }
}