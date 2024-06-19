using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Testing
{
    public class UserTest
    {
        private readonly BaseContext _context;

        public UserTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewUser_ShouldAddNewUserToDatabaseAndCheckHisLoginData()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new UsersRepository();

            var NewUser = new User
            {
                UserName = "Adam",
                UserSurname = "Kowalski",
                UserLogin = "AAAAAAAAAAAAAA",
                UserPassword = "AAAAAAAAAAAAA"
            };


            Assert.NotNull(repository.CheckLogin(NewUser.UserLogin, NewUser.UserPassword));

        }
    }
}