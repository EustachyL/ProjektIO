using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Testing
{
    public class UserParentTest
    {
        private readonly BaseContext _context;

        public UserParentTest()
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
            var repository = new UserParentsRepository();
            var repositoryUser = new UsersRepository();


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

            repositoryUser.Insert(NewStudent);
            repositoryUser.Insert(NewParent);

            var NewUserParentRelation = new UserParent
            {
             StudentId = NewStudent.Id,
             ParentId = NewParent.Id,
             Student = NewStudent,
             Parent = NewParent
            };

            // Act
            repository.AddNewEntry(NewUserParentRelation);

            // Assert
            var added = context.UserParents.FirstOrDefault(c => c.ParentId == 2 && c.StudentId == 1);
            Assert.NotNull(added);
        }
    }
}